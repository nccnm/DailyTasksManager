using DailyTasksManager.Log;
using DailyTasksManager.Logging.LogProviders;
using DailyTasksManager.Tasks.TurnOffHibernation;
using log4net;
using log4net.Config;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DailyTasksManager.Tasks
{
    public class TaskManager
    {
        public TaskManager() { }

        public TaskManager Setup()
        {
            ConfigureLog();

            return this;
        }       

        public async Task Run()
        {
            try
            {
                // Grab the Scheduler instance from the Factory
                NameValueCollection props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };
                StdSchedulerFactory factory = new StdSchedulerFactory(props);
                IScheduler scheduler = await factory.GetScheduler();

                // and start it off
                await scheduler.Start();

                await ScheduleJobs(scheduler);
            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
            }
        }

        private static void ConfigureLog()
        {
            XmlConfigurator.Configure(
             LogManager.GetRepository(Assembly.GetAssembly(typeof(LogManager))),
             new FileInfo("log4net.config"));

            LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());
        }

        private static async Task ScheduleJobs(IScheduler scheduler)
        {
            var task1 = new TurnOffHibernationTask();
            await task1.ScheduleJob(scheduler);

            var task2 = new PreventSleepModeTask();
            await task2.ScheduleJob(scheduler);
        }
    }
}
