namespace SDImager
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblModel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblFormat = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblVolume = new System.Windows.Forms.Label();
            this.lblPartition = new System.Windows.Forms.Label();
            this.lblInterfaceType = new System.Windows.Forms.Label();
            this.lblPhysicalDriveSize = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lstDiskDrive = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnChooseFile = new System.Windows.Forms.Button();
            this.txtFilename = new System.Windows.Forms.TextBox();
            this.ofdFilename = new System.Windows.Forms.OpenFileDialog();
            this.btnRead = new System.Windows.Forms.Button();
            this.progress = new System.Windows.Forms.ProgressBar();
            this.btnStop = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblTimeRemaining = new System.Windows.Forms.Label();
            this.lblByteRemaining = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnWrite = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLblInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLblHome = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblModel);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.lblFormat);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblVolume);
            this.groupBox1.Controls.Add(this.lblPartition);
            this.groupBox1.Controls.Add(this.lblInterfaceType);
            this.groupBox1.Controls.Add(this.lblPhysicalDriveSize);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lstDiskDrive);
            this.groupBox1.Location = new System.Drawing.Point(155, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(380, 208);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Disk drive";
            // 
            // lblModel
            // 
            this.lblModel.AutoSize = true;
            this.lblModel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModel.Location = new System.Drawing.Point(140, 164);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(25, 13);
            this.lblModel.TabIndex = 12;
            this.lblModel.Text = "???";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.DimGray;
            this.label9.Location = new System.Drawing.Point(10, 164);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "Model:";
            // 
            // lblFormat
            // 
            this.lblFormat.AutoSize = true;
            this.lblFormat.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormat.Location = new System.Drawing.Point(140, 84);
            this.lblFormat.Name = "lblFormat";
            this.lblFormat.Size = new System.Drawing.Size(25, 13);
            this.lblFormat.TabIndex = 10;
            this.lblFormat.Text = "???";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(10, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Format(s):";
            // 
            // lblVolume
            // 
            this.lblVolume.AutoSize = true;
            this.lblVolume.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVolume.Location = new System.Drawing.Point(140, 64);
            this.lblVolume.Name = "lblVolume";
            this.lblVolume.Size = new System.Drawing.Size(25, 13);
            this.lblVolume.TabIndex = 8;
            this.lblVolume.Text = "???";
            // 
            // lblPartition
            // 
            this.lblPartition.AutoEllipsis = true;
            this.lblPartition.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPartition.Location = new System.Drawing.Point(140, 104);
            this.lblPartition.Name = "lblPartition";
            this.lblPartition.Size = new System.Drawing.Size(230, 13);
            this.lblPartition.TabIndex = 7;
            this.lblPartition.Text = "???";
            // 
            // lblInterfaceType
            // 
            this.lblInterfaceType.AutoSize = true;
            this.lblInterfaceType.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInterfaceType.Location = new System.Drawing.Point(140, 124);
            this.lblInterfaceType.Name = "lblInterfaceType";
            this.lblInterfaceType.Size = new System.Drawing.Size(25, 13);
            this.lblInterfaceType.TabIndex = 6;
            this.lblInterfaceType.Text = "???";
            // 
            // lblPhysicalDriveSize
            // 
            this.lblPhysicalDriveSize.AutoSize = true;
            this.lblPhysicalDriveSize.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhysicalDriveSize.Location = new System.Drawing.Point(140, 144);
            this.lblPhysicalDriveSize.Name = "lblPhysicalDriveSize";
            this.lblPhysicalDriveSize.Size = new System.Drawing.Size(25, 13);
            this.lblPhysicalDriveSize.TabIndex = 5;
            this.lblPhysicalDriveSize.Text = "???";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(10, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Physical drive size:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(10, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Interface type:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(10, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Partition(s):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(10, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Volume(s):";
            // 
            // lstDiskDrive
            // 
            this.lstDiskDrive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lstDiskDrive.FormattingEnabled = true;
            this.lstDiskDrive.Location = new System.Drawing.Point(10, 29);
            this.lstDiskDrive.Name = "lstDiskDrive";
            this.lstDiskDrive.Size = new System.Drawing.Size(360, 21);
            this.lstDiskDrive.TabIndex = 0;
            this.lstDiskDrive.SelectedIndexChanged += new System.EventHandler(this.lstDiskDrive_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnChooseFile);
            this.groupBox2.Controls.Add(this.txtFilename);
            this.groupBox2.Location = new System.Drawing.Point(155, 248);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(380, 95);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Image file";
            // 
            // btnChooseFile
            // 
            this.btnChooseFile.Image = ((System.Drawing.Image)(resources.GetObject("btnChooseFile.Image")));
            this.btnChooseFile.Location = new System.Drawing.Point(265, 54);
            this.btnChooseFile.Name = "btnChooseFile";
            this.btnChooseFile.Size = new System.Drawing.Size(105, 30);
            this.btnChooseFile.TabIndex = 1;
            this.btnChooseFile.Text = "Browse";
            this.btnChooseFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnChooseFile.UseVisualStyleBackColor = true;
            this.btnChooseFile.Click += new System.EventHandler(this.btnChooseFile_Click);
            // 
            // txtFilename
            // 
            this.txtFilename.AllowDrop = true;
            this.txtFilename.Location = new System.Drawing.Point(10, 27);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.Size = new System.Drawing.Size(360, 21);
            this.txtFilename.TabIndex = 0;
            // 
            // ofdFilename
            // 
            this.ofdFilename.CheckFileExists = false;
            this.ofdFilename.Filter = "Image files|*.img|All files|*.*";
            // 
            // btnRead
            // 
            this.btnRead.Enabled = false;
            this.btnRead.Image = ((System.Drawing.Image)(resources.GetObject("btnRead.Image")));
            this.btnRead.Location = new System.Drawing.Point(10, 20);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(105, 65);
            this.btnRead.TabIndex = 2;
            this.btnRead.Text = "Read";
            this.btnRead.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRead.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // progress
            // 
            this.progress.Location = new System.Drawing.Point(10, 20);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(360, 23);
            this.progress.TabIndex = 3;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(265, 86);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(105, 30);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "Exit";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblSpeed);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.lblTimeRemaining);
            this.groupBox3.Controls.Add(this.btnStop);
            this.groupBox3.Controls.Add(this.lblByteRemaining);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.progress);
            this.groupBox3.Location = new System.Drawing.Point(155, 350);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(380, 128);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Progress";
            // 
            // lblSpeed
            // 
            this.lblSpeed.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSpeed.Location = new System.Drawing.Point(140, 55);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(119, 15);
            this.lblSpeed.TabIndex = 9;
            this.lblSpeed.Text = "???";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DimGray;
            this.label8.Location = new System.Drawing.Point(10, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Transfer speed:";
            // 
            // lblTimeRemaining
            // 
            this.lblTimeRemaining.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeRemaining.Location = new System.Drawing.Point(140, 95);
            this.lblTimeRemaining.Name = "lblTimeRemaining";
            this.lblTimeRemaining.Size = new System.Drawing.Size(119, 15);
            this.lblTimeRemaining.TabIndex = 7;
            this.lblTimeRemaining.Text = "???";
            // 
            // lblByteRemaining
            // 
            this.lblByteRemaining.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblByteRemaining.Location = new System.Drawing.Point(140, 75);
            this.lblByteRemaining.Name = "lblByteRemaining";
            this.lblByteRemaining.Size = new System.Drawing.Size(119, 15);
            this.lblByteRemaining.TabIndex = 6;
            this.lblByteRemaining.Text = "???";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DimGray;
            this.label7.Location = new System.Drawing.Point(10, 95);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Time remaining:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DimGray;
            this.label6.Location = new System.Drawing.Point(10, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Bytes remaining:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button2);
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Controls.Add(this.btnWrite);
            this.groupBox4.Controls.Add(this.btnRead);
            this.groupBox4.Location = new System.Drawing.Point(12, 183);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(128, 295);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Process";
            // 
            // button2
            // 
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(10, 154);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(105, 65);
            this.button2.TabIndex = 8;
            this.button2.Text = "Format";
            this.button2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(10, 221);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 65);
            this.button1.TabIndex = 7;
            this.button1.Text = "Erase";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnWrite
            // 
            this.btnWrite.Enabled = false;
            this.btnWrite.Image = ((System.Drawing.Image)(resources.GetObject("btnWrite.Image")));
            this.btnWrite.Location = new System.Drawing.Point(10, 87);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(105, 65);
            this.btnWrite.TabIndex = 6;
            this.btnWrite.Text = "Write";
            this.btnWrite.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnWrite.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 30);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 128);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLblInfo,
            this.statusLblHome});
            this.statusStrip1.Location = new System.Drawing.Point(0, 489);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(549, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLblInfo
            // 
            this.statusLblInfo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLblInfo.Name = "statusLblInfo";
            this.statusLblInfo.Size = new System.Drawing.Size(208, 17);
            this.statusLblInfo.Text = "Please select an image file to start.";
            // 
            // statusLblHome
            // 
            this.statusLblHome.IsLink = true;
            this.statusLblHome.Name = "statusLblHome";
            this.statusLblHome.Size = new System.Drawing.Size(326, 17);
            this.statusLblHome.Spring = true;
            this.statusLblHome.Text = "www.rizonesoft.com";
            this.statusLblHome.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(549, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // frmMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 511);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SD Image";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox lstDiskDrive;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnChooseFile;
        private System.Windows.Forms.TextBox txtFilename;
        private System.Windows.Forms.OpenFileDialog ofdFilename;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.ProgressBar progress;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblVolume;
        private System.Windows.Forms.Label lblPartition;
        private System.Windows.Forms.Label lblInterfaceType;
        private System.Windows.Forms.Label lblPhysicalDriveSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFormat;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblTimeRemaining;
        private System.Windows.Forms.Label lblByteRemaining;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel statusLblInfo;
        private System.Windows.Forms.ToolStripStatusLabel statusLblHome;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}