using System;
using System.IO;

namespace KWiJisho.Modules.Utils
{
    /// <summary>
    /// Provides methods for handling the application log for important tasks.
    /// </summary>
    internal static class KWiJishoLog
    {
        /// <summary>
        /// The log file path.
        /// </summary>
        private static string Path => "KWiJishoLog.txt";


        internal enum LogType { Debug, Info, Warning, Error, Fatal }

        /// <summary>
        /// Adds a debug log entry with the specified message.
        /// </summary>
        /// <param name="message">The debug log message.</param>
        internal static void AddDebug(string message) => Add(LogType.Debug, message);

        /// <summary>
        /// Adds a info log entry with the specified message.
        /// </summary>
        /// <param name="message">The info log message.</param>
        internal static void AddInfo(string message) => Add(LogType.Info, message);

        /// <summary>
        /// Adds a warning log entry with the specified message.
        /// </summary>
        /// <param name="message">The warning log message.</param>
        internal static void AddWarning(string message) => Add(LogType.Warning, message);

        /// <summary>
        /// Adds a error log entry with the specified message.
        /// </summary>
        /// <param name="message">The error log message.</param>
        internal static void AddError(string message) => Add(LogType.Error, message);

        /// <summary>
        /// Adds a fatal log entry with the specified message.
        /// </summary>
        /// <param name="message">The fatal log message.</param>
        internal static void AddFatal(string message) => Add(LogType.Fatal, message);

        /// <summary>
        /// Adds a formatted log entry to the log file.
        /// </summary>
        /// <param name="logType">The type of log entry.</param>
        /// <param name="message">The log message to be added.</param>
        private static void Add(LogType logType, string message)
            => AddLineToFile($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] {logType.ToString().ToUpper()} {message}");

        /// <summary>
        /// Adds a line to the log file with the specified content.
        /// </summary>
        /// <param name="line">The line to be added to the log file.</param>
        internal static void AddLineToFile(string line)
        {
            using StreamWriter writer = File.AppendText(Path);
            writer.WriteLine(line);
        }
    }
}
