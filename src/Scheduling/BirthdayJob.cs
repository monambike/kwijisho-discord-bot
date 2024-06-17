using DSharpPlus;
using KWiJisho.Commands;
using KWiJisho.Data;
using KWiJisho.Models;
using Quartz;
using System.Threading.Tasks;

namespace KWiJisho.Scheduling
{
    [DisallowConcurrentExecution]
    /// <summary>
    /// Represents a Quartz.NET job that executes a birthday message task.
    /// </summary>
    internal class BirthdayJob : IJob
    {
        private DiscordClient? _client;

        /// Executes the birthday message task as part of the Quartz.NET job.
        /// </summary>
        /// <param name="context">The execution context provided by Quartz.NET.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous execution of the job.</returns>
        public async Task Execute(IJobExecutionContext context)
        {
            var dataMap = context.MergedJobDataMap;
            _client = (DiscordClient)dataMap.Get("DiscordClient");

            await GiveBirthdayMessage();
        }

        /// <summary>
        /// Sends a birthday message as an asynchronous task.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous execution of the birthday message task.</returns>
        private async Task GiveBirthdayMessage()
        {
            var server = Servers.Personal;

            // Getting guild by id.
            var serverGuild = await _client.GetGuildAsync(server.GuildId);

            // Getting channel by id.
            var discordChannel = serverGuild.GetChannel(server.GeneralChannelId);

            // Sending the birthday message.
            await CommandBirthday.SendBirthdayMessage(discordChannel, serverGuild, true);
        }
    }
}
