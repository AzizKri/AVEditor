# VideoEditor

A simple application to cut and/or reduce the size of videos using ffmpeg.

### Options:
- Input: The video you'd like to modify.
- Desired Size (Optional): The output size in MB (Note: The output will be around the given size, usually slightly higher). Leave empty to use the original video's bitrate
- Start (Optional): Cut the video to start from the given timestamp (mm:ss). Leave empty to start from 00:00
- End (Optional): Cut the video to end at the given timestamp (mm:ss). Leave empty to continue until the end of the video
- Mute Audio (Optional): Mute the audio.
- Output Name: The output name and location of the file. Automatically set to `[original_name]_cut.mp4` and the same location as the input video.

Note: The application may be marked as unsafe by Windows Defender as the exe is unsigned. You may check the code and/or the [VirusTotal](https://www.virustotal.com/gui/file/f4de5ed349c3970a4f48c79a436770f13e3620944fd459e17f96f9c6f43e282b) scan to make sure it's safe.
