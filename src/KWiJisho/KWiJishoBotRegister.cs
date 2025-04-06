// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus;
using DSharpPlus.CommandsNext;
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
            DiscordClient.MessageCreated += DiscordMessageHandler.OnMessageCreatedAsync;
        }

        /// <summary>
        /// Register all the Discord bot prefix commands permissions.
        /// </summary>
        public void RegisterPrefixCommandEvents()
        {
            // Permissions
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

            // Log
            PrefixCommands.CommandExecuted += async (sender, e) =>
            {
                var commandName = ConfigJson.Prefix + e.Context.Command.Name;

                var logContext = CreatePrefixCommandLogContext(e.Context, commandName);

                await Logs.DefaultLog.AddInfoAsync(Log.Module.CommandExecution, logContext, $@"Executed ""{commandName}"" prefix command.");
            };
        }

        /// <summary>
        /// Register all the Discord bot slash commands permissions.
        /// </summary>
        public void RegisterSlashCommandEvents()
        {
            // Permissions
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

            // Log
            slash.SlashCommandInvoked += async (sender, e) =>
            {
                var commandName = $"/{e.Context.CommandName}";

                var logContext = CreateSlashCommandLogContext(e.Context, commandName);

                await Logs.DefaultLog.AddInfoAsync(Log.Module.CommandExecution, logContext, $@"Executing ""{commandName}"" slash command...");
            };

            slash.SlashCommandExecuted += async (sender, e) =>
            {
                var commandName = $"/{e.Context.CommandName}";

                var logContext = CreateSlashCommandLogContext(e.Context, commandName);

                await Logs.DefaultLog.AddInfoAsync(Log.Module.CommandExecution, logContext, $@"Executed ""{commandName}"" slash command.");
            };
        }

        /// <summary>
        /// Creates a <see cref="LogContext"/> for a prefix command execution.
        /// </summary>
        /// <param name="commandContext">The context of the prefix command.</param>
        /// <param name="commandName">The name of the executed command.</param>
        /// <returns>A configured <see cref="LogContext"/> for logging.</returns>
        internal static LogContext CreatePrefixCommandLogContext(CommandContext commandContext, string commandName) =>
            CreateLogContext(commandContext.Guild.Id, commandContext.User.Id, commandName, "Prefix Commands");

        /// <summary>
        /// Creates a <see cref="LogContext"/> for a slash command execution.
        /// </summary>
        /// <param name="interactionContext">The context of the slash command interaction.</param>
        /// <param name="commandName">The name of the executed command.</param>
        /// <returns>A configured <see cref="LogContext"/> for logging.</returns>
        internal static LogContext CreateSlashCommandLogContext(InteractionContext interactionContext, string commandName) =>
            CreateLogContext(interactionContext.Guild.Id, interactionContext.User.Id, commandName, "Slash Commands");

        /// <summary>
        /// Create log context for prefix and slash commands.
        /// </summary>
        /// <param name="guildId">The ID of the guild where the command was executed.</param>
        /// <param name="userId">The ID of the user who executed the command.</param>
        /// <param name="commandName">The name of the executed command.</param>
        /// <param name="action">The type or context of the command (e.g., "Prefix Commands", "Slash Commands").</param>
        /// <returns>A configured <see cref="LogContext"/> instance.</returns>
        internal static LogContext CreateLogContext(ulong guildId, ulong userId, string commandName, string action) =>
            new()
            {
                IssuerId = userId.ToString(),
                GuildId = guildId.ToString(),
                Action = commandName,
                ContextType = action
            };
    }
}
