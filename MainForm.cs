using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace VideoEditor
{
    public partial class MainForm : Form
    {
        string ffmpegPath;
        string ffprobePath;
        string inputPathString;
        string outputPathString;

        public MainForm()
        {
            InitializeComponent();
            ffmpegPath = ExtractFfmpeg();
            ffprobePath = ExtractFfprobe();
        }

        // helpers
        private string ExtractFfmpeg()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ffmpeg.exe");
            Debug.WriteLine("FFmpeg path: " + path);
            return path;

            //if (!File.Exists(temp))
            //{
            //    // ffmpeg is embedded in project resources
            //    File.WriteAllBytes(temp, Properties.Resources.ffmpeg);
            //}

            //return temp;
        }
        private string ExtractFfprobe()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ffprobe.exe");

            //if (!File.Exists(temp))
            //{
            //    File.WriteAllBytes(temp, Properties.Resources.ffprobe);
            //}

            //return temp;
        }
        private bool NvencAvailable()
        {
            Process p = new Process();
            p.StartInfo.FileName = ffmpegPath;
            p.StartInfo.Arguments = "-hide_banner -encoders";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = true;

            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();

            return output.Contains("h264_nvenc");
        }

        private string BuildFfmpegCommand()
        {
            string input = inputPathString;
            string start = startFrom.Text; // mm:ss
            string end = endAt.Text; // mm:ss
            bool isMuted = mute.Checked;
            TimeSpan startTime = TimeSpan.Parse("00:00:00");
            TimeSpan endTime;
            double videoLength = GetVideoLength(inputPathString);

            if (!File.Exists(input))
                throw new Exception("Input file does not exist.");

            if (!string.IsNullOrEmpty(start))
            {
                // parse start time from mm:ss to seconds
                if (!TimeSpan.TryParse("00:" + start, out startTime))
                    throw new Exception("Invalid start time format.");

                if (startTime.TotalSeconds > videoLength)
                    throw new Exception("Start time cannot be longer than the video");
            }

            // calculate duration
            bool hasDuration = false;
            double durationSec = 0;

            if (!string.IsNullOrEmpty(end))
            {
                // we have end time
                if (!!TimeSpan.TryParse("00:" + end, out endTime))
                {
                    hasDuration = true;
                    durationSec = endTime.TotalSeconds - startTime.TotalSeconds;
                }
                else
                    throw new Exception("Invalid end time format.");

                // end time is after the end of the video
                if (endTime.TotalSeconds > videoLength)
                    durationSec = videoLength - startTime.TotalSeconds;
            }

            // we have an end time, either user provided it or we calculate full length
            if (durationSec > 0)
                hasDuration = true;
            else if (durationSec == 0)
            {
                // no end time provided
                durationSec = videoLength - startTime.TotalSeconds;
            }
            else
                throw new Exception("End time cannot be before start time");

            // calculate bitrate based on file size
            double sizeMB = double.Parse(fileSize.Text);
            bool hasSize = sizeMB > 0;
            double bitrateKbps = 0;
            if (hasSize)
            {
                bitrateKbps = (sizeMB * 1024 * 1024 * 8) / durationSec / 1000;
            }
            else
            {
                bitrateKbps = GetVideoBitrate(inputPathString);
            }

            // NVENC fallback
            string videoCodec = NvencAvailable() ? "h264_nvenc" : "libx264";

            // Audio section
            string audio = isMuted ? "-an" : GetAudio(inputPathString);

            // Build command
            return $"-y -ss {startTime.TotalSeconds} -i \"{input}\" " + (hasDuration ? $"-t {durationSec} " : "") +
                   $"-c:v {videoCodec} -b:v {bitrateKbps:F0}k " +
                   $"{audio} \"{outputPathString}\"";
        }

        private void RunFfmpeg(string args)
        {
            log.Clear();
            log.AppendText("Running:\r\n" + args + "\r\n\r\n");

            Process p = new Process();
            p.StartInfo.FileName = ffmpegPath;
            p.StartInfo.Arguments = args;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;

            p.ErrorDataReceived += (s, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                    log.Invoke(new Action(() => log.AppendText(e.Data + "\r\n")));
            };

            p.Start();
            p.BeginErrorReadLine();
            p.WaitForExit(120 * 100);
            Debug.WriteLine("Process exited");


            log.AppendText("\r\nDone!");
        }

        private int GetVideoBitrate(string inputPath)
        {
            Process p = new Process();
            p.StartInfo.FileName = ffprobePath;
            p.StartInfo.Arguments = $"-v error -select_streams v:0 -show_entries stream=bit_rate -of default=noprint_wrappers=1:nokey=1 \"{inputPath}\"";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            if (int.TryParse(output.Trim(), out int bitrate))
            {
                return bitrate / 1000; // convert to kbps
            }
            return 0;
        }

        private double GetVideoLength(string inputPath)
        {
            Process p = new Process();
            p.StartInfo.FileName = ffprobePath;
            p.StartInfo.Arguments = $"-v error -show_entries format=duration -of default=noprint_wrappers=1:nokey=1 \"{inputPath}\"";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            if (double.TryParse(output.Trim(), out double durationSec))
            {
                return durationSec;
            }
            return 1; // we return 1 second to avoid division by zero
        }

        private string GetAudio(string inputPath)
        {
            var audioTracks = GetAudioTracks(inputPath);

            if (audioTracks.Count == 1)
            {
                return "-c:a aac -b:a 128k";
            }
            else if (audioTracks.Count >= 2)
            {
                return "-filter_complex \"[0:a:0]volume=0.3[a1]; [0:a:1]volume=1[a2]; [a1][a2]amix=inputs=2:normalize=1\" -c:a aac -b:a 128k";
            }
            else
            {
                return "-an";
            }
        }

        private List<int> GetAudioTracks(string inputPath)
        {
            List<int> tracks = new List<int>();

            Process p = new Process();
            p.StartInfo.FileName = ffprobePath;
            p.StartInfo.Arguments = $"-v error -select_streams a -show_entries stream=index -of csv=p=0 \"{inputPath}\"";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = true;

            p.Start();
            string output = p.StandardOutput.ReadToEnd().Trim();
            p.WaitForExit();

            foreach (string line in output.Split('\n'))
            {
                if (int.TryParse(line.Trim(), out int index))
                    tracks.Add(index);
            }

            return tracks;
        }

        // form code

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "Video files|*.mp4;*.mkv;*.mov;*.avi|All files|*.*";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    string dir = Path.GetDirectoryName(dlg.FileName);
                    string name = Path.GetFileNameWithoutExtension(dlg.FileName);

                    inputPathString = dlg.FileName;
                    filePath.Text = name;
                    outputPathString = Path.Combine(dir, name + "_cut.mp4");
                    outputPath.Text = name + "_cut.mp4";
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                string args = BuildFfmpegCommand();
                RunFfmpeg(args);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void outputFile_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = "MP4 File|*.mp4";
                dlg.FileName = outputFile.Text;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    outputPath.Text = Path.GetFileNameWithoutExtension(dlg.FileName);
                    outputPathString = dlg.FileName;
                }
            }
        }

        private void duration_ValueChanged(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
