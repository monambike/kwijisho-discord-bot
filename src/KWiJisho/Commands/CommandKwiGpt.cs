// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus.Entities;
using KWiJisho.APIs;
using KWiJisho.Config;
using System.Threading.Tasks;

namespace KWiJisho.Commands
{
    /// <summary>
    /// Provides methods for KWiGPT prefix and slash commands.
    /// </summary>
    public static class CommandKwiGpt
    {
        /// <summary>
        /// Represents the command to get the chat gpt prompt in KWiJisho style.
        /// </summary>
        /// <param name="discordChannel">The Discord channel where the prompt will be sent.</param>
        /// <param name="input">The content of user input.</param>
        /// <returns>A <see cref="Task"/> with the current asynchronous method.</returns>
        public static async Task ExecuteKWiJishoPromptAsync(DiscordChannel discordChannel, params string[] input)
        {
            // Getting all user inputs and combining into a string.
            string userInput = string.Join(" ", input);

            // Getting response from the prompt
            var response = await ChatGPT.GetKWiJishoPromptAsync(userInput);

            // Sending the response to the user.
            await discordChannel.SendMessageAsync(response);
        }
    }
}
