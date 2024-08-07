﻿// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

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
    }
}
