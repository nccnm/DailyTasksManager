using Quartz;
using System;
using System.Collections.Generic;
using System.Text;

namespace DailyTasksManager.Tasks.TurnOffHibernation
{
    public class PreventSleepModeTask : AbstractTask
    {
        private int INTERVAL_IN_SECOND = 10;

        public PreventSleepModeTask()
        {
            jobName = "Prevent Sleep Mode";
            groupName = "System";
            triggerName = "TPrevent Sleep Mode Trigger";
        }

        public override IJobDetail CreateJobDetail()
        {
            return JobBuilder.Create<PreventSleepModeJob>()
                    .WithIdentity(jobName, groupName)
                    .Build();
        }

        public override ITrigger CreateTrigger()
        {
            return TriggerBuilder.Create()
                    .WithIdentity(triggerName, groupName)
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(INTERVAL_IN_SECOND)
                        .RepeatForever())
                    .Build();
        }

    }
}
