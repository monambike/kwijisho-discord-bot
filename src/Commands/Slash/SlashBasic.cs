// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus;
using DSharpPlus.SlashCommands;
using System.Threading.Tasks;

namespace KWiJisho.Commands.Slash
{
    /// <summary>
    /// Represents a set of basic slash commands.
    /// </summary>
    public class SlashBasic : ApplicationCommandModule
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
            public static async Task ExecuteSlashRandomAnimatedEmoji(InteractionContext interactionContext)
            {
                await CommandBasic.ExecuteRandomAnimatedEmoji(interactionContext.Channel);
                await interactionContext.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource);
            }

            /// <summary>
            /// Represents the command to send a random animated meme emoji.
            /// </summary>
            [SlashCommand("meme", "Envia um emoji de meme animado.")]
            public static async Task ExecuteSlashRandomAnimatedMemeEmoji(InteractionContext interactionContext)
            {
                await CommandBasic.ExecuteRandomAnimatedMemeEmoji(interactionContext.Channel);
                await interactionContext.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource);
            }

            /// <summary>
            /// Represents the command to send a random animated party emoji.
            /// </summary>
            [SlashCommand("party", "Envia um emoji de festa animado.")]
            public static async Task ExecuteSlashRandomAnimatedPartyEmoji(InteractionContext interactionContext)
            {
                await CommandBasic.ExecuteRandomAnimatedPartyEmoji(interactionContext.Channel);
                await interactionContext.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource);
            }
        }
    }
}
