using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DailyTasksManager.Tasks.TurnOffHibernation
{
    public abstract class AbstractTask
    {
        protected string jobName;
        protected string groupName;
        protected string triggerName;

        public AbstractTask() { }

        public AbstractTask(string jobName, string triggerName, string groupName)
        {
            this.jobName = jobName;
            this.groupName = groupName;
            this.triggerName = triggerName;
        }


        public async Task ScheduleJob(IScheduler scheduler)
        {
            var job = CreateJobDetail();
            var trigger = CreateTrigger();

            await scheduler.ScheduleJob(job, trigger);
        }


        public abstract IJobDetail CreateJobDetail();


        public abstract ITrigger CreateTrigger();
    }
}
