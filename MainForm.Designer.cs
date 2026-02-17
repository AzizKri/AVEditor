using System.Drawing;
using System.Windows.Forms;

namespace VideoEditor
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            fileInput = new Button();
            label3 = new Label();
            fileSize = new NumericUpDown();
            label4 = new Label();
            label5 = new Label();
            mute = new CheckBox();
            button1 = new Button();
            log = new TextBox();
            filePath = new Label();
            outputFile = new Button();
            outputPath = new Label();
            startFrom = new TextBox();
            endAt = new TextBox();
            ((System.ComponentModel.ISupportInitialize)fileSize).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.75F);
            label1.Location = new Point(10, 13);
            label1.Name = "label1";
            label1.Size = new Size(75, 17);
            label1.TabIndex = 1;
            label1.Text = "Input Video";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F);
            label2.Location = new Point(10, 199);
            label2.Name = "label2";
            label2.Size = new Size(87, 17);
            label2.TabIndex = 2;
            label2.Text = "Output Name";
            label2.Click += label2_Click;
            // 
            // fileInput
            // 
            fileInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            fileInput.Font = new Font("Segoe UI", 9.75F);
            fileInput.Location = new Point(136, 10);
            fileInput.Margin = new Padding(2, 3, 2, 3);
            fileInput.Name = "fileInput";
            fileInput.Size = new Size(93, 29);
            fileInput.TabIndex = 3;
            fileInput.Text = "Select";
            fileInput.UseVisualStyleBackColor = true;
            fileInput.Click += button1_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9.75F);
            label3.Location = new Point(10, 70);
            label3.Name = "label3";
            label3.Size = new Size(111, 17);
            label3.TabIndex = 4;
            label3.Text = "Desired Size (MB)";
            label3.Click += label3_Click;
            // 
            // fileSize
            // 
            fileSize.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            fileSize.Font = new Font("Segoe UI", 9.75F);
            fileSize.Location = new Point(136, 68);
            fileSize.Margin = new Padding(2, 3, 2, 3);
            fileSize.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
            fileSize.Name = "fileSize";
            fileSize.Size = new Size(91, 25);
            fileSize.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9.75F);
            label4.Location = new Point(10, 101);
            label4.Name = "label4";
            label4.Size = new Size(84, 17);
            label4.TabIndex = 7;
            label4.Text = "Start (mm:ss)";
            label4.Click += label4_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9.75F);
            label5.Location = new Point(10, 132);
            label5.Name = "label5";
            label5.Size = new Size(79, 17);
            label5.TabIndex = 10;
            label5.Text = "End (mm:ss)";
            // 
            // mute
            // 
            mute.AutoSize = true;
            mute.Location = new Point(10, 164);
            mute.Name = "mute";
            mute.Size = new Size(95, 21);
            mute.TabIndex = 12;
            mute.Text = "Mute Audio";
            mute.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button1.Font = new Font("Segoe UI", 9.75F);
            button1.Location = new Point(74, 249);
            button1.Margin = new Padding(2, 3, 2, 3);
            button1.Name = "button1";
            button1.Size = new Size(93, 29);
            button1.TabIndex = 14;
            button1.Text = "Run";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // log
            // 
            log.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            log.Enabled = false;
            log.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            log.Location = new Point(12, 284);
            log.Multiline = true;
            log.Name = "log";
            log.Size = new Size(215, 83);
            log.TabIndex = 15;
            // 
            // filePath
            // 
            filePath.AutoSize = true;
            filePath.Font = new Font("Segoe UI", 9.75F);
            filePath.Location = new Point(12, 42);
            filePath.Name = "filePath";
            filePath.Size = new Size(48, 17);
            filePath.TabIndex = 16;
            filePath.Text = "(None)";
            // 
            // outputFile
            // 
            outputFile.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            outputFile.Font = new Font("Segoe UI", 9.75F);
            outputFile.Location = new Point(134, 193);
            outputFile.Margin = new Padding(2, 3, 2, 3);
            outputFile.Name = "outputFile";
            outputFile.Size = new Size(93, 29);
            outputFile.TabIndex = 17;
            outputFile.Text = "Select";
            outputFile.UseVisualStyleBackColor = true;
            outputFile.Click += outputFile_Click;
            // 
            // outputPath
            // 
            outputPath.AutoSize = true;
            outputPath.Font = new Font("Segoe UI", 9.75F);
            outputPath.Location = new Point(12, 226);
            outputPath.Name = "outputPath";
            outputPath.Size = new Size(48, 17);
            outputPath.TabIndex = 18;
            outputPath.Text = "(None)";
            // 
            // startFrom
            // 
            startFrom.Location = new Point(136, 98);
            startFrom.Name = "startFrom";
            startFrom.Size = new Size(91, 25);
            startFrom.TabIndex = 19;
            // 
            // endAt
            // 
            endAt.Location = new Point(138, 129);
            endAt.Name = "endAt";
            endAt.Size = new Size(91, 25);
            endAt.TabIndex = 20;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(240, 379);
            Controls.Add(endAt);
            Controls.Add(startFrom);
            Controls.Add(outputPath);
            Controls.Add(outputFile);
            Controls.Add(filePath);
            Controls.Add(log);
            Controls.Add(button1);
            Controls.Add(mute);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(fileSize);
            Controls.Add(label3);
            Controls.Add(fileInput);
            Controls.Add(label2);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = Properties.Resources.VideoEditorIcon;
            Margin = new Padding(3, 4, 3, 4);
            Name = "MainForm";
            Text = "Video Editor";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)fileSize).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Label label2;
        private Button fileInput;
        private Label label3;
        private NumericUpDown fileSize;
        private Label label4;
        private Label label5;
        private CheckBox mute;
        private Button button1;
        private TextBox log;
        private Label filePath;
        private Button outputFile;
        private Label outputPath;
        private TextBox startFrom;
        private TextBox endAt;
    }
}
