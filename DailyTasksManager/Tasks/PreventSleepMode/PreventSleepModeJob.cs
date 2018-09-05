using DailyTasksManager.WinAPI;
using Quartz;
using System;
using System.Threading.Tasks;
using DailyTasksManager.Logging;

namespace DailyTasksManager.Tasks.TurnOffHibernation
{
    class PreventSleepModeJob : IJob
    {
        private readonly ILog Logger = LogProvider.GetCurrentClassLogger();

        public async Task Execute(IJobExecutionContext context)
        {
            PowerManager.SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_SYSTEM_REQUIRED | EXECUTION_STATE.ES_AWAYMODE_REQUIRED);

            var logMessage = "Prevent Sleep Mode Task!";           
            Logger.Info(logMessage);

            //await Task.Yield();
            await Task.CompletedTask;
        }
    }
}

