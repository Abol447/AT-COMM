using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;
using AT_COMMEND;

namespace AT_COMMEND
{
    internal class AT
    {
        private static SerialPort sp = new SerialPort();
        private static string result ="";
        public AT()
        {
            portname();
            sp.Open();
            sp.DataReceived += Sp_DataReceived;
        }

        private void Sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            result = sp.ReadExisting();
        }
        public  string getresult()
        {
            return result;
        } 
        private static void portname()
        {
            foreach (string x in SerialPort.GetPortNames())
            {
                if(x != null)
                {
                    sp.PortName = x;
                }
            }
        }
        public void sendCommend(string commend)
        {
            sp.WriteLine(commend + "\r");
            Thread.Sleep(1000);
        }
    }
}
