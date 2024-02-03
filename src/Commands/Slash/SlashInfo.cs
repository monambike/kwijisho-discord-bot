using DSharpPlus;
using DSharpPlus.SlashCommands;
using System.Threading.Tasks;

namespace KWiJisho.Commands.Slash
{
    /// <summary>
    /// Represents a set of informational and helpful slash commands.
    /// </summary>
    internal class SlashInfo : ApplicationCommandModule
    {
        /// <summary>
        /// Represents the command to show help about the Discord bot commands.
        /// </summary>
        [SlashCommand("help", "Mostra a ajuda.")]
        internal static async Task GetHelpAsync(InteractionContext context)
        {
            await Info.GetHelpAsync(context.Channel, context.Client);
            await context.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource);
        }

        /// <summary>
        /// Represents the command to show information about the bot and the bot owner.
        /// </summary>
        [SlashCommand("info", "Mostra informações básicas sobre mim e o meu criador. (@monambike)")]
        internal static async Task GetInfoAsync(InteractionContext context)
        {
            await Info.GetInfoAsync(context.Channel, context.Client);
            await context.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource);
        }
    }
}
