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
        private static string? x;
        private static string? deviceid; 
        private static string? device;
        public static string? vid;
        public static string? pid;
        public VID()
        {
            x = "SELECT * FROM Win32_PnPEntity WHERE DeviceID LIKE 'USB%'";
            searcher();
        }
        private static void searcher()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(x);
            foreach (ManagementObject obj in searcher.Get())
            {
                deviceid = obj["DeviceID"].ToString();
                if (!string.IsNullOrEmpty(deviceid) && deviceid.Contains("vid") && deviceid.Contains("pid")) 
                {
                    device = getdevice();
                }
                getvidandpid();
            }

        }
        private static string getdevice()
        {
            string x = "";
            string patern = @"//(.*?)//";
            Regex regex = new Regex(patern);
            MatchCollection collection = regex.Matches(x);
            foreach (Match match in collection)
            {
                x = match.Groups[1].Value;
            }
            return x;
        }
        private static void getvidandpid()
        {
            string[] x = device.Split("&");
            foreach (string y in x)
            {
                if (y.Contains("vid"))
                    vid = getnumber(y);
                else if (y.Contains("pid"))
                    pid = getnumber(y);
            }
        }
        private static string getnumber(string a)
        {
            string x ="";
            string pattern = @"\d+";
            Regex regex = new Regex(pattern);
            foreach (Match match in Regex.Matches(a, pattern))
            {
                x = match.Value;
            }
            return x;
        }
        public static string getvid()
        {
            return vid;
        }
    }
}
