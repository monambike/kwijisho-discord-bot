namespace KWiJisho.Utils
{
    /// <summary>
    /// Provides utility extension methods for formatting text with Discord markdown.
    /// </summary>
    internal static class DiscordFormat
    {
        /// <summary>
        /// Formats the specified string in bold using Discord markdown.
        /// </summary>
        /// <param name="str">The string to be formatted.</param>
        /// <returns>The string in bold.</returns>
        internal static string ToDiscordBold(this string str) => $"**{str}**";

        /// <summary>
        /// Formats the specified string in italic using Discord markdown.
        /// </summary>
        /// <param name="str">The string to be formatted.</param>
        /// <returns>The string in italic.</returns>
        internal static string ToDiscordItalic(this string str) => $"*{str}*";

        /// <summary>
        /// Formats the specified string to be escaped using Discord markdown.
        /// </summary>
        /// <param name="str">The string to be formatted.</param>
        /// <returns>The escaped string.</returns>
        internal static string ToDiscordEscape(this string str) => $@"\{str}";

        /// <summary>
        /// Formats the specified string in a link using Discord markdown.
        /// </summary>
        /// <param name="str">The string to be formatted.</param>
        /// <param name="url">The link url.</param>
        /// <returns>The string as a link.</returns>
        internal static string ToDiscordLink(this string str, string url) => $"[{str}]({url})";
    }
}
