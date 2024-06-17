using DSharpPlus;
using DSharpPlus.SlashCommands;
using System.Threading.Tasks;

namespace KWiJisho.Commands.Slash
{
    /// <summary>
    /// Represents a set of basic slash commands.
    /// </summary>
    internal class SlashBasic : ApplicationCommandModule
    {
        /// <summary>
        /// Represents a set of emoji slash commands.
        /// </summary>
        [SlashCommandGroup("emoji", "Envia um emoji animado aleatório! hehe.")]
        public class Emoji : ApplicationCommandModule
        {
            /// <summary>
            /// Represents the command to send a random animated emoji.
            /// </summary>
            [SlashCommand("any", "Envia qualquer emoji animado.")]
            internal static async Task ExecuteSlashRandomAnimatedEmoji(InteractionContext interactionContext)
            {
                await CommandBasic.ExecuteRandomAnimatedEmoji(interactionContext.Channel);
                await interactionContext.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource);
            }

            /// <summary>
            /// Represents the command to send a random animated meme emoji.
            /// </summary>
            [SlashCommand("meme", "Envia um emoji de meme animado.")]
            internal static async Task ExecuteSlashRandomAnimatedMemeEmoji(InteractionContext interactionContext)
            {
                await CommandBasic.ExecuteRandomAnimatedMemeEmoji(interactionContext.Channel);
                await interactionContext.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource);
            }

            /// <summary>
            /// Represents the command to send a random animated party emoji.
            /// </summary>
            [SlashCommand("party", "Envia um emoji de festa animado.")]
            internal static async Task ExecuteSlashRandomAnimatedPartyEmoji(InteractionContext interactionContext)
            {
                await CommandBasic.ExecuteRandomAnimatedPartyEmoji(interactionContext.Channel);
                await interactionContext.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource);
            }
        }
    }
}
