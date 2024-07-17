// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
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
        /// Initializing Discord bot activity class.
        /// </summary>
        public static DiscordActivity DiscordActivity => new(ConfigJson.Activity);

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
            // Getting info from Json file and setting into the ConfigJson class.
            var configJson = await Entities.ConfigJson.DeserializeConfigJsonFileAsync();
            ConfigJson.SetValuesFromConfigJson(configJson);

            // Settings Discord bot settings for DiscordClient.
            var discordConfiguration = new DiscordConfiguration
            {
                Token = ConfigJson.KWiJishoToken,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Debug,
                Intents = DiscordIntents.All
            };

            // Defining and registering the Discord bot configuration and events.
            DiscordClient = new DiscordClient(discordConfiguration);
            RegisterBotEvents();

            // Defining bot prefix commands settings.
            var commandsNextConfiguration = new CommandsNextConfiguration
            {
                StringPrefixes = [ConfigJson.Prefix],
                EnableDms = false,
                EnableMentionPrefix = true,
                EnableDefaultHelp = false
            };

            // Defining and registering the Discord bot prefix commands.
            PrefixCommands = DiscordClient.UseCommandsNext(commandsNextConfiguration);
            RegisterPrefixCommands();
            RegisterPrefixCommandsPermissions();

            // Defining and registering the Discord bot slash commands.
            SlashCommands = DiscordClient.UseSlashCommands();
            RegisterSlashCommands();
            RegisterSlashCommandsPermissions();

            // Creating all schedulers for application jobs.
            await Scheduler.CreateAllSchedulersAsync(DiscordClient);

            // Instantiating all application logs.
            KWiJishoLogs.InstantiateAllLogs(DiscordClient);

            // Connecting the bot into the Discord.
            await DiscordClient.ConnectAsync();

            // This code that will be executed when the bot is ready and connected to Discord sending.
            // a message into my personal server
            var channel = await DiscordClient.GetChannelAsync(Servers.Personal.WelcomeChannelId);
            if (channel != null) await channel.SendMessageAsync("Olá!! Agora eu tô online e prontíssima pra ajudar! 🥳🎉🎉");

            // Keeps the task alive that effectively never completes, preventing the bot from
            // disconnecting when method ends.
            await Task.Delay(-1);
        }

        private void UpdateGuildsOnDatabase()
        {
            foreach (var guild in DiscordClient.Guilds)
            {

            }
        }
    }
}
