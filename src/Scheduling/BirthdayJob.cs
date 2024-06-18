// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus;
using KWiJisho.Commands;
using KWiJisho.Data;
using KWiJisho.Utils;
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
            await KWiJishoLogs.DefaultLog.AddInfoAsync(KWiJishoLog.Module.Birthday, "Executing birthday job.");
            var dataMap = context.MergedJobDataMap;
            _client = (DiscordClient) dataMap.Get("DiscordClient");
            await GiveBirthdayMessage();
            await KWiJishoLogs.DefaultLog.AddInfoAsync(KWiJishoLog.Module.Birthday, "Finished birthday job.");
        }

        /// <summary>
        /// Sends a birthday message as an asynchronous task.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous execution of the birthday message task.</returns>
        private async Task GiveBirthdayMessage()
        {
            var server = Servers.Tramontina;

            // Getting guild by id.
            var serverGuild = await _client.GetGuildAsync(server.GuildId);

            // Getting channel by id.
            var discordChannel = serverGuild.GetChannel(server.GeneralChannelId);

            // Sending the birthday message.
            await CommandBirthday.SendBirthdayMessage(discordChannel, serverGuild, true);
        }
    }
}
