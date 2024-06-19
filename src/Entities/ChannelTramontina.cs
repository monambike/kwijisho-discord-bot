// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus;
using System.Collections.Generic;
using static KWiJisho.Commands.CommandThemeTramontina;

namespace KWiJisho.Entities
{
    /// <summary>
    /// Initializes a new instance of <see cref="ChannelTramontina"/> class with the specified id, the default channel
    /// text title and emoji theme.
    /// </summary>
    /// <param name="id">The Id from the Discord channel.</param>
    /// <param name="defaultTextTitle">The default text title from the Discord channel.</param>
    /// <param name="emojiTheme">The emoji theme dictionary avaiable for this channel.</param>
    public class ChannelTramontina(ulong id, string defaultTextTitle, Dictionary<EmojiTheme, string> emojiTheme) : Channel(id, $"{defaultTextTitle}")
    {
        /// <summary>
        /// The default text title for the tramontina Discord channel.
        /// </summary>
        public string DefaultTextTitle { get; set; } = defaultTextTitle;

        /// <summary>
        /// The dictionary containing the EmojiTheme that represents the seasonal theme associate to the emoji, and
        /// the string that represents the emoji itself for the tramontina Discord channel.
        /// </summary>
        public Dictionary<EmojiTheme, string> EmojiTheme { get; set; } = emojiTheme;

        /// <summary>
        /// Change the emoji for current channel. At the current moment, you can do this twice with the same channel
        /// every 10 minutes.
        /// </summary>
        /// <param name="discordClient">The Discord client instance.</param>
        /// <param name="emoji">The emoji to be changed in the channel.</param>
        public void ChangeEmoji(DiscordClient discordClient, string emoji) => UpdateChannelNameAsync(discordClient, $"{emoji}│{DefaultTextTitle}");

    }
}
