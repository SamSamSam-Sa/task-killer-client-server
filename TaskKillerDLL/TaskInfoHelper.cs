using System;
using System.Collections.Generic;

namespace TaskKillerDLL
{
    public class TaskInfoHelper
    {

        public static List<TaskInfo> GetOrderedTaskList(List<string> tasksInfoList)
        {
            var оrderedTaskList = new List<TaskInfo>();
            var stringNumber = 0;
            var taskInfo = new TaskInfo();

            foreach (var infoString in tasksInfoList)
            {
                var infoStringValue = GetinfoStringValue(infoString);
                switch (stringNumber % 9)
                {
                    case 0:
                        {
                            taskInfo.TaskImageName = infoStringValue;
                        }
                        break;
                    case 1:
                        {
                            taskInfo.PID = infoStringValue;
                        }
                        break;
                    case 2:
                        {
                            taskInfo.SessionName = infoStringValue;
                        }
                        break;
                    case 3:
                        {
                            taskInfo.SessionNumber = infoStringValue;
                        }
                        break;
                    case 4:
                        {
                            var infostringWithoutHeader = infoStringValue;
                            infostringWithoutHeader = infostringWithoutHeader.Substring(0, infostringWithoutHeader.LastIndexOf(' ')).Replace(" ", "");
                            taskInfo.TaskMemory = Convert.ToInt32(infostringWithoutHeader);
                        }
                        break;
                    case 8:
                        {
                            taskInfo.WindowTitle = infoStringValue;

                            оrderedTaskList.Add(taskInfo);
                            taskInfo = new TaskInfo();
                        }
                        break;

                    default:
                        break;

                }
                stringNumber++;
            }
            return оrderedTaskList;
        }

        private static string GetinfoStringValue(string infoString)
        {
            return infoString.Substring(infoString.IndexOf(":") + 1, infoString.Length - (infoString.IndexOf(":") + 1)).Trim(' ');
        }

    }
}
