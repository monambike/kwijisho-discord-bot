using DSharpPlus;
using DSharpPlus.SlashCommands;
using System.Threading.Tasks;

namespace KWiJisho.Models.Commands.Slash
{
    internal class SlashBasic : ApplicationCommandModule
    {
        [SlashCommandGroup("emoji", "Envia um emoji animado aleatório! hehe.")]
        public class Emoji : ApplicationCommandModule
        {
            [SlashCommand("any", "Envia qualquer emoji animado.")]
            internal static async Task SendRandomAnimatedEmoji(InteractionContext interactionContext)
            {
                await Basic.SendRandomAnimatedEmoji(interactionContext.Channel);
                await interactionContext.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource);
            }

            [SlashCommand("meme", "Envia um emoji de meme animado.")]
            internal static async Task SendRandomAnimatedMemeEmoji(InteractionContext interactionContext)
            {
                await Basic.SendRandomAnimatedMemeEmoji(interactionContext.Channel);
                await interactionContext.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource);
            }

            [SlashCommand("party", "Envia um emoji de festa animado.")]
            internal static async Task SendRandomAnimatedPartyEmoji(InteractionContext interactionContext)
            {
                await Basic.SendRandomAnimatedPartyEmoji(interactionContext.Channel);
                await interactionContext.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource);
            }
        }
    }
}
