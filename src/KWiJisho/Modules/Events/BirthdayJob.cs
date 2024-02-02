using Quartz;
using Quartz.Impl;
using System.Threading.Tasks;

namespace KWiJisho.Modules.Events
{
    /// <summary>
    /// Represents a Quartz.NET job that executes a birthday message task.
    /// </summary>
    internal class BirthdayJob : IJob
    {
        /// <summary>
        /// Executes the birthday message task as part of the Quartz.NET job.
        /// </summary>
        /// <param name="context">The execution context provided by Quartz.NET.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous execution of the job.</returns>
        public Task Execute(IJobExecutionContext context) => GiveBirthdayMessage();

        /// <summary>
        /// Sends a birthday message as an asynchronous task.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous execution of the birthday message task.</returns>
        private static async Task GiveBirthdayMessage() => await Task.Run(() => "");
    }

    /// <summary>
    /// Provides methods for scheduling tasks.
    /// </summary>
    internal class Scheduler
    {
        /// <summary>
        /// Creates a birthday scheduler as part of the Quartz.NET library.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous creation of the birthday scheduler.</returns>
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
