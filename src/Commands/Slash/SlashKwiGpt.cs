using DSharpPlus;
using DSharpPlus.SlashCommands;
using System.Threading.Tasks;

namespace KWiJisho.Commands.Slash
{
    /// <summary>
    /// Represents a set of KWiGpt slash commands. Interaction commands with ChatGpt
    /// in KWiJisho bot style.
    /// </summary>
    internal class SlashKwiGpt : ApplicationCommandModule
    {
        /// <summary>
        /// Represent the command to interact with the bot with ChatGpt in KWiJisho style.
        /// </summary>
        [SlashCommand("k", "Manda qualquer coisinha na frente que eu respondo alá ChatGPT! Conversa comigo!!")]
        internal static async Task ChatGptPromptAsync(InteractionContext interactionContext, [Option("seu-texto", "Qualquer coisa! Manda um textinho que você quiser que eu responda <3")] string input)
        {
            // Show's that the bot is "processing" while it process everything.
            await interactionContext.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

            // Get's the prompt and delete the "processing" message.
            await KwiGpt.ChatGptPromptAsync(interactionContext.Channel, input);
            await interactionContext.DeleteResponseAsync();
        }
    }
}
