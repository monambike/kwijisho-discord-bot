// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus;
using Quartz;
using Quartz.Impl;
using System.Threading.Tasks;

namespace KWiJisho.Scheduling
{
    /// <summary>
    /// Provides methods for scheduling tasks.
    /// </summary>
    public class Scheduler
    {
        public static async Task CreateAllSchedulers(DiscordClient discordClient)
        {
            await CreateBirthdayScheduler(discordClient);
        }

        /// <summary>
        /// Creates a birthday scheduler as part of the Quartz.NET library.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous creation of the birthday scheduler.</returns>
        public static async Task CreateBirthdayScheduler(DiscordClient discordClient)
        {
            // Creating and starting a scheduler.
            var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            // Creating a job.
            var job = JobBuilder.Create<BirthdayJob>().WithIdentity("BirthdayCheck", "KWiJisho").Build();

            // Creating a trigger that will fire everyday at 12pm.
            var trigger = TriggerBuilder.Create()
                .UsingJobData(new JobDataMap { { "DiscordClient", discordClient } })
                .WithIdentity("BirthdayCheck", "KWiJisho")
                .WithDailyTimeIntervalSchedule(period => period.StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0)).WithIntervalInHours(24))
                .Build();

            // Scheduling the job with the trigger.
            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
