using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using KWiJisho.Entities;
using System.Threading.Tasks;

namespace KWiJisho.Commands.Prefix
{
    /// <summary>
    /// Provides a set of commands and classes for managing commands in a Discord server.
    /// </summary>
    internal partial class PrefixCommandManager
    {
        /// <summary>
        /// Represents a set of theme prefix commands.
        /// </summary>
        internal class PrefixTheme
        {
            /// <summary>
            /// Represents a set of prefix commands to change theme designed specifically for Tramontina Discord server.
            /// </summary>
            internal class PrefixThemeTramontina : BaseCommandModule
            {
                /// <summary>
                /// Represents the command to set the Default Theme.
                /// </summary>
                internal PrefixCommand themeReset = new(nameof(themeReset), @"Define o servidor para o tema padrão.", Manage, Permissions.Administrator);

                /// <summary>
                /// Represents the command to set the Christmas Theme.
                /// </summary>
                internal PrefixCommand themeChristmas = new(nameof(themeChristmas), @"Define o servidor para o tema de natal.", Manage, Permissions.Administrator);

                /// <summary>
                /// Represents the command to set the Easter Theme.
                /// </summary>
                internal PrefixCommand themeEaster = new(nameof(themeEaster), @"Define o servidor para o tema de páscoa.", Manage, Permissions.Administrator);

                /// <summary>
                /// Represents the command to set the Halloween Theme.
                /// </summary>
                internal PrefixCommand themeHalloween = new(nameof(themeHalloween), @"Define o servidor para o tema de halloween.", Manage, Permissions.Administrator);
            
                [Command(nameof(themeReset))]
                [RequireUserPermissions(Permissions.Administrator)]
                internal async Task ResetThemeAsync(CommandContext commandContext)
                    => await ThemeTramontina.ResetThemeAsync(commandContext.Channel, commandContext.Client);


                [Command(nameof(themeChristmas))]
                [RequireUserPermissions(Permissions.Administrator)]
                internal async Task SetChristmasThemeAsync(CommandContext commandContext)
                    => await ThemeTramontina.SetChristmasThemeAsync(commandContext.Channel, commandContext.Client);

                [Command(nameof(themeEaster))]
                [RequireUserPermissions(Permissions.Administrator)]
                internal async Task SetEasterThemeAsync(CommandContext commandContext)
                    => await ThemeTramontina.SetEasterThemeAsync(commandContext.Channel, commandContext.Client);

                [Command(nameof(themeHalloween))]
                [RequireUserPermissions(Permissions.Administrator)]
                internal async Task SetHalloweenThemeAsync(CommandContext commandContext)
                    => await ThemeTramontina.SetHalloweenThemeAsync(commandContext.Channel, commandContext.Client);
            }
        }
    }
}
