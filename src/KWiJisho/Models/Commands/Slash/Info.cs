using DSharpPlus.Entities;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using System.Threading.Tasks;

namespace KWiJisho.Models.Commands.Slash
{
    public class Info : ApplicationCommandModule
    {

        [SlashCommand("help", "Comando de ajuda.")]
        public async Task GetHelpAsync(InteractionContext context)
        {
            await new CommandManager.Info().GetHelpAsync(context);
            await context.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource);
        }
    }
}
