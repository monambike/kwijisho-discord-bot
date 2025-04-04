// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus;
using KWiJisho.Data;
using Quartz.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace KWiJisho.Utils
{
    /// <summary>
    /// Provides methods for handling the application log for important tasks.
    /// </summary>
    public class Log(DiscordClient? client)
    {
        public required bool WriteFile { get; init; }

        public required bool SendToDiscord { get; init; }

        private readonly DiscordClient? _client = client;

        /// <summary>
        /// The log file name.
        /// </summary>
        private const string FileName = "KWiJishoLog.txt";

        /// <summary>
        /// Represents options for the application log type.
        /// </summary>
        public enum LogType
        {
            /// <summary>
            /// Indicated a debug level log.
            /// </summary>
            Debug,

            /// <summary>
            /// Indicated a info level log.
            /// </summary>
            Info,

            /// <summary>
            /// Indicated a warning level log.
            /// </summary>
            Warning,

            /// <summary>
            /// Indicated a error level log.
            /// </summary>
            Error,

            /// <summary>
            /// Indicated a fatal level log.
            /// </summary>
            Fatal
        }

        /// <summary>
        /// Represents the command modules for the application.
        /// </summary>
        public enum Module
        {
            /// <summary>
            /// Represents the general KWiJisho module.
            /// </summary>
            KWiJisho,

            /// <summary>
            /// Represents the module for "Basic" commands.
            /// </summary>
            Basic,

            /// <summary>
            /// Represents the module for "Birthday" commands.
            /// </summary>
            Birthday,

            /// <summary>
            /// Represents the module for "Info" commands.
            /// </summary>
            Info,

            /// <summary>
            /// Represents the module for "KWiGpt" commands.
            /// </summary>
            KWiGpt,

            /// <summary>
            /// Represents the module for "Nasa" commands.
            /// </summary>
            Nasa,

            /// <summary>
            /// Represents the module for "Notice" commands.
            /// </summary>
            Notice,

            /// <summary>
            /// Represents the module for "ThemeTramontina" commands.
            /// </summary>
            ThemeTramontina
        }

        /// <summary>
        /// Adds a debug log entry with the specified message.
        /// </summary>
        /// <param name="message">The debug log message.</param>
        public async Task AddDebugAsync(Module module, LogContext logContext, string message) => await AddCustomLogAsync(LogType.Debug, module, logContext, message);

        /// <summary>
        /// Adds a info log entry with the specified message.
        /// </summary>
        /// <param name="message">The info log message.</param>
        public async Task AddInfoAsync(Module module, LogContext logContext, string message) => await AddCustomLogAsync(LogType.Info, module, logContext, message);

        /// <summary>
        /// Adds a warning log entry with the specified message.
        /// </summary>
        /// <param name="message">The warning log message.</param>
        public async Task AddWarningAsync(Module module, LogContext logContext, string message) => await AddCustomLogAsync(LogType.Warning, module, logContext, message);

        /// <summary>
        /// Adds a error log entry with the specified message.
        /// </summary>
        /// <param name="message">The error log message.</param>
        public async Task AddErrorAsync(Module module, LogContext logContext, string message) => await AddCustomLogAsync(LogType.Error, module, logContext, message);

        /// <summary>
        /// Adds a fatal log entry with the specified message.
        /// </summary>
        /// <param name="message">The fatal log message.</param>
        public async Task AddFatalAsync(Module module, LogContext logContext, string message) => await AddCustomLogAsync(LogType.Fatal, module, logContext, message);

        /// <summary>
        /// Adds a formatted log entry to the log file.
        /// </summary>
        /// <param name="logType">The type of log entry.</param>
        /// <param name="message">The log message to be added.</param>
        private async Task AddCustomLogAsync(LogType logType, Module module, LogContext logContext, string message)
        {
            TimeZoneInfo brazilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

            DateTime brazilTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, brazilTimeZone);

            string guildInfo = string.IsNullOrEmpty(logContext.GuildId)
            ? "" : $"[Guild: {logContext.GuildId}] ";

            string contextInfo = string.IsNullOrEmpty(logContext.ContextType) || string.IsNullOrEmpty(logContext.Action)
            ? "" : $"[{logContext.ContextType}: {logContext.Action}] ";

            await AddEntryAsync($"[{brazilTime:yyyy-MM-dd HH:mm:ss.fff UTC-zzz}] [{module}] {guildInfo}[Issuer ID: {logContext.IssuerId}] {contextInfo}{logType.ToString().ToUpper()} - {message}");
        }

        /// <summary>
        /// Adds a line to the log file or send to discord log channel with the specified content.
        /// </summary>
        /// <param name="entry">The entry to be added to the log.</param>
        public async Task AddEntryAsync(string entry)
        {
            // Writing entry to log file.
            var taskWriteToFile = WriteEntryToFileAsync(entry);

            // Sending entry to discord log channel.
            var taskSendToDiscord = SendEntryToDiscordAsync(entry);

            // Running all tasks assynchronously.
            await Task.WhenAll(taskWriteToFile, taskSendToDiscord);
        }

        public async Task WriteEntryToFileAsync(string entry)
        {
            if (!WriteFile) return;

            try
            {
                // Opens the log file at the specified path for appending a new text.
                using StreamWriter writer = File.AppendText(FileName);

                // Writing the line on the log file.
                await writer.WriteLineAsync(entry);
            }
            catch (Exception ex)
            {
                await SendEntryToDiscordAsync($"An exception ocurred: {ex} while logging the entry: {entry}");
            }
        }

        public async Task SendEntryToDiscordAsync(string entry)
        {
            if (!SendToDiscord) return;

            var server = Servers.Personal;

            var serverGuild = await _client.GetGuildAsync(server.GuildId);

            // Getting channel by id.
            var discordChannel = serverGuild.GetChannel(server.LogChannelId);

            // Sending the birthday message.
            await discordChannel.SendMessageAsync(entry);
        }
    }
}
