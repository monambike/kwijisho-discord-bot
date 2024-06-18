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
    internal partial class PrefixCommandManager
    {
        /// <summary>
        /// Represents a set of birthday prefix commands.
        /// </summary>
        internal class PrefixBirthday : BaseCommandModule
        {
            /// <summary>
            /// Represents the command to show the next person to have a birthday.
            /// </summary>
            internal PrefixCommand nextBirthday = new(nameof(nextBirthday), "Mostra o aniversário mais próximo de alguém presente no servidor e cadastrado na listinha de aniversariantes!", Birthday);

            /// <summary>
            /// Represents the command to list people will have birthday.
            /// </summary>
            internal PrefixCommand listBirthday = new(nameof(listBirthday), "Mostra a lista de aniversariantes de alguém presente no servidor e cadastrado na listinha de aniversariantes!", Birthday);

            /// <summary>
            /// Represents the asynchronous prefix get next birthday method called when requesting to show the next person to have a birthday.
            /// </summary>
            /// <param name="commandContext">The command context from the current command call.</param>
            /// <returns>A <see cref="Task"/> from the current asynchronous task.</returns>
            [Command(nameof(nextBirthday))]
            internal async Task GetNextBirthdayAsync(CommandContext commandContext) => await KWiJisho.Commands.CommandBirthday.ExecuteNextBirthdayAsync(commandContext.Channel, commandContext.Guild);

            /// <summary>
            /// Represents the asynchronous prefix get birthday list called when requesting to show the list of people to have a birthday.
            /// </summary>
            /// <param name="commandContext">The command context from the current command call.</param>
            /// <returns>A <see cref="Task"/> from the current asynchronous task.</returns>
            [Command(nameof(listBirthday))]
            internal async Task GetListBirthdayAsync(CommandContext commandContext) => await KWiJisho.Commands.CommandBirthday.ExecuteBirthdayListAsync(commandContext.Channel, commandContext.Guild);
        }
    }
}
