// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using System;
using System.Collections.Generic;

namespace KWiJisho.Utils
{
    /// <summary>
    /// Provides utility extension methods for formatting text with Discord markdown.
    /// </summary>
    public static class DiscordFormat
    {
        /// <summary>
        /// Formats the specified string in bold using Discord markdown.
        /// </summary>
        /// <param name="str">The string to be formatted.</param>
        /// <returns>The string in bold.</returns>
        public static string ToDiscordBold(this string str) => $"**{str}**";

        /// <summary>
        /// Formats the specified string in italic using Discord markdown.
        /// </summary>
        /// <param name="str">The string to be formatted.</param>
        /// <returns>The string in italic.</returns>
        public static string ToDiscordItalic(this string str) => $"*{str}*";

        /// <summary>
        /// Formats the specified string to be escaped using Discord markdown.
        /// </summary>
        /// <param name="str">The string to be formatted.</param>
        /// <returns>The escaped string.</returns>
        public static string ToDiscordEscape(this string str) => $@"\{str}";

        /// <summary>
        /// Formats the specified string in a link using Discord markdown.
        /// </summary>
        /// <param name="str">The string to be formatted.</param>
        /// <param name="url">The link url.</param>
        /// <returns>The string as a link.</returns>
        public static string ToDiscordLink(this string str, string url) => $"[{str}]({url})";

        public static string ToDiscordDate(this DateTime dateTime, DiscordDateTimeFormat discordDateTimeFormat)
        {
            var unixTimestamp = UtilDate.ConvertDateTimeToUnixTimestamp(dateTime).ToString();

            var result = ToDiscordDate(unixTimestamp, discordDateTimeFormat);

            return result;
        }

        public static string ToDiscordDate(this string unixDateTime, DiscordDateTimeFormat discordDateTimeFormat)
        {
            var suffixDateTimeFormat = GetDiscordDateTimeFormat(discordDateTimeFormat);

            var result = $"<t:{unixDateTime}{suffixDateTimeFormat}>";

            return result;
        }

        public static string GetDiscordDateTimeFormat(DiscordDateTimeFormat discordDateTimeFormat)
        {
            // Returns a empty string.
            if (discordDateTimeFormat == DiscordDateTimeFormat.Default) return "";

            Dictionary<DiscordDateTimeFormat, string> dateTimeFormats = new()
            {
                { DiscordDateTimeFormat.Long, "f"},
                { DiscordDateTimeFormat.Full, "F"},
                { DiscordDateTimeFormat.DateOnly, "d"},
                { DiscordDateTimeFormat.DateMonthName, "D"},
                { DiscordDateTimeFormat.HourMinute, "t"},
                { DiscordDateTimeFormat.HourMinuteSeconds, "T"},
                { DiscordDateTimeFormat.RelativeTime, "R" }
            };

            var result = $":{dateTimeFormats[discordDateTimeFormat]}";

            return result;
        }

        /// <summary>
        /// Represents a date in Discord date format.
        /// </summary>
        public enum DiscordDateTimeFormat
        {
            /// <summary>
            /// Represents a long date and time format.
            /// </summary>
            /// <remarks>
            /// Example: <code>&lt;t:1038146764&gt;</code> results in November 24, 2002 12:06 PM.
            /// </remarks>
            Default,

            /// <summary>
            /// Represents a long date and time format.
            /// </summary>
            /// <remarks>
            /// Example: <code>&lt;t:1038146764:f&gt;</code> results in November 24, 2002 12:06 PM.
            /// </remarks>
            Long,

            /// <summary>
            /// Represents a full date and time format with the day of the week.
            /// </summary>
            /// <remarks>
            /// Example: <code>&lt;t:1038146764:F&gt;</code> results in Sunday, November 24, 2002 12:06 PM.
            /// </remarks>
            Full,

            /// <summary>
            /// Represents a short date format.
            /// </summary>
            /// <remarks>
            /// Example: <code>&lt;t:1038146764:d&gt;</code> results in 11/24/2002.
            /// </remarks>
            DateOnly,

            /// <summary>
            /// Represents a long date format.
            /// </summary>
            /// <remarks>
            /// Example: <code>&lt;t:1038146764:D&gt;</code> results in November 24, 2002.
            /// </remarks>
            DateMonthName,

            /// <summary>
            /// Represents a short time format.
            /// </summary>
            /// <remarks>
            /// Example: <code>&lt;t:1038146764:t&gt;</code> results in 12:06 PM.
            /// </remarks>
            HourMinute,

            /// <summary>
            /// Represents a long time format including seconds.
            /// </summary>
            /// <remarks>
            /// Example: <code>&lt;t:1038146764:T&gt;</code> results in 12:06:04 PM.
            /// </remarks>
            HourMinuteSeconds,

            /// <summary>
            /// Represents relative time (e.g., "22 years ago" or "6 hours ago").
            /// </summary>
            /// <remarks>
            /// Example: <code>&lt;t:1038146764:R&gt;</code> results in 22 years ago".
            /// </remarks>
            RelativeTime
        }
    }
}
