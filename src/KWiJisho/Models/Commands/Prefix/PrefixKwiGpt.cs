using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.SlashCommands;
using System.Threading.Tasks;

namespace KWiJisho.Models.Commands.Prefix
{
    internal partial class PrefixCommandManager
    {
        internal class PrefixKwiGpt : BaseCommandModule
        {
            const string kDescription = "Manda qualquer coisinha na frente que eu respondo alá ChatGPT! Conversa comigo!!";
            internal PrefixCommand k = new(nameof(k), kDescription, ChatGpt);
            [Command(nameof(k))]
            [SlashCommand(nameof(k), kDescription)]
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
