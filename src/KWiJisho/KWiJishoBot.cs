﻿// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.SlashCommands;
using KWiJisho.Config;
using KWiJisho.Data;
using KWiJisho.Scheduling;
using KWiJisho.Utils;
using System.Threading.Tasks;

namespace KWiJisho
{
    /// <summary>
    /// Main class responsible for managing and instantiate the KWiJisho Discord bot. The main entry
    /// point class of the application.
    /// </summary>
    public partial class KWiJishoBot
    {
        /// <summary>
        /// Gets or sets the Discord client instance used by the bot.
        /// </summary>
        public DiscordClient DiscordClient { get; private set; } = null!;

        /// <summary>
        /// Gets or sets the <see cref="CommandsNextExtension"/> instance for handling bot prefix commands.
        /// </summary>
        public CommandsNextExtension PrefixCommands { get; private set; } = null!;

        /// <summary>
        /// Gets or sets the <see cref="SlashCommandsExtension"/> instance for handling bot slash commands.
        /// </summary>
        public SlashCommandsExtension SlashCommands { get; private set; } = null!;

        /// <summary>
        /// The log context.
        /// </summary>
        public LogContext LogContext { get; private set; } = new LogContext
        {
            Action = "Startup",
            ContextType = "System",
            GuildId = Servers.Personal.GuildId,
            IssuerId = Data.KWiJisho.Id
        };

        /// <summary>
        /// The main entry point method of the application.
        /// </summary>
        public static void Main()
        {
            // Creating a new instance of the Discord bot class.
            var bot = new KWiJishoBot();

            // Initializing the Discord bot application and waiting for the inifinite task
            // result on the awaiter.
            bot.RunAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// Asynchronously runs the bot connecting it to the Discord and initializing
        /// the necessary configurations.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task RunAsync()
        {
            // Gets info from Json file and setting into the ConfigJson class.
            var configJson = await Entities.ConfigJson.DeserializeConfigJsonFileAsync();
            ConfigJson.SetValuesFromConfigJson(configJson);

            // Sets Discord bot settings for DiscordClient.
            var discordConfiguration = new DiscordConfiguration
            {
                Token = ConfigJson.KWiJishoToken,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Debug,
                Intents = DiscordIntents.All
            };

            // Defines and registering the Discord bot configuration and events.
            DiscordClient = new DiscordClient(discordConfiguration);
            RegisterBotEvents();

            // Defines bot prefix commands settings.
            var commandsNextConfiguration = new CommandsNextConfiguration
            {
                StringPrefixes = [ConfigJson.Prefix],
                EnableDms = false,
                EnableMentionPrefix = true,
                EnableDefaultHelp = false
            };

            // Defines and registering the Discord bot prefix commands.
            PrefixCommands = DiscordClient.UseCommandsNext(commandsNextConfiguration);
            RegisterPrefixCommands();
            RegisterPrefixCommandEvents();

            // Defines and registering the Discord bot slash commands.
            SlashCommands = DiscordClient.UseSlashCommands();
            RegisterSlashCommands();
            RegisterSlashCommandEvents();

            // Creates all schedulers for application jobs.
            await Scheduler.CreateAllSchedulersAsync(DiscordClient);

            // Instantiates all application logs.
            Logs.InstantiateAllLogs(DiscordClient);

            // Connects the bot into the Discord.
            await DiscordClient.ConnectAsync();

            // This code that will be executed when the bot is ready and connected to Discord sending.
            // a message into my personal server
            await Logs.DefaultLog.AddInfoAsync(Log.Module.System, LogContext, $"Initializing the {Data.KWiJisho.Name} Discord bot application...");
            var channel = await DiscordClient.GetChannelAsync(Servers.Personal.WelcomeChannelId);
            if (channel != null) await channel.SendMessageAsync("Olá!! Agora eu tô online e prontíssima pra ajudar! 🥳🎉🎉");

            // Keeps the task alive that effectively never completes, preventing the bot from
            // disconnecting when method ends.
            await Task.Delay(-1);
        }
    }
}
