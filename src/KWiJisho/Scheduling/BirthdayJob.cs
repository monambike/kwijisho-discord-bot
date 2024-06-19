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
    /// <summary>
    /// Represents a Quartz.NET job that executes a birthday message task.
    /// </summary>
    [DisallowConcurrentExecution]
    public class BirthdayJob : IJob
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
            // Retrieve the server details for Tramontina
            var server = Servers.Tramontina;

            // Check if the _client is null and return if it is
            if (_client is null) return;

            // Get the server guild asynchronously using the client and the server's GuildId
            var serverGuild = await _client.GetGuildAsync(server.GuildId);

            // Get the general channel in the server guild using the server's GeneralChannelId
            var discordChannel = serverGuild.GetChannel(server.GeneralChannelId);

            // Send the birthday message to the retrieved discord channel and server guild
            await CommandBirthday.SendBirthdayMessage(discordChannel, serverGuild, true);
        }
    }
}
