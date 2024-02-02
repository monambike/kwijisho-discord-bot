using DSharpPlus.Entities;
using KWiJisho.Modules.Utils;
using System.Threading.Tasks;

namespace KWiJisho.Modules.Commands
{
    /// <summary>
    /// Provides methods for "prefix" and "slash" basic commands.
    /// </summary>
    internal static class Basic
    {
        /// <summary>
        /// Sends a random animated emoji from <see cref="DiscordEmojis.AnimatedEmojis.KWiJishoEmojis"/>.
        /// </summary>
        /// <param name="discordChannel">The discord channel where the command will be executed.</param>
        /// <returns>A <see cref="Task"/> from the current asynchronous task.</returns>
        internal static async Task SendRandomAnimatedEmoji(DiscordChannel discordChannel)
            => await discordChannel.SendMessageAsync(UtilList.GetRandomValueFromList(DiscordEmojis.AnimatedEmojis.KWiJishoEmojis).Code);

        /// <summary>
        /// Sends a random animated emoji from <see cref="DiscordEmojis.AnimatedEmojis.KWiJishoMemeEmojis"/>.
        /// </summary>
        /// <param name="discordChannel">The discord channel where the command will be executed.</param>
        /// <returns>A <see cref="Task"/> from the current asynchronous task.</returns>
        internal static async Task SendRandomAnimatedMemeEmoji(DiscordChannel discordChannel)
            => await discordChannel.SendMessageAsync(UtilList.GetRandomValueFromList(DiscordEmojis.AnimatedEmojis.KWiJishoMemeEmojis).Code);

        /// <summary>
        /// Sends a random animated emoji from <see cref="DiscordEmojis.AnimatedEmojis.KWiJishoPartyEmojis"/>.
        /// </summary>
        /// <param name="discordChannel">The discord channel where the command will be executed.</param>
        /// <returns>A <see cref="Task"/> from the current asynchronous task.</returns>
        internal static async Task SendRandomAnimatedPartyEmoji(DiscordChannel discordChannel)
            => await discordChannel.SendMessageAsync(UtilList.GetRandomValueFromList(DiscordEmojis.AnimatedEmojis.KWiJishoPartyEmojis).Code);
    }
}
