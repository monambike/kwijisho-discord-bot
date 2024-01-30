using DSharpPlus.Entities;
using KWiJisho.Models.Utils;
using System.Threading.Tasks;

namespace KWiJisho.Models.Commands
{
    internal class Basic
    {
        /// <summary>
        /// Sends a random animated emoji from <see cref="Emojis.AnimatedEmojis.KWiJishoEmojis"/>.
        /// </summary>
        /// <param name="discordChannel"></param>
        /// <returns></returns>
        internal static async Task SendRandomAnimatedEmoji(DiscordChannel discordChannel)
            => await discordChannel.SendMessageAsync(List.GetRandomValueFromList(Emojis.AnimatedEmojis.KWiJishoEmojis).Code);

        /// <summary>
        /// Sends a random animated emoji from <see cref="Emojis.AnimatedEmojis.KWiJishoMemeEmojis"/>.
        /// </summary>
        /// <param name="discordChannel"></param>
        /// <returns></returns>
        internal static async Task SendRandomAnimatedMemeEmoji(DiscordChannel discordChannel)
            => await discordChannel.SendMessageAsync(List.GetRandomValueFromList(Emojis.AnimatedEmojis.KWiJishoMemeEmojis).Code);

        /// <summary>
        /// Sends a random animated emoji from <see cref="Emojis.AnimatedEmojis.KWiJishoPartyEmojis"/>.
        /// </summary>
        /// <param name="discordChannel"></param>
        /// <returns></returns>
        internal static async Task SendRandomAnimatedPartyEmoji(DiscordChannel discordChannel)
            => await discordChannel.SendMessageAsync(List.GetRandomValueFromList(Emojis.AnimatedEmojis.KWiJishoPartyEmojis).Code);
    }
}
