using DSharpPlus.Entities;
using KWiJisho.Data;
using KWiJisho.Utils;
using System.Threading.Tasks;

namespace KWiJisho.Commands
{
    /// <summary>
    /// Provides methods for basic prefix and slash commands.
    /// </summary>
    internal static class CommandBasic
    {
        /// <summary>
        /// Sends a random animated emoji from <see cref="DiscordEmojis.AnimatedEmojis"/>.
        /// </summary>
        /// <param name="discordChannel">The discord channel where the command will be executed.</param>
        /// <returns>A <see cref="Task"/> from the current asynchronous task.</returns>
        internal static async Task ExecuteRandomAnimatedEmoji(DiscordChannel discordChannel)
            => await discordChannel.SendMessageAsync(UtilCollections.GetRandomValueFromList(DiscordEmojis.AnimatedEmojis).Code);

        /// <summary>
        /// Sends a random animated emoji from <see cref="DiscordEmojis.AnimatedMemeEmojis"/>.
        /// </summary>
        /// <param name="discordChannel">The discord channel where the command will be executed.</param>
        /// <returns>A <see cref="Task"/> from the current asynchronous task.</returns>
        internal static async Task ExecuteRandomAnimatedMemeEmoji(DiscordChannel discordChannel)
            => await discordChannel.SendMessageAsync(UtilCollections.GetRandomValueFromList(DiscordEmojis.AnimatedMemeEmojis).Code);

        /// <summary>
        /// Sends a random animated emoji from <see cref="DiscordEmojis.AnimatedPartyEmojis"/>.
        /// </summary>
        /// <param name="discordChannel">The discord channel where the command will be executed.</param>
        /// <returns>A <see cref="Task"/> from the current asynchronous task.</returns>
        internal static async Task ExecuteRandomAnimatedPartyEmoji(DiscordChannel discordChannel)
            => await discordChannel.SendMessageAsync(UtilCollections.GetRandomValueFromList(DiscordEmojis.AnimatedPartyEmojis).Code);
    }
}
