using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.SlashCommands;
using System.Threading.Tasks;

namespace KWiJisho.Modules.Commands.Prefix
{
    internal partial class PrefixCommandManager
    {
        /// <summary>
        /// Represents a set of KWiGpt prefix commands. Interaction commands with ChatGpt
        /// in KWiJisho bot style.
        /// </summary>
        internal class PrefixKwiGpt : BaseCommandModule
        {
            /// <summary>
            /// Represent the command to interact with the bot with ChatGpt in KWiJisho style.
            /// </summary>
            internal PrefixCommand k = new(nameof(k), "Manda qualquer coisinha na frente que eu respondo alá ChatGPT! Conversa comigo!!", ChatGpt);
            [Command(nameof(k))]
            internal async Task ChatGptPromptAsync(CommandContext commandContext, params string[] inputs)
            {
                // Show's that the bot is "typing" while it process everything
                await commandContext.TriggerTypingAsync();

                // Calling the method that will execute the prompt
                await KwiGpt.ChatGptPromptAsync(commandContext.Channel, inputs);
            }
        }
    }
}
