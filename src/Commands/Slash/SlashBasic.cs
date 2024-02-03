using DSharpPlus;
using DSharpPlus.SlashCommands;
using KWiJisho.Commands;
using System.Threading.Tasks;

namespace KWiJisho.Commands.Slash
{
    /// <summary>
    /// Represents a set of basic slash commands.
    /// </summary>
    internal class SlashBasic : ApplicationCommandModule
    {
        /// <summary>
        /// Represents a command group to senda random animated emoji.
        /// </summary>
        [SlashCommandGroup("emoji", "Envia um emoji animado aleatório! hehe.")]
        public class Emoji : ApplicationCommandModule
        {
            /// <summary>
            /// Represents the command to send a random animated emoji.
            /// </summary>
            [SlashCommand("any", "Envia qualquer emoji animado.")]
            internal static async Task SendRandomAnimatedEmoji(InteractionContext interactionContext)
            {
                await Basic.SendRandomAnimatedEmoji(interactionContext.Channel);
                await interactionContext.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource);
            }

            /// <summary>
            /// Represents the command to send a random animated meme emoji.
            /// </summary>
            [SlashCommand("meme", "Envia um emoji de meme animado.")]
            internal static async Task SendRandomAnimatedMemeEmoji(InteractionContext interactionContext)
            {
                await Basic.SendRandomAnimatedMemeEmoji(interactionContext.Channel);
                await interactionContext.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource);
            }

            /// <summary>
            /// Represents the command to send a random animated party emoji.
            /// </summary>
            [SlashCommand("party", "Envia um emoji de festa animado.")]
            internal static async Task SendRandomAnimatedPartyEmoji(InteractionContext interactionContext)
            {
                await Basic.SendRandomAnimatedPartyEmoji(interactionContext.Channel);
                await interactionContext.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource);
            }
        }
    }
}
