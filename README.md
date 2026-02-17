# AVEditor

A simple application to cut and/or reduce the size of videos using ffmpeg.

### Options:
- Input: The video you'd like to modify.
- Desired Size (Optional): The output size in MB (Note: The output will be around the given size, usually slightly higher). Leave empty to use the original video's bitrate
- Start (Optional): Cut the video to start from the given timestamp (mm:ss). Leave empty to start from 00:00
- End (Optional): Cut the video to end at the given timestamp (mm:ss). Leave empty to continue until the end of the video
- Mute Audio (Optional): Mute the audio.
- Output Name: The output name and location of the file. Automatically set to `[original_name]_cut.mp4` and the same location as the input video.

Note: The application may be marked as unsafe by Windows Defender because the exe is unsigned (and I do not plan on purchasing a certificate for such a small application). You may check the code and/or the [VirusTotal (Zip)](https://www.virustotal.com/gui/file/df17ddc212ffa36f995df2813f04e5d83f9f878fe0c759facc36345ab70eb7ac) [VirusTotal (exe)](https://www.virustotal.com/gui/file/0db6b3cf6219c835b605e0625a3611b45c81bba339dc85a123f081f69f240184) scan to make sure it's safe.
