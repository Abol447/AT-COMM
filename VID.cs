using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AT_COMMEND
{
    internal class VID
    {
        private static string? deviceid; 
        private static string? device;
        public static string? vid;
        public static string? pid;
        public VID()
        {
            string query = "SELECT * FROM Win32_PnPEntity WHERE DeviceID LIKE 'USB%'";
            searcher( query);
        }
        private static void searcher(string query)
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            foreach (ManagementObject obj in searcher.Get())
            {
                deviceid = obj["DeviceID"].ToString();
                if (!string.IsNullOrEmpty(deviceid) && deviceid.Contains("VID_") && deviceid.Contains("PID_")) 
                {
                    device = getdevice(deviceid);
                    break;
                } 
            }
            getvidandpid(device);
        }
        private static string getdevice(string input)
        {
            string x = "";
            string patern = @"VID_\w+&PID_\w+";
            Regex regex = new Regex(patern);
            Match match = regex.Match(input);
            x = match.Value;
            return x;
        }
        private static void getvidandpid(string input)
        {
            string[] x = input.Split("&");
            foreach (string y in x)
            {
                if (y.Contains("VID_"))
                    vid = getnumber(y);
                else if (y.Contains("PID_"))
                    pid = getnumber(y);
            }
        }
        private static string getnumber(string a)
        {
            return a.Substring(4, 4);
        }
        public  string getvid()
        {
            return vid;
        }
        public string getpid()
        {
            return pid;
        }
    }
}
