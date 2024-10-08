﻿// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
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
        /// Represents a set of KWiGPT prefix commands.
        /// </summary>
        public class PrefixKwiGpt : BaseCommandModule
        {
            /// <summary>
            /// Represents the command to get the chat gpt prompt in KWiJisho style.
            /// </summary>
            public PrefixCommand k = new(nameof(k), "Manda qualquer coisinha na frente que eu respondo alá ChatGPT! Conversa comigo!!", ChatGpt);

            /// <summary>
            /// Represent the asynchronous prefix get chat gpt prompt async method called when user uses the
            /// command to interact with the bot with ChatGpt in KWiJisho style.
            /// </summary>
            /// <param name="commandContext">The command context from the current command call.</param>
            /// <param name="inputs">The user input for getting the chat prompt</param>
            /// <returns>A <see cref="Task"/> from the current asynchronous task.</returns>
            [Command(nameof(k))]
            public async Task GetChatGptPromptAsync(CommandContext commandContext, params string[] inputs)
            {
                // Show's that the bot is "typing" while it process everything.
                await commandContext.TriggerTypingAsync();

                // Calling the method that will execute the prompt.
                await CommandKwiGpt.ExecuteKWiJishoPromptAsync(commandContext.Channel, inputs);
            }
        }
    }
}
