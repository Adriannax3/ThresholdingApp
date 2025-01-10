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
            components = new System.ComponentModel.Container();
            btn_addFile = new Button();
            openFileDialog1 = new OpenFileDialog();
            pictureBox_before = new PictureBox();
            pictureBox_after = new PictureBox();
            txtSelectedFile = new Label();
            txtAfterThresholding = new Label();
            btnDownload = new Button();
            txtInfo = new Label();
            panel_thresholding = new Panel();
            richTextBox_Info = new RichTextBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            panel_settings = new Panel();
            radioASMLib = new RadioButton();
            radioCLib = new RadioButton();
            label2 = new Label();
            btn_thresholding = new Button();
            btn_settings = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox_before).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_after).BeginInit();
            panel_thresholding.SuspendLayout();
            panel_settings.SuspendLayout();
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
            btn_addFile.Location = new Point(2, 2);
            btn_addFile.Margin = new Padding(2);
            btn_addFile.Name = "btn_addFile";
            btn_addFile.Size = new Size(200, 40);
            btn_addFile.TabIndex = 0;
            btn_addFile.Text = "Select file";
            btn_addFile.UseVisualStyleBackColor = false;
            btn_addFile.Click += selectFile;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // pictureBox_before
            // 
            pictureBox_before.BorderStyle = BorderStyle.FixedSingle;
            pictureBox_before.Image = Properties.Resources.undraw_images;
            pictureBox_before.Location = new Point(236, 90);
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
            pictureBox_after.Location = new Point(555, 90);
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
            txtSelectedFile.Location = new Point(236, 63);
            txtSelectedFile.Name = "txtSelectedFile";
            txtSelectedFile.Size = new Size(120, 17);
            txtSelectedFile.TabIndex = 3;
            txtSelectedFile.Text = "Selected file:";
            // 
            // txtAfterThresholding
            // 
            txtAfterThresholding.AutoSize = true;
            txtAfterThresholding.Font = new Font("Consolas", 10F, FontStyle.Regular, GraphicsUnit.Point);
            txtAfterThresholding.Location = new Point(555, 63);
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
            btnDownload.Location = new Point(609, 357);
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
            txtInfo.Location = new Point(2, 63);
            txtInfo.Name = "txtInfo";
            txtInfo.Size = new Size(192, 17);
            txtInfo.TabIndex = 6;
            txtInfo.Text = "Information about file:";
            // 
            // panel_thresholding
            // 
            panel_thresholding.Controls.Add(richTextBox_Info);
            panel_thresholding.Controls.Add(btn_addFile);
            panel_thresholding.Controls.Add(pictureBox_before);
            panel_thresholding.Controls.Add(txtInfo);
            panel_thresholding.Controls.Add(pictureBox_after);
            panel_thresholding.Controls.Add(btnDownload);
            panel_thresholding.Controls.Add(txtSelectedFile);
            panel_thresholding.Controls.Add(txtAfterThresholding);
            panel_thresholding.Location = new Point(12, 70);
            panel_thresholding.Name = "panel_thresholding";
            panel_thresholding.Size = new Size(858, 418);
            panel_thresholding.TabIndex = 8;
            // 
            // richTextBox_Info
            // 
            richTextBox_Info.BackColor = Color.White;
            richTextBox_Info.BorderStyle = BorderStyle.None;
            richTextBox_Info.Enabled = false;
            richTextBox_Info.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            richTextBox_Info.Location = new Point(0, 90);
            richTextBox_Info.Name = "richTextBox_Info";
            richTextBox_Info.ReadOnly = true;
            richTextBox_Info.Size = new Size(202, 250);
            richTextBox_Info.TabIndex = 7;
            richTextBox_Info.Text = "Select file to see informations...";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // panel_settings
            // 
            panel_settings.Controls.Add(radioASMLib);
            panel_settings.Controls.Add(radioCLib);
            panel_settings.Controls.Add(label2);
            panel_settings.Location = new Point(444, 70);
            panel_settings.Name = "panel_settings";
            panel_settings.Size = new Size(426, 418);
            panel_settings.TabIndex = 10;
            panel_settings.Visible = false;
            // 
            // radioASMLib
            // 
            radioASMLib.AutoSize = true;
            radioASMLib.Checked = true;
            radioASMLib.Location = new Point(206, 83);
            radioASMLib.Name = "radioASMLib";
            radioASMLib.Size = new Size(50, 19);
            radioASMLib.TabIndex = 6;
            radioASMLib.TabStop = true;
            radioASMLib.Text = "ASM";
            radioASMLib.UseVisualStyleBackColor = true;
            // 
            // radioCLib
            // 
            radioCLib.AutoSize = true;
            radioCLib.Location = new Point(151, 83);
            radioCLib.Name = "radioCLib";
            radioCLib.Size = new Size(40, 19);
            radioCLib.TabIndex = 5;
            radioCLib.Text = "C#";
            radioCLib.UseVisualStyleBackColor = true;
            radioCLib.CheckedChanged += radioCLib_CheckedChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Consolas", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(163, 25);
            label2.Name = "label2";
            label2.Size = new Size(72, 17);
            label2.TabIndex = 4;
            label2.Text = "Settings";
            // 
            // btn_thresholding
            // 
            btn_thresholding.BackColor = Color.Black;
            btn_thresholding.Cursor = Cursors.Hand;
            btn_thresholding.FlatAppearance.BorderColor = Color.Black;
            btn_thresholding.FlatAppearance.BorderSize = 0;
            btn_thresholding.FlatStyle = FlatStyle.Flat;
            btn_thresholding.Font = new Font("Consolas", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btn_thresholding.ForeColor = Color.White;
            btn_thresholding.Location = new Point(14, 11);
            btn_thresholding.Margin = new Padding(2);
            btn_thresholding.Name = "btn_thresholding";
            btn_thresholding.Size = new Size(200, 40);
            btn_thresholding.TabIndex = 11;
            btn_thresholding.Text = "Thresholding";
            btn_thresholding.UseVisualStyleBackColor = false;
            btn_thresholding.Click += btn_thresholding_Click;
            // 
            // btn_settings
            // 
            btn_settings.BackColor = Color.Black;
            btn_settings.Cursor = Cursors.Hand;
            btn_settings.FlatAppearance.BorderColor = Color.Black;
            btn_settings.FlatAppearance.BorderSize = 0;
            btn_settings.FlatStyle = FlatStyle.Flat;
            btn_settings.Font = new Font("Consolas", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btn_settings.ForeColor = Color.White;
            btn_settings.Location = new Point(218, 11);
            btn_settings.Margin = new Padding(2);
            btn_settings.Name = "btn_settings";
            btn_settings.Size = new Size(200, 40);
            btn_settings.TabIndex = 12;
            btn_settings.Text = "Settings && Info";
            btn_settings.UseVisualStyleBackColor = false;
            btn_settings.Click += btn_settings_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(884, 501);
            Controls.Add(btn_settings);
            Controls.Add(btn_thresholding);
            Controls.Add(panel_settings);
            Controls.Add(panel_thresholding);
            Margin = new Padding(2);
            Name = "Form1";
            Text = "ThresholdingApp";
            ((System.ComponentModel.ISupportInitialize)pictureBox_before).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_after).EndInit();
            panel_thresholding.ResumeLayout(false);
            panel_thresholding.PerformLayout();
            panel_settings.ResumeLayout(false);
            panel_settings.PerformLayout();
            ResumeLayout(false);
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
        private Panel panel_thresholding;
        private RichTextBox richTextBox_Info;
        private ContextMenuStrip contextMenuStrip1;
        private Panel panel_settings;
        private Button btn_thresholding;
        private Button btn_settings;
        private RadioButton radioASMLib;
        private RadioButton radioCLib;
        private Label label2;
    }
}