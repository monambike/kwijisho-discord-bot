namespace KWiJisho.Models.Utils
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
    }
}
