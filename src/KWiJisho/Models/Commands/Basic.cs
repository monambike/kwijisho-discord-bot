using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using KWiJisho.Models.Utils;
using System.Threading.Tasks;

namespace KWiJisho.Models.Commands
{
    internal class Basic
    {
        /// <summary>
        /// Sends a random animated emoji from <see cref="DiscordEmojis.AnimatedEmojis.KWiJishoEmojis"/>.
        /// </summary>
        /// <param name="discordChannel"></param>
        /// <returns></returns>
        internal static async Task SendRandomAnimatedEmoji(DiscordChannel discordChannel)
            => await discordChannel.SendMessageAsync(UtilList.GetRandomValueFromList(DiscordEmojis.AnimatedEmojis.KWiJishoEmojis).Code);

        /// <summary>
        /// Sends a random animated emoji from <see cref="DiscordEmojis.AnimatedEmojis.KWiJishoMemeEmojis"/>.
        /// </summary>
        /// <param name="discordChannel"></param>
        /// <returns></returns>
        internal static async Task SendRandomAnimatedMemeEmoji(DiscordChannel discordChannel)
            => await discordChannel.SendMessageAsync(UtilList.GetRandomValueFromList(DiscordEmojis.AnimatedEmojis.KWiJishoMemeEmojis).Code);

        /// <summary>
        /// Sends a random animated emoji from <see cref="DiscordEmojis.AnimatedEmojis.KWiJishoPartyEmojis"/>.
        /// </summary>
        /// <param name="discordChannel"></param>
        /// <returns></returns>
        internal static async Task SendRandomAnimatedPartyEmoji(DiscordChannel discordChannel)
            => await discordChannel.SendMessageAsync(UtilList.GetRandomValueFromList(DiscordEmojis.AnimatedEmojis.KWiJishoPartyEmojis).Code);
    }
}
