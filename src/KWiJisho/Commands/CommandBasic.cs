// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus.Entities;
using KWiJisho.Data;
using KWiJisho.Utils;
using System.Threading.Tasks;

namespace KWiJisho.Commands
{
    /// <summary>
    /// Provides methods for basic prefix and slash commands.
    /// </summary>
    public static class CommandBasic
    {
        /// <summary>
        /// Sends a random animated emoji from <see cref="DiscordEmojis.AnimatedEmojis"/>.
        /// </summary>
        /// <param name="discordChannel">The discord channel where the command will be executed.</param>
        /// <returns>A <see cref="Task"/> from the current asynchronous task.</returns>
        public static async Task CommandRandomAnimatedEmojiAsync(DiscordChannel discordChannel)
            => await discordChannel.SendMessageAsync(UtilCollections.GetRandomValueFromList(DiscordEmojis.AnimatedEmojis).Code);

        /// <summary>
        /// Sends a random animated emoji from <see cref="DiscordEmojis.AnimatedMemeEmojis"/>.
        /// </summary>
        /// <param name="discordChannel">The discord channel where the command will be executed.</param>
        /// <returns>A <see cref="Task"/> from the current asynchronous task.</returns>
        public static async Task CommandRandomAnimatedMemeEmojiAsync(DiscordChannel discordChannel)
            => await discordChannel.SendMessageAsync(UtilCollections.GetRandomValueFromList(DiscordEmojis.AnimatedMemeEmojis).Code);

        /// <summary>
        /// Sends a random animated emoji from <see cref="DiscordEmojis.AnimatedPartyEmojis"/>.
        /// </summary>
        /// <param name="discordChannel">The discord channel where the command will be executed.</param>
        /// <returns>A <see cref="Task"/> from the current asynchronous task.</returns>
        public static async Task CommandRandomAnimatedPartyEmojiAsync(DiscordChannel discordChannel)
            => await discordChannel.SendMessageAsync(UtilCollections.GetRandomValueFromList(DiscordEmojis.AnimatedPartyEmojis).Code);
    }
}
