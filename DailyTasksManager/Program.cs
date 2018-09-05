using System;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using DailyTasksManager.Log;
using DailyTasksManager.Tasks;
using DailyTasksManager.Tasks.TurnOffHibernation;
using log4net;
using log4net.Config;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;

namespace DailyTasksManager
{
    public class Program
    {
        private static void Main(string[] args)
        {
          

            TaskManager taskManager = new TaskManager();
            taskManager
                .Setup()
                .Run()
                .GetAwaiter()
                .GetResult(); 

            Console.WriteLine("Press any key to close the application");
            Console.ReadKey();

            //Thread.Sleep(Timeout.Infinite);
        }
    }
}
