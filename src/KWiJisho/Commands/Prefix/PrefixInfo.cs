// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;

namespace KWiJisho.Commands.Prefix
{
    /// <summary>
    /// Provides a set of commands and classes for managing commands in a Discord server.
    /// </summary>
    public partial class PrefixCommandManager
    {
        /// <summary>
        /// Represents a set of info prefix commands.
        /// </summary>
        public class PrefixInfo : BaseCommandModule
        {
            /// <summary>
            /// Constant string that holds the further help message.
            /// </summary>
            public const string furtherHelpMessage = @" Para receber detalhes sobre um comando digite ""help <nome do comando>"".";

            /// <summary>
            /// Represents the command to show help about the Discord bot commands.
            /// </summary>
            public PrefixCommand help = new(nameof(help), $"Mostra a ajuda.{furtherHelpMessage}", Info);

            /// <summary>
            /// Represents the command to show information about the bot and the bot owner.
            /// </summary>
            public PrefixCommand info = new(nameof(info), "Mostra informações básicas sobre mim e o meu criador.", Info);

            /// <summary>
            /// Represents the asynchronous prefix help method called when user asks for the prefix help command.
            /// </summary>
            /// <param name="commandContext">The command context from the current command call.</param>
            /// <returns>A <see cref="Task"/> from the current asynchronous task.</returns>
            [Command(nameof(help))]
            public async Task GetHelpAsync(CommandContext commandContext) => await Commands.CommandInfo.ExecuteHelpAsync(commandContext.Channel, commandContext.Client);

            /// <summary>
            /// Represents the asynchronous prefix info method called when user asks for the prefix info command.
            /// </summary>
            /// <param name="commandContext">The command context from the current command call.</param>
            /// <returns>A <see cref="Task"/> from the current asynchronous task.</returns>
            [Command(nameof(info))]
            public async Task GetInfoAsync(CommandContext commandContext) => await Commands.CommandInfo.ExecuteInfoAsync(commandContext.Channel, commandContext.Client);
        }
    }
}
