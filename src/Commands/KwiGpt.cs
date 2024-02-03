using DSharpPlus.Entities;
using KWiJisho.APIs;
using System.Threading.Tasks;

namespace KWiJisho.Commands
{
    /// <summary>
    /// Provides methods for "prefix" and "slash" KWiGPT commands.
    /// </summary>
    internal static class KwiGpt
    {
        /// <summary>
        /// Represents the command to get the chat gpt prompt in KWiJisho style.
        /// </summary>
        internal static async Task ChatGptPromptAsync(DiscordChannel discordChannel, params string[] input)
        {
            // Getting all user inputs and combining into a string.
            string userInput = string.Join(" ", input);

            // Getting response from the prompt
            var response = await OpenAiApi.GetPromptKWiJishoStyleAsync(userInput);

            // Adding the prompt into a embed builder.
            var discordEmbedBuilder = new DiscordEmbedBuilder
            {
                Description = response,
                Color = ConfigJson.DefaultColor.DiscordColor
            };

            // Sending the response to the user.
            await discordChannel.SendMessageAsync(discordEmbedBuilder);
        }
    }
}
