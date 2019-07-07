using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mac2000CertificatesConvertor
{
    public class ProgressBarEventArgs :EventArgs
    {
        public int StepsToDo { get; set; }
    }
}
