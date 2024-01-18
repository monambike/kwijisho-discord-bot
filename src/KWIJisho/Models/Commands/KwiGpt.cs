﻿using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using KWIJisho.Models.Apis;
using System.Threading.Tasks;

namespace KWIJisho.Models.Commands
{
    internal partial class CommandManager
    {
        internal class KwiGpt : BaseCommandModule
        {
            internal Command kwigpt = new(nameof(kwigpt), $@"Manda o comendo com o que quiser na frente que eu respondo alá chat gpt!", ChatGptGroup);
            [Command(nameof(kwigpt))]

            internal async Task ChatGptPrompt(CommandContext commandContext, params string[] inputs)
            {
                // Getting all user inputs and combining into a string
                string userInput = string.Join(" ", inputs);
                // Show's that the bot is "typing" while it process everything
                await commandContext.TriggerTypingAsync();

                // Getting response from the prompt
                var response = await OpenAiApi.GetPromptKWIJishoStyleAsync(userInput);

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