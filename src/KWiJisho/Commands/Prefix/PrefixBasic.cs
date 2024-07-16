// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;

namespace KWiJisho.Commands.Prefix
{
    /// <summary>
    /// Provides a set of commands and classes for managing commands in a Discord server.
    /// </summary>
    public partial class PrefixCommandManager
    {
        /// <summary>
        /// Represents a set of basic prefix commands.
        /// </summary>
        public class PrefixBasic : BaseCommandModule
        {
            /// <summary>
            /// Represents the command to send a random animated emoji.
            /// </summary>
            public PrefixCommand emoji = new(nameof(emoji), "Envia um emoji animado aleatório! hehe", Basic);


            /// <summary>
            /// Represents the command to send a random animated meme emoji.
            /// </summary>
            public PrefixCommand emojim = new(nameof(emojim), "Envia um emoji de meme aleatório! hehe", Basic);

            /// <summary>
            /// Represents the command to send a random animated party emoji.
            /// </summary>
            public PrefixCommand emojip = new(nameof(emojip), "Envia um emoji de festa aleatório! hehe", Basic);

            /// <summary>
            /// Represents the asynchronous prefix send random animated emoji method called when sending a random animated emoji.
            /// </summary>
            /// <param name="commandContext">The command context from the current command call.</param>
            /// <returns>A <see cref="Task"/> from the current asynchronous task.</returns>
            [Command(nameof(emoji))]
            public async Task SendRandomAnimatedEmojiAsync(CommandContext commandContext) => await KWiJisho.Commands.CommandBasic.ExecuteRandomAnimatedEmojiAsync(commandContext.Channel);

            /// <summary>
            /// Represents the asynchronous prefix send random animated meme emoji method called when sending a random meme animated emoji.
            /// </summary>
            /// <param name="commandContext">The command context from the current command call.</param>
            /// <returns>A <see cref="Task"/> from the current asynchronous task.</returns>
            [Command(nameof(emojim))]
            public async Task SendRandomMemeEmojiAsync(CommandContext commandContext) => await KWiJisho.Commands.CommandBasic.ExecuteRandomAnimatedMemeEmojiAsync(commandContext.Channel);

            /// <summary>
            /// Represents the asynchronous prefix send random animated party emoji method called when sending a random party animated emoji.
            /// </summary>
            /// <param name="commandContext">The command context from the current command call.</param>
            /// <returns>A <see cref="Task"/> from the current asynchronous task.</returns>
            [Command(nameof(emojip))]
            public async Task SendRandomPartyEmojiAsync(CommandContext commandContext) => await KWiJisho.Commands.CommandBasic.ExecuteRandomAnimatedPartyEmojiAsync(commandContext.Channel);
        }
    }
}
