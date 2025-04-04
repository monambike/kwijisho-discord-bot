// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;
using KWiJisho.Commands.Prefix;
using KWiJisho.Commands.Slash;
using KWiJisho.Config;
using KWiJisho.Data;
using KWiJisho.Events;
using KWiJisho.Utils;

namespace KWiJisho
{
    /// <summary>
    /// Main class responsible for managing and instantiate the KWiJisho Discord bot.
    /// </summary>
    public partial class KWiJishoBot
    {
        /// <summary>
        /// Register all the Discord bot prefix commands.
        /// </summary>
        public void RegisterPrefixCommands()
        {
            // Registering discord bot prefix commands.
            PrefixCommands.RegisterCommands<PrefixCommandManager.PrefixBasic>();
            PrefixCommands.RegisterCommands<PrefixCommandManager.PrefixBirthday>();
            PrefixCommands.RegisterCommands<PrefixCommandManager.PrefixInfo>();
            PrefixCommands.RegisterCommands<PrefixCommandManager.PrefixKwiGpt>();
            PrefixCommands.RegisterCommands<PrefixCommandManager.PrefixNasa>();
            PrefixCommands.RegisterCommands<PrefixCommandManager.PrefixNotice>();
            PrefixCommands.RegisterCommands<PrefixCommandManager.PrefixTheme.PrefixThemeTramontina>();
        }
        
        /// <summary>
        /// Register all the Discord bot slash commands.
        /// </summary>
        public void RegisterSlashCommands()
        {
            // Registering discord bot slash commands for every server.
            RegisterSlashCommandsToServer();

#if DEBUG
            // Registering discord bot slash commands for test server.
            RegisterSlashCommandsToServer(Servers.Personal.GuildId);
#endif
        }

        /// <summary>
        /// Register all the Discord bot slash commands to a server.
        /// </summary>
        private void RegisterSlashCommandsToServer(ulong? guildId = null)
        {
            // Registering discord bot slash commands.
            SlashCommands.RegisterCommands<SlashBasic>(guildId);
            SlashCommands.RegisterCommands<SlashBirthday>(guildId);
            SlashCommands.RegisterCommands<SlashConfig>(guildId);
            SlashCommands.RegisterCommands<SlashInfo>(guildId);
            SlashCommands.RegisterCommands<SlashKwiGpt>(guildId);
            SlashCommands.RegisterCommands<SlashNasa>(guildId);
            SlashCommands.RegisterCommands<SlashNotice>(guildId);
            SlashCommands.RegisterCommands<SlashThemeTramontina>(guildId);
        }

        /// <summary>
        /// Register all the Discord bot events.
        /// </summary>
        /// <returns></returns>
        public void RegisterBotEvents()
        {
            // Registering other Discord bot events.
            DiscordClient.ComponentInteractionCreated += DiscordComponentHandler.OnComponentInteractionCreatedAsync;
            DiscordClient.GuildMemberAdded += DiscordGuildHandler.OnGuildMemberAddedAsync;
            DiscordClient.GuildMemberRemoved += DiscordGuildHandler.OnGuildMemberRemovedAsync;
            DiscordClient.Ready += (sender, e) => DiscordReadyHandler.OnReadyAsync(sender, e, DiscordClient);
        }

        /// <summary>
        /// Register all the Discord bot prefix commands permissions.
        /// </summary>
        public void RegisterPrefixCommandsPermissions()
        {
            PrefixCommands.CommandErrored += async (sender, e) =>
            {
                // Checking if the exception is a checks failed exception.
                if (e.Exception is ChecksFailedException checkFailedException)
                {
                    // Iterate over the failed checks.
                    foreach (var check in checkFailedException.FailedChecks)
                    {
                        // Check if the failed check is a require user permissions attribute.
                        if (check is RequireUserPermissionsAttribute requireUserPermissionAttribute)
                        {
                            // Send a custom error message to the user.
                            await e.Context.RespondAsync(KWiJishoPermission.PermissionCustomErrorMessage(requireUserPermissionAttribute.Permissions));
                        }
                    }
                }
            };

            PrefixCommands.CommandExecuted += async (sender, e) =>
            {
                var logContext = new LogContext
                {
                    IssuerId = e.Context.Member.Id.ToString(),
                    GuildId = e.Context.Guild.Id.ToString(),
                    Action = ConfigJson.Prefix + e.Context.Command.Name,
                    ContextType = "Prefix Command"
                };

                await Logs.DefaultLog.AddInfoAsync(Log.Module.KWiJisho, logContext, "Executed prefix command.");
            };
        }

        /// <summary>
        /// Register all the Discord bot slash commands permissions.
        /// </summary>
        public void RegisterSlashCommandsPermissions()
        {
            SlashCommandsExtension slash = SlashCommands;
            slash.SlashCommandErrored += async (sender, e) =>
            {
                // Checking if the exception is a slash execution checks failed exception.
                if (e.Exception is SlashExecutionChecksFailedException slashExecutionChecksFailedException)
                {
                    // Iterate over the failed checks
                    foreach (var check in slashExecutionChecksFailedException.FailedChecks)
                    {
                        // Check if the failed check is a slash require user permissions attribute.
                        if (check is SlashRequireUserPermissionsAttribute slashRequireUserPermissionsAttribute)
                        {
                            // Send a custom error message to the user.
                            await e.Context.Channel.SendMessageAsync(KWiJishoPermission.PermissionCustomErrorMessage(slashRequireUserPermissionsAttribute.Permissions));
                        }
                    }
                }
            };

            slash.SlashCommandExecuted += async (sender, e) =>
            {
                var logContext = new LogContext
                {
                    IssuerId = e.Context.Member.Id.ToString(),
                    GuildId = e.Context.Guild.Id.ToString(),
                    Action = e.Context.CommandName,
                    ContextType = "Slash Command"
                };

                await Logs.DefaultLog.AddInfoAsync(Log.Module.KWiJisho, logContext, "Executed slash command.");
            };
        }
    }
}
