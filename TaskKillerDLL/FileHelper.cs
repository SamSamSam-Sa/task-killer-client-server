using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace TaskKillerDLL
{
    public class FileHelper
    {
        private const string LIST_FILENAME = "CompInfo.txt";

        private static readonly string LIST_FILENAME_PATH = Directory.GetCurrentDirectory() + "//" + LIST_FILENAME;
        public static bool TryGetComputersList(out IEnumerable<ComputerInfo> computerInfo)
        {
            if (File.Exists(LIST_FILENAME_PATH))
            {
                string json = File.ReadAllText(LIST_FILENAME_PATH);
                computerInfo = JsonConvert.DeserializeObject<IEnumerable<ComputerInfo>>(json);
                return true;
            }

            computerInfo = null;
            return false;
        }
        public static void SaveComputersList(IEnumerable<ComputerInfo> computerInfo)
        {
            string json = JsonConvert.SerializeObject(computerInfo, Formatting.Indented);
            File.WriteAllText(LIST_FILENAME_PATH, json);
        }
    }
}
