using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Http;
using System.Windows.Forms;
using TaskKillerDLL;

namespace TaskKillerAbonent
{
    [RoutePrefix("api/tasks")]
    public class ValuesController : ApiController
    {
        [Route("taskInfo")]
        [HttpGet]

        public IEnumerable<TaskInfo> Get()
        {
            var tasksInfo = TaskService.GetTasksInfo();
            return TaskInfoHelper.GetOrderedTaskList(tasksInfo);
        }


        [Route("killTask")]
        [HttpPost]

        public bool PostKill([FromBody] TaskInfo taskinfo)
        {
           DialogResult dialogResult =  MessageBox.Show($"Confirm {taskinfo.TaskImageName} close", "TaskKiller Message", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                var isProcessKilled = TaskService.KillTask(taskinfo.PID);
                if (isProcessKilled)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        [Route("restartTask")]
        [HttpPost]

        public bool PostRestart([FromBody] TaskInfo taskinfo)
        {
            DialogResult dialogResult = MessageBox.Show($"Confirm {taskinfo.TaskImageName} restart", "TaskKiller Message", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                var process = Process.GetProcessById(Convert.ToInt32(taskinfo.PID));
                var path = process.MainModule.FileName;

                var isProcessKilled = TaskService.KillTask(taskinfo.PID);
                if (isProcessKilled)
                {
                    var isProcessRestarted = TaskService.RestartTask(taskinfo.PID, path);
                    if (isProcessRestarted)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


    }
}