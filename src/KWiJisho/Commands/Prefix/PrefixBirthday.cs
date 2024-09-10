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
        /// Represents a set of birthday prefix commands.
        /// </summary>
        public class PrefixBirthday : BaseCommandModule
        {
            /// <summary>
            /// Represents the command to show the next person to have a birthday.
            /// </summary>
            public PrefixCommand nextBirthday = new(nameof(nextBirthday), "Mostra o aniversário mais próximo de alguém presente no servidor e cadastrado na listinha de aniversariantes!", Birthday);

            /// <summary>
            /// Represents the command to list people will have birthday.
            /// </summary>
            public PrefixCommand listBirthday = new(nameof(listBirthday), "Mostra a lista de aniversariantes de alguém presente no servidor e cadastrado na listinha de aniversariantes!", Birthday);

            /// <summary>
            /// Represents the asynchronous prefix get next birthday method called when requesting to show the next person to have a birthday.
            /// </summary>
            /// <param name="commandContext">The command context from the current command call.</param>
            /// <returns>A <see cref="Task"/> from the current asynchronous task.</returns>
            [Command(nameof(nextBirthday))]
            public async Task GetNextBirthdayAsync(CommandContext commandContext) => await CommandBirthday.CommandNextBirthdayAsync(commandContext.Channel, commandContext.Guild);

            /// <summary>
            /// Represents the asynchronous prefix get birthday list called when requesting to show the list of people to have a birthday.
            /// </summary>
            /// <param name="commandContext">The command context from the current command call.</param>
            /// <returns>A <see cref="Task"/> from the current asynchronous task.</returns>
            [Command(nameof(listBirthday))]
            public async Task GetListBirthdayAsync(CommandContext commandContext) => await CommandBirthday.CommandBirthdayListAsync(commandContext.Channel, commandContext.Guild);
        }
    }
}
