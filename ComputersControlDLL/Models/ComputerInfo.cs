using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputersControlDLL
{
    public class ComputerInfo 
    {
        public string ComputerNuber { get; set; }
        public string ComputerName { get; set; }
        public string IP { get; set; }

        public ComputerInfo(string computerNumber, string computerName, string ip)
        {
            ComputerNuber = computerNumber;
            ComputerName = computerName;
            IP = ip;
        }

        public ComputerInfo()
        {

        }

    }
}
