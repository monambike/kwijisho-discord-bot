using System.IO;
using System;

namespace KWiJisho.Models.Utils
{
    /// <summary>
    /// Class responsible for creating application logs for important tasks.
    /// </summary>
    internal static class Log
    {
        private static string Path => "KWiJishoLog.txt";

        internal enum LogType
        {
            Debug,
            Info,
            Warning,
            Error,
            Fatal
        }

        internal static void AddDebug(string message) => Add(LogType.Debug, message);
        internal static void AddInfo(string message) => Add(LogType.Info, message);
        internal static void AddWarning(string message) => Add(LogType.Warning, message);
        internal static void AddError(string message) => Add(LogType.Error, message);
        internal static void AddFatal(string message) => Add(LogType.Fatal, message);

        private static void Add(LogType logType, string message)
            => AddLineToFile($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] {logType.ToString().ToUpper()} {message}");

        internal static void AddLineToFile(string line)
        {
            using StreamWriter writer = File.AppendText(Path);
            writer.WriteLine(line);
        }
    }
}
