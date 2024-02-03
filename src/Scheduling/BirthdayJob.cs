using Quartz;
using Quartz.Impl;
using System.Threading.Tasks;

namespace KWiJisho.Scheduling
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
}
