using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System;

namespace TaskKillerDLL
{
    public class WebService
    {
        static HttpClient client;

        static WebService()
        {
            client = new HttpClient();
        }

        public static async Task<IEnumerable<TaskInfo>> GetTasksAsync(string path)
        {
            List<TaskInfo> tasksInfo = null;
            HttpResponseMessage response = await client.GetAsync(path).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                tasksInfo = JsonConvert.DeserializeObject<List<TaskInfo>>(data);
            }
            return tasksInfo;
        }
        public static bool AskToManageTask(string path, TaskInfo taskInfo)
        {
            var isProcessKilled = false;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(path);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST"; 

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(taskInfo);
                streamWriter.Write(json);
            }

            var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                isProcessKilled = Convert.ToBoolean(streamReader.ReadToEnd());
            }
            return isProcessKilled;
        }

    }
}
