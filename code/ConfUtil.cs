using System.Collections.Generic;
using System.IO;

namespace DungeonPapperWPF.code
{
    public class ConfUtil
    {

        public  static void save(string key, string value)
        {
            Dictionary<string, string> props = read();

            if (props.ContainsKey(key))
            {
                props[key] = value;
            }
            else
            {
                props.Add(key, value);
            }

            string v = "";
            foreach (var propsValue in props)
            {
                v+=propsValue.Key+"="+propsValue.Value+"\n";
            }
            File.WriteAllText("user.conf", v);
            
        }

        public static Dictionary<string, string> read()
        {
            Dictionary<string, string> props = new Dictionary<string, string>();

            if (File.Exists("user.conf"))
            {
                foreach (string readLine in File.ReadAllLines("user.conf"))
                {
                    props.Add(readLine.Split("=".ToCharArray())[0], readLine.Split("=".ToCharArray())[1]);
                }
            }
         

            return props;
        }
    }
}
