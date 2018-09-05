using DailyTasksManager.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DailyTasksManager.Tasks.TurnOffHibernation
{
    class TurnOffHibernationJob : IJob
    {
        private readonly ILog Logger = LogProvider.GetCurrentClassLogger();

        public async Task Execute(IJobExecutionContext context)
        {
            System.Diagnostics.Process.Start("powercfg.exe", "-h off");           

            var logMessage = "Turn off Hibernation Task!";           
            Logger.Info(logMessage);

            //await Task.Yield();
            await Task.CompletedTask;
        }
    }
}
