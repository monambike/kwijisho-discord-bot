// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus;
using KWiJisho.Commands;
using KWiJisho.Data;
using KWiJisho.Entities;
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
        /// <summary>
        /// The discord client.
        /// </summary>
        private DiscordClient? _client;

        /// <summary>
        /// Represents the Discord server "Tramontina".
        /// </summary>
        private readonly static Server ServerTramontina = Servers.Tramontina;

        /// <summary>
        /// The log context.
        /// </summary>
        public static readonly LogContext LogContext = new()
        {
            Action = "BirthdayReminder",
            ContextType = "Job",
            GuildId = ServerTramontina.GuildId.ToString(),
            IssuerId = Data.KWiJisho.Name
        };

        /// Executes the birthday message task as part of the Quartz.NET job.
        /// </summary>
        /// <param name="context">The execution context provided by Quartz.NET.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous execution of the job.</returns>
        public async Task Execute(IJobExecutionContext context)
        {

            await Logs.DefaultLog.AddInfoAsync(Log.Module.Birthday, LogContext, "Executing birthday job...");
            
            // Setting discord client on initialize.
            var dataMap = context.MergedJobDataMap;
            _client = (DiscordClient) dataMap.Get("DiscordClient");
            
            await GiveBirthdayMessageAsync();

            await Logs.DefaultLog.AddInfoAsync(Log.Module.Birthday, LogContext, "Finished birthday job.");
        }

        /// <summary>
        /// Sends a birthday message as an asynchronous task.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous execution of the birthday message task.</returns>
        private async Task GiveBirthdayMessageAsync()
        {
            // Check if the _client is null and return if it is
            if (_client is null) return;

            // Get the server guild asynchronously using the client and the server's GuildId
            var serverGuild = await _client.GetGuildAsync(ServerTramontina.GuildId);

            // Get the general channel in the server guild using the server's GeneralChannelId
            var discordChannel = serverGuild.GetChannel(ServerTramontina.GeneralChannelId);

            // Send the birthday message to the retrieved discord channel and server guild
            await CommandBirthday.NextBirthdayMessageAsync(discordChannel, serverGuild, true);
        }
    }
}
