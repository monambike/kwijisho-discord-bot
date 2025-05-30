﻿// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus;
using KWiJisho.Data;
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
        /// <summary>
        /// Check if the log should be written in a text file.
        /// </summary>
        public required bool WriteFile { get; init; }

        /// <summary>
        /// Check if the log should be send to the Discord channel.
        /// </summary>
        public required bool SendToDiscord { get; init; }

        /// <summary>
        /// The Discord client.
        /// </summary>
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
            /// Represents the slash and prefix commands module.
            /// </summary>
            CommandExecution,

            /// <summary>
            /// Represents the ChatGpt module.
            /// </summary>
            ChatGpt,

            /// <summary>
            /// Represents the general system KWiJisho module.
            /// </summary>
            System,

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
        /// <param name="module">The module of the log entry.</param>
        /// <param name="logContext">The log context of the log entry</param>
        /// <param name="message">The debug log message.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task AddDebugAsync(Module module, LogContext logContext, string message) => await AddCustomLogAsync(LogType.Debug, module, logContext, message);

        /// <summary>
        /// Adds a info log entry with the specified message.
        /// </summary>
        /// <param name="module">The module of the log entry.</param>
        /// <param name="logContext">The log context of the log entry</param>
        /// <param name="message">The info log message.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task AddInfoAsync(Module module, LogContext logContext, string message) => await AddCustomLogAsync(LogType.Info, module, logContext, message);

        /// <summary>
        /// Adds a warning log entry with the specified message.
        /// </summary>
        /// <param name="module">The module of the log entry.</param>
        /// <param name="logContext">The log context of the log entry</param>
        /// <param name="message">The warning log message.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task AddWarningAsync(Module module, LogContext logContext, string message) => await AddCustomLogAsync(LogType.Warning, module, logContext, message);

        /// <summary>
        /// Adds a error log entry with the specified message.
        /// </summary>
        /// <param name="module">The module of the log entry.</param>
        /// <param name="logContext">The log context of the log entry</param>
        /// <param name="message">The error log message.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task AddErrorAsync(Module module, LogContext logContext, string message) => await AddCustomLogAsync(LogType.Error, module, logContext, message);

        /// <summary>
        /// Adds a fatal log entry with the specified message.
        /// </summary>
        /// <param name="module">The module of the log entry.</param>
        /// <param name="logContext">The log context of the log entry</param>
        /// <param name="message">The fatal log message.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task AddFatalAsync(Module module, LogContext logContext, string message) => await AddCustomLogAsync(LogType.Fatal, module, logContext, message);

        /// <summary>
        /// Adds a formatted log entry to the log file.
        /// </summary>
        /// <param name="logType">The type of log entry.</param>
        /// <param name="module">The module of the log entry.</param>
        /// <param name="logContext">The log context of the log entry</param>
        /// <param name="message">The log message to be added.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        private async Task AddCustomLogAsync(LogType logType, Module module, LogContext logContext, string message)
        {
            TimeZoneInfo brazilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

            DateTime brazilTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, brazilTimeZone);

            string guildInfo = string.IsNullOrEmpty($"{logContext.GuildId}")
            ? "" : $"[Guild: {logContext.GuildId}] ";

            string contextInfo = string.IsNullOrEmpty(logContext.ContextType) || string.IsNullOrEmpty(logContext.Action)
            ? "" : $"[{logContext.ContextType}: {logContext.Action}] ";

            string issuerInfo = string.IsNullOrEmpty($"{logContext.IssuerId}")
            ? "" : $"[Issuer ID: {logContext.IssuerId}] ";

            await AddEntryAsync($"[{brazilTime:yyyy-MM-dd HH:mm:ss.fff UTC: zzz}] [{module}] {guildInfo}{issuerInfo}{contextInfo}{logType.ToString().ToUpper()}: {message}".ToDiscordMonospace());
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

        /// <summary>
        /// Writes a log entry to a file asynchronously, if file logging is enabled.
        /// </summary>
        /// <param name="entry">The log message to be written to the file.</param>
        /// <returns>A <see cref="Task"/> from the method execution.</returns>
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

        /// <summary>
        /// Send a log entry to a Discord channel asynchronously, if Discord logging is enabled.
        /// </summary>
        /// <param name="entry">The log message to be written to the Discord channel.</param>
        /// <returns>A <see cref="Task"/> from the method execution.</returns>
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
