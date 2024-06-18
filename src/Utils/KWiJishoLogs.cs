using DSharpPlus;

namespace KWiJisho.Utils
{
    internal static class KWiJishoLogs
    {
        /// <summary>
        /// Gets or sets the <see cref="KWiJishoBot"/> instance for handling bot log.
        /// </summary>
        internal static KWiJishoLog DefaultLog { get; set; } = null!;

        public static void InstantiateAllLogs(DiscordClient discordClient)
        {
            // Defining default discord log instance.
            DefaultLog = new KWiJishoLog(discordClient)
            {
                SendToDiscord = true,
                WriteFile = true
            };
        }
    }
}
