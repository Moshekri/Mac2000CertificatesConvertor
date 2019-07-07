using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Mac2000CertificatesConvertor
{
    public class ConfigurationManager
    {
        readonly private string configFileName = "config.bin";
        public void CreateDefaultConfigFile()
        {
           // string outputFolderName = "Certificates";

            //string desktop = Path.Combine( Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString(), outputFolderName).ToString(); ;
            //string outputPath = Path.Combine(desktop, outputFolderName).ToString();

            String DefaultFolder = @"c:\GeneratedCertificates";
            string outputPath = DefaultFolder;
            string currentFolder = Environment.CurrentDirectory.ToString();
            string openSSLFolder = currentFolder + @"\openssl";


            Settings config = new Settings();
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
            config.SaveDirectory = new DirectoryInfo(outputPath);
            config.OpenSSLLocation = new DirectoryInfo(openSSLFolder);
            config.SplitCACertsIfMoreThenOneExists = true;
            config.CreateFolderForCertificates = true;
            SaveSettings(config);

           
        }
        public void SaveSettings(Settings config)
        {
            try
            {
                using (var fs = File.OpenWrite(configFileName))
                {
                    var bf = new BinaryFormatter();
                    bf.Serialize(fs, config);
                }
            }
            catch (Exception ex)
            {
                throw ex;               
            }
            
        }
        public Settings GetSettings(string filePath)
        {
            if (!File.Exists(filePath))
            {
                CreateDefaultConfigFile();
            }
            using (var fs = File.OpenRead(filePath))
            {
                BinaryFormatter bf = new BinaryFormatter();
                var config = bf.Deserialize(fs) as Settings;
                return config;
            }
        }
    }
}
