using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using KWiJisho.Models.Utils;
using System;
using System.IO;
using System.Threading.Tasks;

namespace KWiJisho.Models.Commands.Prefix
{
    internal partial class PrefixCommandManager
    {
        internal class PrefixInfo : BaseCommandModule
        {
            internal const string furtherHelpDetailsMessage = @" Para receber detalhes sobre um comando digite ""help <nome do comando>"".";
            internal PrefixCommand help = new(nameof(help), $"Mostra a ajuda.{furtherHelpDetailsMessage}", Info);
            [Command(nameof(help))]
            internal async Task GetHelpAsync(CommandContext commandContext) => await Commands.Info.GetHelpAsync(commandContext.Channel, commandContext.Client);

            internal PrefixCommand info = new(nameof(info), "Mostra informações básicas sobre mim e o meu criador.", Info);
            [Command(nameof(info))]
            internal async Task GetInfoAsync(CommandContext commandContext) => await Commands.Info.GetInfoAsync(commandContext.Channel, commandContext.Client);
        }
    }
}
