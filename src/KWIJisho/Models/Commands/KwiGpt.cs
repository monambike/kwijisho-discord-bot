using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using OpenAI_API;
using System;
using System.Threading.Tasks;

namespace KWIJisho.Models.Commands
{
    internal partial class CommandManager
    {
        internal class KwiGpt : BaseCommandModule
        {
            internal Command kwigpt = new("kwigpt", $@"Manda o comendo com o que quiser na frente que eu respondo alá chat gpt!", ChatGptGroup);
            [Command(nameof(kwigpt))]

            internal async Task ChatGptPrompt(CommandContext commandContext, params string[] inputs)
            {
                // Getting all user inputs and combining into a string
                string userInput = string.Join(" ", inputs);
                // Show's that the bot is "typing" while it process everything
                await commandContext.TriggerTypingAsync();

                // Getting chat gpt token
                var api = new OpenAIAPI(ConfigJson.ChatGptToken);

                var chat = api.Chat.CreateConversation();
                chat.AppendSystemMessage("Aja alegre e animada, falando de um jeito descontraído e se possível com emojis. Nada de formalidade, pontos finais e capitalizar o início das palavras.");
                chat.AppendUserInput(userInput);

                var response = await chat.GetResponseFromChatbotAsync();
                var discordEmbedBuilder = new DiscordEmbedBuilder
                {
                    Description = response,
                    Color = ConfigJson.DefaultColor.DiscordColor
                };

                await commandContext.Channel.SendMessageAsync(discordEmbedBuilder);
            }
        }
    }
}
