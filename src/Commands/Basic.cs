using DSharpPlus.Entities;
using KWiJisho.Data;
using KWiJisho.Utils;
using System.Threading.Tasks;

namespace KWiJisho.Commands
{
    /// <summary>
    /// Provides methods for "prefix" and "slash" basic commands.
    /// </summary>
    internal static class Basic
    {
        /// <summary>
        /// Sends a random animated emoji from <see cref="DiscordEmojis.AnimatedEmojis"/>.
        /// </summary>
        /// <param name="discordChannel">The discord channel where the command will be executed.</param>
        /// <returns>A <see cref="Task"/> from the current asynchronous task.</returns>
        internal static async Task SendRandomAnimatedEmoji(DiscordChannel discordChannel)
            => await discordChannel.SendMessageAsync(UtilList.GetRandomValueFromList(DiscordEmojis.AnimatedEmojis).Code);

        /// <summary>
        /// Sends a random animated emoji from <see cref="DiscordEmojis.AnimatedMemeEmojis"/>.
        /// </summary>
        /// <param name="discordChannel">The discord channel where the command will be executed.</param>
        /// <returns>A <see cref="Task"/> from the current asynchronous task.</returns>
        internal static async Task SendRandomAnimatedMemeEmoji(DiscordChannel discordChannel)
            => await discordChannel.SendMessageAsync(UtilList.GetRandomValueFromList(DiscordEmojis.AnimatedMemeEmojis).Code);

        /// <summary>
        /// Sends a random animated emoji from <see cref="DiscordEmojis.AnimatedPartyEmojis"/>.
        /// </summary>
        /// <param name="discordChannel">The discord channel where the command will be executed.</param>
        /// <returns>A <see cref="Task"/> from the current asynchronous task.</returns>
        internal static async Task SendRandomAnimatedPartyEmoji(DiscordChannel discordChannel)
            => await discordChannel.SendMessageAsync(UtilList.GetRandomValueFromList(DiscordEmojis.AnimatedPartyEmojis).Code);
    }
}
