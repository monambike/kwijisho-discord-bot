using DSharpPlus;
using DSharpPlus.SlashCommands;
using System.Threading.Tasks;

namespace KWiJisho.Models.Commands.Slash
{
    internal class SlashInfo : ApplicationCommandModule
    {
        [SlashCommand("help", "Mostra a ajuda.")]
        internal static async Task GetHelpAsync(InteractionContext context)
        {
            await Info.GetHelpAsync(context.Channel, context.Client);
            await context.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource);
        }

        [SlashCommand("info", "Mostra informações básicas sobre mim e o meu criador. (@monambike)")]
        internal static async Task GetInfoAsync(InteractionContext context)
        {
            await Info.GetInfoAsync(context.Channel, context.Client);
            await context.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource);
        }
    }
}
