using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System;
using System.Threading.Tasks;

namespace KWiJisho.Models.Commands.Slash
{
    internal class SlashKwiGpt : ApplicationCommandModule
    {
        [SlashCommand("k", "Manda qualquer coisinha na frente que eu respondo alá ChatGPT! Conversa comigo!!")]
        internal static async Task ChatGptPromptAsync(InteractionContext interactionContext, [Option("seu-texto", "Qualquer coisa! Manda um textinho que você quiser que eu responda <3")] string input)
        {
            // Show's that the bot is "processing" while it process everything
            await interactionContext.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

            // Get's the prompt and delete the "processing" message
            await KwiGpt.ChatGptPromptAsync(interactionContext.Channel, input);
            await interactionContext.DeleteResponseAsync();
        }
    }
}
