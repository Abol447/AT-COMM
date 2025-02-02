using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AT_COMMEND
{
    internal class fastbootCommend
    {
        private string fileName;
        private string output;
        public fastbootCommend(string fileName)
        {
            this.fileName = fileName;
        }
        public void process(string commend)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                FileName = fileName,
                Arguments = commend,
                RedirectStandardOutput = true,
            };
            Process process = new Process { StartInfo = psi };
            process.Start();
            output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
        }
        public string result()
        {
            return output;
        }

    }
}
