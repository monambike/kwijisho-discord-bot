using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using KWiJisho.Models.Apis;
using System.Threading.Tasks;

namespace KWiJisho.Models.Commands
{
    internal partial class CommandManager
    {
        internal class KwiGpt : BaseCommandModule
        {
            internal Command k = new(nameof(k), $@"Manda qualquer coisinha na frente que eu respondo alá ChatGPT! Conversa comigo!!", ChatGptGroup);
            [Command(nameof(k))]

            internal async Task ChatGptPromptAsync(CommandContext commandContext, params string[] inputs)
            {
                // Getting all user inputs and combining into a string
                string userInput = string.Join(" ", inputs);
                // Show's that the bot is "typing" while it process everything
                await commandContext.TriggerTypingAsync();

                // Getting response from the prompt
                var response = await OpenAiApi.GetPromptKWiJishoStyleAsync(userInput);

                // Adding the prompt into a embed builder
                var discordEmbedBuilder = new DiscordEmbedBuilder
                {
                    Description = response,
                    Color = ConfigJson.DefaultColor.DiscordColor
                };

                // Sending the response to the user
                await commandContext.Channel.SendMessageAsync(discordEmbedBuilder);
            }
        }
    }
}
