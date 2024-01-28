using DSharpPlus;
using DSharpPlus.SlashCommands;
using System.Reflection;
using System.Threading.Tasks;

namespace KWiJisho.Models.Commands.Slash
{
    public class Info : ApplicationCommandModule
    {
        [SlashCommand("help", "Mostra a ajuda.")]
        public static async Task GetHelpAsync(InteractionContext context)
        {
            await CommandManager.Info.GetHelpAsync(context);
            await context.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource);
        }
    }
}
