using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;

namespace KWiJisho.Commands.Prefix
{
    /// <summary>
    /// Provides a set of commands and classes for managing commands in a Discord server.
    /// </summary>
    internal partial class PrefixCommandManager
    {
        /// <summary>
        /// Represents a set of info prefix commands.
        /// </summary>
        internal class PrefixInfo : BaseCommandModule
        {
            /// <summary>
            /// Constant string that holds the further help message.
            /// </summary>
            internal const string furtherHelpMessage = @" Para receber detalhes sobre um comando digite ""help <nome do comando>"".";

            /// <summary>
            /// Represents the command to show help about the Discord bot commands.
            /// </summary>
            internal PrefixCommand help = new(nameof(help), $"Mostra a ajuda.{furtherHelpMessage}", Info);

            /// <summary>
            /// Represents the command to show information about the bot and the bot owner.
            /// </summary>
            internal PrefixCommand info = new(nameof(info), "Mostra informações básicas sobre mim e o meu criador.", Info);

            /// <summary>
            /// Represents the asynchronous prefix help method called when user asks for the prefix help command.
            /// </summary>
            /// <param name="commandContext">The command context from the current command call.</param>
            /// <returns>A <see cref="Task"/> from the current asynchronous task.</returns>
            [Command(nameof(help))]
            internal async Task GetHelpAsync(CommandContext commandContext) => await KWiJisho.Commands.Info.ExecuteHelpAsync(commandContext.Channel, commandContext.Client);

            /// <summary>
            /// Represents the asynchronous prefix info method called when user asks for the prefix info command.
            /// </summary>
            /// <param name="commandContext">The command context from the current command call.</param>
            /// <returns>A <see cref="Task"/> from the current asynchronous task.</returns>
            [Command(nameof(info))]
            internal async Task GetInfoAsync(CommandContext commandContext) => await KWiJisho.Commands.Info.ExecuteInfoAsync(commandContext.Channel, commandContext.Client);
        }
    }
}
