using Quartz.Impl;
using Quartz;
using System;
using System.Threading.Tasks;

namespace KWiJisho.Models.Events
{
    internal class BirthdayJob : IJob
    {
        /// <inheritdoc/>
        public Task Execute(IJobExecutionContext context) => GiveBirthdayMessage();

        private static async Task GiveBirthdayMessage() => await Task.Run(() => "");
    }


    internal class Scheduler
    {
        internal static async Task CreateBirthdayScheduler()
        {
            // Creating and starting a scheduler
            var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            // Creating a job
            var job = JobBuilder.Create<BirthdayJob>().WithIdentity("BirthdayCheck", "KWiJisho").Build();

            // Creating a trigger that will fire everyday at 12pm
            var trigger = TriggerBuilder.Create()
                .WithIdentity("BirthdayCheck", "KWiJisho")
                .WithDailyTimeIntervalSchedule(period=> period.StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(12, 0)).OnEveryDay())
                .Build();

            // Scheduling the job with the trigger
            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
