using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace ComputersControlDLL
{
    public class FileHelper
    {
        private const string LIST_FILENAME = "CompInfo.txt";

        private static readonly string LIST_FILENAME_PATH = Directory.GetCurrentDirectory() + "//" + LIST_FILENAME;
        public static bool TryGetComputersList(out List<ComputerInfo> computerInfo)
        {
            if (File.Exists(LIST_FILENAME_PATH))
            {
                string json = File.ReadAllText(LIST_FILENAME_PATH);
                computerInfo = JsonConvert.DeserializeObject<List<ComputerInfo>>(json);
                return true;
            }

            computerInfo = null;
            return false;
        }
        public static void SaveComputersList(List<ComputerInfo> computerInfo)
        {
            string json = JsonConvert.SerializeObject(computerInfo, Formatting.Indented);
            File.WriteAllText(LIST_FILENAME_PATH, json);
        }
    }
}
