using Quartz;
using System;
using System.Collections.Generic;
using System.Text;

namespace DailyTasksManager.Tasks.TurnOffHibernation
{
    public class TurnOffHibernationTask : AbstractTask
    {
        private int INTERVAL_IN_SECOND = 10;

        public TurnOffHibernationTask()
        {
            jobName = "Turn off Hibernation";
            groupName = "System";
            triggerName = "Turn off Hibernation Trigger"; 
        }

        public override IJobDetail CreateJobDetail()
        {
            return JobBuilder.Create<TurnOffHibernationJob>()
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
