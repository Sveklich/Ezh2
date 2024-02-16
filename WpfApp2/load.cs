using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public static class Load
    {
        public static void Serialize(Dictionary<DateTime, List<todo>> todo, string fileName)
        {
            string json = JsonConvert.SerializeObject(todo);
            File.WriteAllText(fileName, json);
        }

        public static Dictionary<DateTime, List<todo>> Deserialize(string fileName)
        {
            string json = File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject<Dictionary<DateTime, List<todo>>>(json);

        }
    }
}
