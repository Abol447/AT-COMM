using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AT_COMMEND
{
    internal class JSON
    {
        private string[] key = new string[] {"MN","BASE","VER","HIDVER","MNC",
                                             "MCC","PRD","AID","CC","OMCCODE","SN",
                                             "IMEI","UN","PN","CON","LOCK","LIMIT",
                                             "SDP","HVID"};
        private string[] key2 = new string[] { "MODEL", "UN", "CAPA", "VENDOR", "FWVER",
                                               "PRODUCT","PROV","SALES","VER","DID","TMU_TEMP"};
        private string[] input;
        private string mode;
        private Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
        public JSON(string x,string mode)
        {
            this.mode = mode;
            input = x.Split(";");
            Insert();
        }
        private string textinside(string txt)
        {
            string x = "";
            string patternInside ="";
            if (mode == "MTP")
                patternInside = @"\(([^)]+)\)";
            else if (mode == "Download Mode")
                patternInside = @"=(.*)";
            Regex regex = new Regex(patternInside);
            MatchCollection match = regex.Matches(txt);
            foreach (Match m in match)
            {
                x = m.Groups[1].Value;
            }
            return x;
        }
        public Dictionary<string,string> Insert() {
            int count = 0;
            if (mode == "MTP")
                count = key.Length;
            else if (mode == "Download Mode")
                count = key2.Length;
            for (int i = 0; i < input.Length; i++)
                for (int j = 0; j < count; j++)
                    if (mode == "MTP")
                    {
                        if (input[i].Contains(key[j]))
                        {
                            keyValuePairs.TryAdd(key[j], textinside(input[i]));
                            break;
                        }
                    }
                    else if (mode == "Download Mode")
                    {
                        if (input[i].Contains(key2[j]))
                        {
                            keyValuePairs.TryAdd(key[j], textinside(input[i]));
                            break;
                        }
                    }
            return keyValuePairs;
        }
        public void saveJsonFile(string path)
        {
            string js =  JsonConvert.SerializeObject(keyValuePairs, Formatting.Indented);
            File.WriteAllText(path, js);
        }
        public void addtojson(string newKey ,string newvalue)
        {
            keyValuePairs.TryAdd(newKey, newvalue);
        }
    }
}
