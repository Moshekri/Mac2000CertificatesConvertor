using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mac2000CertificatesConvertor
{
    [Serializable]
    public class Settings
    {
        public DirectoryInfo OpenSSLLocation { get; set; }
        public DirectoryInfo SaveDirectory { get; set; }
        public bool SplitCACertsIfMoreThenOneExists { get; set; }
        public bool CreateFolderForCertificates { get; set; }

    }
}
