using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext;
using System.Threading.Tasks;
using DSharpPlus.SlashCommands;

namespace KWiJisho.Models.Commands.Prefix
{
    internal partial class PrefixCommandManager
    {
        internal class PrefixBasic : BaseCommandModule
        {
            internal PrefixCommand emoji = new(nameof(emoji), "Envia um emoji animado aleatório! hehe", Basic);
            [Command(nameof(emoji))]
            internal async Task SendRandomAnimatedEmoji(CommandContext commandContext) => await Commands.Basic.SendRandomAnimatedEmoji(commandContext.Channel);

            internal PrefixCommand emojim = new(nameof(emojim), "Envia um emoji de meme aleatório! hehe", Basic);
            [Command(nameof(emojim))]
            internal async Task SendRandomMemeEmoji(CommandContext commandContext) => await Commands.Basic.SendRandomAnimatedMemeEmoji(commandContext.Channel);

            internal PrefixCommand emojip = new(nameof(emojip), "Envia um emoji de festa aleatório! hehe", Basic);
            [Command(nameof(emojip))]
            internal async Task SendRandomPartyEmoji(CommandContext commandContext) => await Commands.Basic.SendRandomAnimatedPartyEmoji(commandContext.Channel);
        }
    }
}
