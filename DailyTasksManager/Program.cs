using DailyTasksManager.Tasks;
using System;

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
