using Microsoft.Owin.Hosting;
using System;

namespace TaskKillerAbonent
{
    public class Program
    {
        static void Main()
        {
            string baseAddress = "http://*:58415/";

            // Start OWIN host 
            using (WebApp.Start<OwinSelfHostStartup>(url: baseAddress))
            {
                Console.ReadLine();
            }
        }
    }
}