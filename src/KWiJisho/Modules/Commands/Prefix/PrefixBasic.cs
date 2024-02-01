using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;

namespace KWiJisho.Modules.Commands.Prefix
{
    internal partial class PrefixCommandManager
    {
        /// <summary>
        /// Represents a set of basic prefix commands.
        /// </summary>
        internal class PrefixBasic : BaseCommandModule
        {
            /// <summary>
            /// Represents the command to send a random animated emoji.
            /// </summary>
            internal PrefixCommand emoji = new(nameof(emoji), "Envia um emoji animado aleatório! hehe", Basic);
            [Command(nameof(emoji))]
            internal async Task SendRandomAnimatedEmoji(CommandContext commandContext) => await Commands.Basic.SendRandomAnimatedEmoji(commandContext.Channel);

            /// <summary>
            /// Represents the command to send a random animated meme emoji.
            /// </summary>
            internal PrefixCommand emojim = new(nameof(emojim), "Envia um emoji de meme aleatório! hehe", Basic);
            [Command(nameof(emojim))]
            internal async Task SendRandomMemeEmoji(CommandContext commandContext) => await Commands.Basic.SendRandomAnimatedMemeEmoji(commandContext.Channel);

            /// <summary>
            /// Represents the command to send a random animated party emoji.
            /// </summary>
            internal PrefixCommand emojip = new(nameof(emojip), "Envia um emoji de festa aleatório! hehe", Basic);
            [Command(nameof(emojip))]
            internal async Task SendRandomPartyEmoji(CommandContext commandContext) => await Commands.Basic.SendRandomAnimatedPartyEmoji(commandContext.Channel);
        }
    }
}
