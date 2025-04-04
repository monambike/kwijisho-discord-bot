// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus;

namespace KWiJisho.Utils
{
    public static class Logs
    {
        /// <summary>
        /// Gets or sets the <see cref="KWiJishoBot"/> instance for handling bot log.
        /// </summary>
        public static Log DefaultLog { get; set; } = null!;

        public static void InstantiateAllLogs(DiscordClient discordClient)
        {
            // Defining default discord log instance.
            DefaultLog = new Log(discordClient)
            {
                SendToDiscord = true,

                WriteFile = true
            };
        }
    }
}
