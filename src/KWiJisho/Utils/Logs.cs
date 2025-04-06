// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus;

namespace KWiJisho.Utils
{
    /// <summary>
    /// Provides centralized access to logging utilities used by the bot.
    /// </summary>
    public static class Logs
    {
        /// <summary>
        /// Gets or sets the <see cref="KWiJishoBot"/> instance for handling bot log.
        /// </summary>
        public static Log DefaultLog { get; set; } = null!;

        /// <summary>
        /// Instantiates and configures all log instances used by the application.
        /// </summary>
        /// <param name="discordClient">The <see cref="DiscordClient"/> instance used to initialize the logging system.</param>
        public static void InstantiateAllLogs(DiscordClient discordClient)
        {
            // The default discord log instance.
            DefaultLog = new Log(discordClient)
            {
                SendToDiscord = true,

                WriteFile = true
            };
        }
    }
}
