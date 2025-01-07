namespace ThresholdingApp
{
    partial class Form1
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
            btn_addFile = new Button();
            openFileDialog1 = new OpenFileDialog();
            pictureBox_before = new PictureBox();
            pictureBox_after = new PictureBox();
            txtSelectedFile = new Label();
            txtAfterThresholding = new Label();
            btnDownload = new Button();
            txtInfo = new Label();
            richTextBox1 = new RichTextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox_before).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_after).BeginInit();
            SuspendLayout();
            // 
            // btn_addFile
            // 
            btn_addFile.BackColor = Color.Black;
            btn_addFile.Cursor = Cursors.Hand;
            btn_addFile.FlatAppearance.BorderColor = Color.Black;
            btn_addFile.FlatAppearance.BorderSize = 0;
            btn_addFile.FlatStyle = FlatStyle.Flat;
            btn_addFile.Font = new Font("Consolas", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btn_addFile.ForeColor = Color.White;
            btn_addFile.Location = new Point(11, 11);
            btn_addFile.Margin = new Padding(2);
            btn_addFile.Name = "btn_addFile";
            btn_addFile.Size = new Size(200, 40);
            btn_addFile.TabIndex = 0;
            btn_addFile.Text = "Select file";
            btn_addFile.UseVisualStyleBackColor = false;
            btn_addFile.Click += testLibraries;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // pictureBox_before
            // 
            pictureBox_before.BorderStyle = BorderStyle.FixedSingle;
            pictureBox_before.Image = Properties.Resources.undraw_images;
            pictureBox_before.Location = new Point(245, 99);
            pictureBox_before.Name = "pictureBox_before";
            pictureBox_before.Size = new Size(300, 250);
            pictureBox_before.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox_before.TabIndex = 1;
            pictureBox_before.TabStop = false;
            // 
            // pictureBox_after
            // 
            pictureBox_after.BorderStyle = BorderStyle.FixedSingle;
            pictureBox_after.Image = Properties.Resources.undraw_images;
            pictureBox_after.Location = new Point(564, 99);
            pictureBox_after.Name = "pictureBox_after";
            pictureBox_after.Size = new Size(300, 250);
            pictureBox_after.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox_after.TabIndex = 2;
            pictureBox_after.TabStop = false;
            // 
            // txtSelectedFile
            // 
            txtSelectedFile.AutoSize = true;
            txtSelectedFile.Font = new Font("Consolas", 10F, FontStyle.Regular, GraphicsUnit.Point);
            txtSelectedFile.Location = new Point(245, 72);
            txtSelectedFile.Name = "txtSelectedFile";
            txtSelectedFile.Size = new Size(120, 17);
            txtSelectedFile.TabIndex = 3;
            txtSelectedFile.Text = "Selected file:";
            // 
            // txtAfterThresholding
            // 
            txtAfterThresholding.AutoSize = true;
            txtAfterThresholding.Font = new Font("Consolas", 10F, FontStyle.Regular, GraphicsUnit.Point);
            txtAfterThresholding.Location = new Point(564, 72);
            txtAfterThresholding.Name = "txtAfterThresholding";
            txtAfterThresholding.Size = new Size(160, 17);
            txtAfterThresholding.TabIndex = 4;
            txtAfterThresholding.Text = "After thresholding:";
            // 
            // btnDownload
            // 
            btnDownload.BackColor = Color.Black;
            btnDownload.Cursor = Cursors.Hand;
            btnDownload.FlatAppearance.BorderColor = Color.Black;
            btnDownload.FlatAppearance.BorderSize = 0;
            btnDownload.FlatStyle = FlatStyle.Flat;
            btnDownload.Font = new Font("Consolas", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnDownload.ForeColor = Color.White;
            btnDownload.Location = new Point(618, 366);
            btnDownload.Margin = new Padding(2);
            btnDownload.Name = "btnDownload";
            btnDownload.Size = new Size(200, 40);
            btnDownload.TabIndex = 5;
            btnDownload.Text = "Download";
            btnDownload.UseVisualStyleBackColor = false;
            // 
            // txtInfo
            // 
            txtInfo.AutoSize = true;
            txtInfo.Font = new Font("Consolas", 10F, FontStyle.Regular, GraphicsUnit.Point);
            txtInfo.Location = new Point(11, 72);
            txtInfo.Name = "txtInfo";
            txtInfo.Size = new Size(192, 17);
            txtInfo.TabIndex = 6;
            txtInfo.Text = "Information about file:";
            txtInfo.Click += label1_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.BorderStyle = BorderStyle.None;
            richTextBox1.Enabled = false;
            richTextBox1.Location = new Point(12, 99);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(200, 250);
            richTextBox1.TabIndex = 7;
            richTextBox1.Text = "Select the file to see inforamations...";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(876, 432);
            Controls.Add(richTextBox1);
            Controls.Add(txtInfo);
            Controls.Add(btnDownload);
            Controls.Add(txtAfterThresholding);
            Controls.Add(txtSelectedFile);
            Controls.Add(pictureBox_after);
            Controls.Add(pictureBox_before);
            Controls.Add(btn_addFile);
            Margin = new Padding(2);
            Name = "Form1";
            Text = "ThresholdingApp";
            ((System.ComponentModel.ISupportInitialize)pictureBox_before).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_after).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_addFile;
        private OpenFileDialog openFileDialog1;
        private PictureBox pictureBox_before;
        private PictureBox pictureBox_after;
        private Label txtSelectedFile;
        private Label txtAfterThresholding;
        private Button btnDownload;
        private Label txtInfo;
        private RichTextBox richTextBox1;
    }
}