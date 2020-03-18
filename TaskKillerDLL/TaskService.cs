using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace TaskKillerDLL
{
    public class TaskService
    {
        public static float GigabyteToKilobyte(float gb)
        {
            return gb * 1000000;
        }

        public static float RAMLimit(float allRam, string ramLimitPercent)
        {
            return GigabyteToKilobyte(allRam) * (float)(Convert.ToDouble(ramLimitPercent) / 100);
        }
        public static List<string> GetTasksInfo()
        {
            List<string> tasksInfoList = new List<string>();

            var p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = @"/c tasklist /v /fo list";
            //p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = false;
            p.StartInfo.UseShellExecute = false;
            p.OutputDataReceived += (a, b) =>
            {
                if (!string.IsNullOrWhiteSpace(b.Data))
                {
                    tasksInfoList.Add(b.Data);
                }
            };
            p.Start();
            p.BeginErrorReadLine();
            p.BeginOutputReadLine();
            p.WaitForExit();

            return tasksInfoList;

        }

        public static bool KillTask(string pid)
        {
            try
            {
                var p = new Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.Arguments = $@"/c taskkill /f /pid {pid}";
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardInput = false;
                p.StartInfo.UseShellExecute = false;
                p.Start();
                p.WaitForExit();
                
                return true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
                return false;
            }
        }
        public static bool RestartTask(string pid, string path)
        {
            try
            {
                var p = new Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.Arguments = $@"/c start {path}";
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardInput = false;
                p.StartInfo.UseShellExecute = false;
                p.Start();
                p.WaitForExit();

            return true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
                return false;
            }
        }
        
        //public static List<string> ProcessMemoryAnalyse(List<TaskInfo> orderedTaskList, Dictionary<string, int[]> thresholdList)
        //{
        //    var exceedingProcessesList = new List<string>();
        //    foreach (var taskInfo in orderedTaskList)
        //    {
        //        if (taskInfo.TaskMemory > (thresholdList[taskInfo.TaskImageName][0] * 1.1))
        //        {
        //            exceedingProcessesList.Add(taskInfo.TaskImageName);
        //        }
        //    }
        //    return exceedingProcessesList;
        //}


    }
}
