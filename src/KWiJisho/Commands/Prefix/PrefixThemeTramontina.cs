// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus;
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
        /// Represents a set of theme prefix commands.
        /// </summary>
        public class PrefixTheme
        {
            /// <summary>
            /// Represents a set of prefix commands to change theme designed specifically for Tramontina Discord server.
            /// </summary>
            public class PrefixThemeTramontina : BaseCommandModule
            {
                /// <summary>
                /// Represents the command to set the Default Theme.
                /// </summary>
                public PrefixCommand themeReset = new(nameof(themeReset), @"Define o servidor para o tema padrão.", Manage, Permissions.Administrator);

                /// <summary>
                /// Represents the command to set the Christmas Theme.
                /// </summary>
                public PrefixCommand themeChristmas = new(nameof(themeChristmas), @"Define o servidor para o tema de natal.", Manage, Permissions.Administrator);

                /// <summary>
                /// Represents the command to set the Easter Theme.
                /// </summary>
                public PrefixCommand themeEaster = new(nameof(themeEaster), @"Define o servidor para o tema de páscoa.", Manage, Permissions.Administrator);

                /// <summary>
                /// Represents the command to set the Halloween Theme.
                /// </summary>
                public PrefixCommand themeHalloween = new(nameof(themeHalloween), @"Define o servidor para o tema de halloween.", Manage, Permissions.Administrator);

                /// <summary>
                /// Represents the asynchronous prefix reset theme method called when user asks for the prefix reset theme command.
                /// </summary>
                /// <param name="commandContext">The command context from the current command call.</param>
                /// <returns>A <see cref="Task"/> from the current asynchronous task.</returns>
                [Command(nameof(themeReset))]
                [RequireUserPermissions(Permissions.Administrator)]
                public async Task ResetThemeAsync(CommandContext commandContext)
                    => await CommandThemeTramontina.ResetThemeAsync(commandContext.Channel, commandContext.Client);


                /// <summary>
                /// Represents the asynchronous prefix christmas theme method called when user asks for the prefix christmas theme command.
                /// </summary>
                /// <param name="commandContext">The command context from the current command call.</param>
                /// <returns>A <see cref="Task"/> from the current asynchronous task.</returns>
                [Command(nameof(themeChristmas))]
                [RequireUserPermissions(Permissions.Administrator)]
                public async Task SetChristmasThemeAsync(CommandContext commandContext)
                    => await CommandThemeTramontina.SetChristmasThemeAsync(commandContext.Channel, commandContext.Client);

                /// <summary>
                /// Represents the asynchronous prefix easter theme method called when user asks for the prefix easter theme command.
                /// </summary>
                /// <param name="commandContext">The command context from the current command call.</param>
                /// <returns>A <see cref="Task"/> from the current asynchronous task.</returns>
                [Command(nameof(themeEaster))]
                [RequireUserPermissions(Permissions.Administrator)]
                public async Task SetEasterThemeAsync(CommandContext commandContext)
                    => await CommandThemeTramontina.SetEasterThemeAsync(commandContext.Channel, commandContext.Client);

                /// <summary>
                /// Represents the asynchronous prefix halloween theme method called when user asks for the prefix halloween theme command.
                /// </summary>
                /// <param name="commandContext">The command context from the current command call.</param>
                /// <returns>A <see cref="Task"/> from the current asynchronous task.</returns>
                [Command(nameof(themeHalloween))]
                [RequireUserPermissions(Permissions.Administrator)]
                public async Task SetHalloweenThemeAsync(CommandContext commandContext)
                    => await CommandThemeTramontina.SetHalloweenThemeAsync(commandContext.Channel, commandContext.Client);
            }
        }
    }
}
