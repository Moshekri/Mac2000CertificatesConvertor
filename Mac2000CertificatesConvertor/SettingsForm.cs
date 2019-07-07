using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Mac2000CertificatesConvertor
{
    public partial class SettingsForm : Form
    {
        ConfigurationManager cm;
        readonly private string configFileName = "config.bin";
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            cm = new ConfigurationManager();
            var config = cm.GetSettings(configFileName);
            try
            {
                txtCertificatesOutPut.Text = config.SaveDirectory.ToString();

            }
            catch (Exception)
            {

            }
            try
            {
                txtOpenSSLFolder.Text = config.OpenSSLLocation.ToString();

            }
            catch (Exception)
            {

            }
            chkSplitCA.Checked = config.SplitCACertsIfMoreThenOneExists;
            chkCreateDifferentFolderForCertificates.Checked = config.CreateFolderForCertificates;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            var config = new Settings();

            if (Directory.Exists(txtCertificatesOutPut.Text))
            {
                config.SaveDirectory = new System.IO.DirectoryInfo(txtCertificatesOutPut.Text);
            }
            else
            {
                ShowBadFolderError(txtCertificatesOutPut.Text);
                return;
            }

            if (Directory.Exists(txtOpenSSLFolder.Text))
            {
                config.OpenSSLLocation = new System.IO.DirectoryInfo(txtOpenSSLFolder.Text);
            }
            else
            {
                ShowBadFolderError(txtOpenSSLFolder.Text);
                return;
            }

            config.SplitCACertsIfMoreThenOneExists = chkSplitCA.Checked;
            config.CreateFolderForCertificates = chkCreateDifferentFolderForCertificates.Checked;
            cm.SaveSettings(config);
            MessageBox.Show("Settings Saved Successfully","Success",MessageBoxButtons.OK);
            this.Close();
        }

       
        private void ShowBadFolderError(string text)
        {
            MessageBox.Show($"The Chosen Directory \"{text}\" does not exist ! " +
                                $"\r\n Please Choose a valid Directory .  ", "Bad Folder Selection ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSelectOpenSSlLocation_Click(object sender, EventArgs e)
        {
            string res =  SelectFolder();
            txtOpenSSLFolder.Text = res == null ? txtOpenSSLFolder.Text : res;
        }

        private string SelectFolder()
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                return folderBrowserDialog1.SelectedPath;
            }
            else
                return null;
        }

        private void BtnSelectOutputLocation_Click(object sender, EventArgs e)
        {
            string res = SelectFolder();
            txtCertificatesOutPut.Text = res == null ? txtCertificatesOutPut.Text : res;
        }
    }
}
