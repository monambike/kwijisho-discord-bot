using DSharpPlus;
using DSharpPlus.CommandsNext;
using KWIJisho.Models.Commands;
using KWIJisho.Models.Events;
using System.Threading.Tasks;

namespace KWIJisho.Models
{
    internal partial class Bot
    {
        internal DiscordClient DiscordClient { get; private set; }
        internal CommandsNextExtension Commands { get; private set; }

        internal async Task RunAsync()
        {
            // Getting info from Json file and setting into the ConfigJson class
            await ConfigJson.DeserializeConfigJsonFileAsync();

            // Defining initial bot settings
            var discordConfiguration = new DiscordConfiguration
            {
                Token = ConfigJson.KWIJishoToken,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Debug,
                Intents = DiscordIntents.All
            };

            // DiscordClients instance and events
            DiscordClient = new DiscordClient(discordConfiguration);
            DiscordClient = RegisterAllBotEvents();

            // Defining bot commands settings and registering commands
            var commandsNextConfiguration = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { ConfigJson.Prefix },
                EnableDms = false,
                EnableMentionPrefix = true,
                EnableDefaultHelp = false
            };
            Commands = DiscordClient.UseCommandsNext(commandsNextConfiguration);
            RegisterAllBotCommands();

            // Connecting to the bot
            await DiscordClient.ConnectAsync();

            // This code block will be executed when the bot is ready and connected to Discord.
            var channel = await DiscordClient.GetChannelAsync(737541664775602269); // My main personal server's channel
            if (channel != null) await channel.SendMessageAsync("Olá!! Agora eu tô online e prontíssima pra ajudar! 🥳🎉🎉");


            await Task.Delay(-1);
        }

        private void RegisterAllBotCommands()
        {
            Commands.RegisterCommands<CommandManager.Nasa>();
            Commands.RegisterCommands<CommandManager.KwiGpt>();
            Commands.RegisterCommands<CommandManager.Info>();
            Commands.RegisterCommands<CommandManager.Theme.Tramontina>();
        }

        private DiscordClient RegisterAllBotEvents()
        {
            DiscordClient.Ready += BotStart.OnClientReady;
            DiscordClient.ComponentInteractionCreated += Buttons.OnComponentInteractionCreatedAsync;
            DiscordClient.GuildMemberAdded += GoodbyeWelcome.OnGuildMemberAddedAsync;
            DiscordClient.GuildMemberRemoved += GoodbyeWelcome.OnGuildMemberRemovedAsync;

            return DiscordClient;
        }
    }
}
