namespace KWiJisho.Models.Utils
{
    internal static class DiscordFormat
    {
        internal static string ToDiscordBold(this string str) => $"**{str}**";

        internal static string ToDiscordItalic(this string str) => $"*{str}*";
    }
}
