using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;

namespace Mac2000CertificatesConvertor
{
    public partial class MainForm : Form
    {
        Logger logger;
        ConfigurationManager cm;
        Settings config;
        bool errorState = false;
        readonly string configFile = "config.bin";
        CertificateProcessor cf;
        public MainForm()
        {
            InitializeComponent();
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var settingsForm = new SettingsForm();
            settingsForm.ShowDialog();
            config = cm.GetSettings(configFile);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            cm = new ConfigurationManager();
            
            config = cm.GetSettings(configFile);
            try
            {
                lblOutPutFolder.Text = config.SaveDirectory.ToString();

            }
            catch (Exception)
            {

                lblOutPutFolder.Text = "Some error occured , please check settings";
                errorState = true;
            }
        }

        private void MainForm_MouseEnter(object sender, EventArgs e)
        {
            if (errorState)
            {
                config = cm.GetSettings(configFile);
                lblOutPutFolder.Text = config.SaveDirectory.ToString();

            }
        }

        private void Panel1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Link;
        }

        private void Panel1_DragDrop(object sender, DragEventArgs e)
        {


            Thread t = new Thread(() => { HandleDragDrop(e); });
            t.Start();


        }

        private void HandleDragDrop(DragEventArgs e)
        {
            try
            {
                if (txtInputPassowrd.Text == string.Empty || txtInputPassowrd.Text == null)
                {
                    MessageBox.Show("Please set input password ... ");
                    return;
                }
                if (txtOutputPrivateKeyPassword.Text.Length < 4 && txtOutputPrivateKeyPassword.Text != string.Empty)
                {
                    MessageBox.Show("Output password must be more then longer then 4 digits");
                    return;

                }


                string[] files = new string[1];
                e.Effect = DragDropEffects.Move;
                var d = e.Data.GetData("string", true);
                string[] format = e.Data.GetFormats();
                files = e.Data.GetData(DataFormats.FileDrop, true) as string[];
                e.Effect = DragDropEffects.None;
                cf = new CertificateProcessor(files, config);
                cf.PreformStep += Cf_PreformStep;

                var certificates = cf.GetCertificatesPaths();
                //foreach (var item in certificates)
                //{
                //    CreateDirectory(item);
                //}
                progressBar1.Maximum = 100;
                progressBar1.Style = ProgressBarStyle.Continuous;
                int numberOfSteps = certificates.Count;
                UpdateProgressBar(1);
                foreach (var item in certificates)
                {
                    cf.ProcessOneCertificate(config, item, txtInputPassowrd.Text, txtOutputPrivateKeyPassword.Text);
                    UpdateProgressBar(100 / numberOfSteps);
                }
                ResetProgressBar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Cf_PreformStep(object sender, EventArgs e)
        {
            var args = e as ProgressBarEventArgs;
            UpdateProgressBar(args.StepsToDo);
        }

        private void CreateDirectory(string pFXpath)
        {
            string OutPutDirectory = config.SaveDirectory.ToString();
            string CertificateDirectory = Path.GetFileNameWithoutExtension(pFXpath);
            if (config.CreateFolderForCertificates)
            {
                OutPutDirectory += @"\" + CertificateDirectory;
            }
            try
            {
                //Process p = new Process();
                //ProcessStartInfo psi = new ProcessStartInfo("cmd.exe",$"md {OutPutDirectory}");
                //p.StartInfo = psi;
                //p.Start();

                var di = Directory.CreateDirectory(OutPutDirectory);
                di.Refresh();
            }
            catch (Exception)
            {
                throw new DirectoryNotFoundException($"The Directory {OutPutDirectory} does not exsist and cannot be created.\r\n please make sure to run the application as Administrator\r\n" +
                    $"or create the directory manually");

            }
            Thread.Sleep(1500);
        }

        private void UpdateProgressBar(int Steps)
        {
            if (progressBar1.InvokeRequired)
            {
                progressBar1.Invoke(new Action(()=> { UpdateProgressBar(Steps); }));
            }
            else
            {
                progressBar1.Visible = true;
                progressBar1.Step = 1;
                for (int i = 0; i < Steps; i++)
                {
                    progressBar1.PerformStep();
                }
                
            }
        }
        void ResetProgressBar()
        {
            if (progressBar1.InvokeRequired)
            {
                progressBar1.Invoke(new Action(() => { ResetProgressBar(); }));
            }
            else
            {
                progressBar1.Value = 0;
                progressBar1.Visible = false;

            }
        }
    }
}
