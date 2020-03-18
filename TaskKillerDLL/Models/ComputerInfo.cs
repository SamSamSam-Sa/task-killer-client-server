namespace TaskKillerDLL
{
    public class ComputerInfo 
    {
        public string ComputerNuber { get; set; }
        public string ComputerName { get; set; }
        public string IP { get; set; }
        public float RAM { get; set; }

        public ComputerInfo(string computerNumber, string computerName, string ip, float ram)
        {
            ComputerNuber = computerNumber;
            ComputerName = computerName;
            IP = ip;
            RAM = ram;
        }

    }
}
