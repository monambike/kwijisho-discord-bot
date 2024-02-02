using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;

namespace KWiJisho.Modules.Commands.Prefix
{
    internal partial class PrefixCommandManager
    {
        /// <summary>
        /// Represents a set of info prefix commands.
        /// </summary>
        internal class PrefixInfo : BaseCommandModule
        {
            /// <summary>
            /// Represents the command to show help about the Discord bot commands.
            /// </summary>
            internal const string furtherHelpDetailsMessage = @" Para receber detalhes sobre um comando digite ""help <nome do comando>"".";
            internal PrefixCommand help = new(nameof(help), $"Mostra a ajuda.{furtherHelpDetailsMessage}", Info);
            [Command(nameof(help))]
            internal async Task GetHelpAsync(CommandContext commandContext) => await Commands.Info.GetHelpAsync(commandContext.Channel, commandContext.Client);

            /// <summary>
            /// Represents the command to show information about the bot and the bot owner.
            /// </summary>
            internal PrefixCommand info = new(nameof(info), "Mostra informações básicas sobre mim e o meu criador.", Info);
            [Command(nameof(info))]
            internal async Task GetInfoAsync(CommandContext commandContext) => await Commands.Info.GetInfoAsync(commandContext.Channel, commandContext.Client);
        }
    }
}
