namespace Mac2000CertificatesConvertor
{
    partial class SettingsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOpenSSLFolder = new System.Windows.Forms.TextBox();
            this.txtCertificatesOutPut = new System.Windows.Forms.TextBox();
            this.btnSelectOpenSSlLocation = new System.Windows.Forms.Button();
            this.btnSelectOutputLocation = new System.Windows.Forms.Button();
            this.chkSplitCA = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.chkCreateDifferentFolderForCertificates = new System.Windows.Forms.CheckBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "(Optional) Open SSL Location";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Certificates Output Folder";
            // 
            // txtOpenSSLFolder
            // 
            this.txtOpenSSLFolder.Location = new System.Drawing.Point(51, 57);
            this.txtOpenSSLFolder.Name = "txtOpenSSLFolder";
            this.txtOpenSSLFolder.Size = new System.Drawing.Size(319, 20);
            this.txtOpenSSLFolder.TabIndex = 1;
            // 
            // txtCertificatesOutPut
            // 
            this.txtCertificatesOutPut.Location = new System.Drawing.Point(51, 139);
            this.txtCertificatesOutPut.Name = "txtCertificatesOutPut";
            this.txtCertificatesOutPut.Size = new System.Drawing.Size(319, 20);
            this.txtCertificatesOutPut.TabIndex = 1;
            // 
            // btnSelectOpenSSlLocation
            // 
            this.btnSelectOpenSSlLocation.Location = new System.Drawing.Point(412, 55);
            this.btnSelectOpenSSlLocation.Name = "btnSelectOpenSSlLocation";
            this.btnSelectOpenSSlLocation.Size = new System.Drawing.Size(75, 23);
            this.btnSelectOpenSSlLocation.TabIndex = 2;
            this.btnSelectOpenSSlLocation.Text = "Select";
            this.btnSelectOpenSSlLocation.UseVisualStyleBackColor = true;
            this.btnSelectOpenSSlLocation.Click += new System.EventHandler(this.BtnSelectOpenSSlLocation_Click);
            // 
            // btnSelectOutputLocation
            // 
            this.btnSelectOutputLocation.Location = new System.Drawing.Point(412, 137);
            this.btnSelectOutputLocation.Name = "btnSelectOutputLocation";
            this.btnSelectOutputLocation.Size = new System.Drawing.Size(75, 23);
            this.btnSelectOutputLocation.TabIndex = 2;
            this.btnSelectOutputLocation.Text = "Select";
            this.btnSelectOutputLocation.UseVisualStyleBackColor = true;
            this.btnSelectOutputLocation.Click += new System.EventHandler(this.BtnSelectOutputLocation_Click);
            // 
            // chkSplitCA
            // 
            this.chkSplitCA.AutoSize = true;
            this.chkSplitCA.Location = new System.Drawing.Point(33, 191);
            this.chkSplitCA.Name = "chkSplitCA";
            this.chkSplitCA.Size = new System.Drawing.Size(251, 17);
            this.chkSplitCA.TabIndex = 3;
            this.chkSplitCA.Text = "Split CA Certificats if more then one exists in pfx ";
            this.chkSplitCA.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(33, 262);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Save ";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(412, 262);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "Exit";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // chkCreateDifferentFolderForCertificates
            // 
            this.chkCreateDifferentFolderForCertificates.AutoSize = true;
            this.chkCreateDifferentFolderForCertificates.Location = new System.Drawing.Point(33, 214);
            this.chkCreateDifferentFolderForCertificates.Name = "chkCreateDifferentFolderForCertificates";
            this.chkCreateDifferentFolderForCertificates.Size = new System.Drawing.Size(152, 17);
            this.chkCreateDifferentFolderForCertificates.TabIndex = 6;
            this.chkCreateDifferentFolderForCertificates.Text = "Create Folder for certificats";
            this.chkCreateDifferentFolderForCertificates.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 318);
            this.Controls.Add(this.chkCreateDifferentFolderForCertificates);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.chkSplitCA);
            this.Controls.Add(this.btnSelectOutputLocation);
            this.Controls.Add(this.btnSelectOpenSSlLocation);
            this.Controls.Add(this.txtCertificatesOutPut);
            this.Controls.Add(this.txtOpenSSLFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOpenSSLFolder;
        private System.Windows.Forms.TextBox txtCertificatesOutPut;
        private System.Windows.Forms.Button btnSelectOpenSSlLocation;
        private System.Windows.Forms.Button btnSelectOutputLocation;
        private System.Windows.Forms.CheckBox chkSplitCA;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.CheckBox chkCreateDifferentFolderForCertificates;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}