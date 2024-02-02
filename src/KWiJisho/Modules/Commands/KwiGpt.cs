using DSharpPlus.Entities;
using KWiJisho.Modules.Api;
using System.Threading.Tasks;

namespace KWiJisho.Modules.Commands
{
    internal static class KwiGpt
    {
        internal static async Task ChatGptPromptAsync(DiscordChannel discordChannel, params string[] input)
        {
            // Getting all user inputs and combining into a string
            string userInput = string.Join(" ", input);

            // Getting response from the prompt
            var response = await OpenAiApi.GetPromptKWiJishoStyleAsync(userInput);

            // Adding the prompt into a embed builder
            var discordEmbedBuilder = new DiscordEmbedBuilder
            {
                Description = response,
                Color = ConfigJson.DefaultColor.DiscordColor
            };

            // Sending the response to the user
            await discordChannel.SendMessageAsync(discordEmbedBuilder);
        }
    }
}
