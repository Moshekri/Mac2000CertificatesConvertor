using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace Mac2000CertificatesConvertor
{
    public class CertificateProcessor
    {
        Settings config;
        string OutPutDirectory;
        string CertificateDirectory;
        public event EventHandler PreformStep;

        List<string> certificates = new List<string>();
        public CertificateProcessor(string[] FilePaths, Settings config)
        {
            this.config = config;
            this.OutPutDirectory = config.SaveDirectory.ToString();
            foreach (var item in FilePaths)
            {
                if (item.ToLower().Contains(".pfx"))
                {
                    certificates.Add(item);
                }
            }
        }
        public List<string> GetCertificatesPaths()
        {
            return certificates;
        }

        public void ProcessOneCertificate(Settings config, string PFXpath, string inputPassword, string outputPassword)
        {
            CreateDirectory(PFXpath);
            OutPutDirectory = config.SaveDirectory.ToString();
            CertificateDirectory = Path.GetFileNameWithoutExtension(PFXpath);
            if (config.CreateFolderForCertificates)
            {
                OutPutDirectory += @"\" + CertificateDirectory;
            }
            CreateMainCACertificate(config, inputPassword, PFXpath);
            SplitCACertificates(config);
            CreatePublicKey(config, inputPassword, PFXpath);
            CreatePVKPrivateKey(config, PFXpath, inputPassword, outputPassword);

            
        }

        private void CreateDirectory(string pFXpath)
        {
            OutPutDirectory = config.SaveDirectory.ToString();
            CertificateDirectory = Path.GetFileNameWithoutExtension(pFXpath);
            if (config.CreateFolderForCertificates)
            {
                OutPutDirectory += @"\" + CertificateDirectory;
            }
            try
            {
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

        private void CreatePVKPrivateKey(Settings config, string pFXpath, string inputPassword, string outputPassword)
        {
            if (outputPassword == string.Empty || outputPassword.Length < 5)
            {
                outputPassword = inputPassword;
            }
            string command = config.OpenSSLLocation.ToString() + @"\openssl.exe";
            string args = @" pkcs12 -in " + pFXpath + " -nocerts -nodes -passin pass:" + inputPassword + " -out " + OutPutDirectory + @"\temp.pem";

            Process p = new Process();
            ProcessStartInfo psi = new ProcessStartInfo(command, args);
            psi.CreateNoWindow = true;
            p.StartInfo = psi;
            p.Start();

            p.Close();
            Thread.Sleep(2000);
            string[] Temp = File.ReadAllLines(OutPutDirectory + @"\temp.pem");

            var data = CleanCertificates(config, Temp);
            if (data[0] == null)
            {
                return;
            }
            File.WriteAllText(OutPutDirectory + @"\temp.pem", data[0]);

            args = @" rsa -in " + OutPutDirectory + @"\temp.pem" + " -outform PVK -pvk-strong -passout pass:" + outputPassword + " -out " + OutPutDirectory + @"\Client_Private_Key.pvk";
            p = new Process();
            psi = new ProcessStartInfo(command, args);

            p.StartInfo = psi;
            p.Start();
            Thread.Sleep(1000);
            try
            {
            File.Delete(OutPutDirectory + @"\temp.pem");
            }
            catch (Exception)
            {
                throw new IOException($"Unable to remove file {OutPutDirectory + @"\temp.pem"} Please Delete it Manually");
            }
        }

        private void CreatePublicKey(Settings config, string inputPassword, string pFXpath)
        {
            string command = config.OpenSSLLocation.ToString() + @"\openssl.exe";
            string args = @" pkcs12 -in " + pFXpath + " -nokeys -clcerts -passin pass:" + inputPassword + " -out " + OutPutDirectory + @"\Client_Public_Key.cer";
            string comp = command + args;
            var p = Process.Start(command, args);
            Thread.Sleep(500);
            var cleanPublicKey = CleanCertificates(config, File.ReadAllLines(OutPutDirectory + @"\Client_Public_Key.cer"));
            File.WriteAllText(OutPutDirectory + @"\Client_Public_Key.cer", cleanPublicKey[0]);
          
        }

        private void CreateMainCACertificate(Settings config, string inputPassword, string pfxPath)
        {
            //create the CA Autority certificate
            string command = config.OpenSSLLocation.ToString() + @"\openssl.exe";
            string args = @" pkcs12 -in " + pfxPath + " -nokeys -cacerts -passin pass:" + inputPassword + " -out " + OutPutDirectory + @"\MainCA.cer";
            string comp = command + args;
            var p = Process.Start(command, args);
            

        }

        private void SplitCACertificates(Settings config)
        {
            //Read the file
            Thread.Sleep(1500);
            var CALines = File.ReadAllLines(OutPutDirectory + @"\MainCA.cer");

            //if settings allow it - chek if there is more then one CA Certificate inside and split them to different files
            if (config.SplitCACertsIfMoreThenOneExists)
            {
                int counter = 1;
                var cleanedCertificates = CleanCertificates(config, CALines);
                foreach (var item in cleanedCertificates)
                {
                    string filename = "Ca_" + counter.ToString() + ".cer";
                    File.WriteAllText(Path.Combine(OutPutDirectory, filename), item);
                    counter++;
                }
            }
        }

        private List<string> CleanCertificates(Settings config, string[] cALines)
        {
            List<string> data = new List<string>();
            StringBuilder sb = new StringBuilder();
            bool startOfCertificate = false;
            foreach (var line in cALines)
            {
                if (line.Contains("END CERTIFICATE") || line.Contains("END PRIVATE KEY"))
                {
                    startOfCertificate = false;
                    sb.AppendLine(line);
                    data.Add(sb.ToString());
                    sb.Clear();
                }
                if (startOfCertificate)
                {
                    sb.AppendLine(line);
                }
                else if (line.Contains("BEGIN CERTIFICATE") || line.Contains("BEGIN PRIVATE KEY"))
                {
                    startOfCertificate = true;
                    sb.AppendLine(line);
                }
            }
            return data;
        }

    }
}
