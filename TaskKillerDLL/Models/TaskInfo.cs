namespace TaskKillerDLL
{
    public class TaskInfo
    {
        public string TaskImageName { get; set; }
        public string PID { get; set; }
        public string SessionName { get; set; }
        public string SessionNumber { get; set; }
        public int TaskMemory { get; set; }
        public string WindowTitle { get; set; }
        public bool IsLimitExceeded { get; set; }

        public TaskInfo()
        {

        }

    }
}
