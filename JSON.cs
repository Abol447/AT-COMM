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
        private string[] input;
        private Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
        public JSON(string x)
        {
            input = x.Split(";");
            Insert();
        }
        private string textinside(string txt)
        {
            string x = "";
            string patternInside = @"\(([^)]+)\)";
            Regex regex = new Regex(patternInside);
            MatchCollection match = regex.Matches(txt);
            foreach (Match m in match)
            {
                x = m.Groups[1].Value;
            }
            return x;
        }
        public Dictionary<string,string> Insert() {
            for (int i = 0; i < input.Length; i++)
                for (int j = 0; j < key.Length; j++)
                    if(input[i].Contains(key[j]))
                        keyValuePairs.TryAdd(key[j], textinside(input[i]));
            return keyValuePairs;
        }
        public void saveJsonFile(string path)
        {
            string js =  JsonConvert.SerializeObject(keyValuePairs, Formatting.Indented);
            File.WriteAllText(path, js);
        }
    }
}
