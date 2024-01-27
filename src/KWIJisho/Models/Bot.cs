using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.SlashCommands;
using KWiJisho.Models.Commands;
using KWiJisho.Models.Commands.Slash;
using KWiJisho.Models.Events;
using System.Threading.Tasks;

namespace KWiJisho.Models
{
    internal partial class Bot
    {
        internal DiscordClient DiscordClient { get; private set; }
        internal CommandsNextExtension PrefixCommands { get; private set; }
        internal SlashCommandsExtension SlashCommands { get; private set; }

        internal async Task RunAsync()
        {
            // Getting info from Json file and setting into the ConfigJson class
            await ConfigJson.DeserializeConfigJsonFileAsync();

            // Defining initial bot settings
            var discordConfiguration = new DiscordConfiguration
            {
                Token = ConfigJson.KWiJishoToken,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Debug,
                Intents = DiscordIntents.All
            };

            // DiscordClients instance and events
            DiscordClient = new DiscordClient(discordConfiguration);
            DiscordClient = RegisterAllBotEvents();

            // Defining bot prefix commands settings
            var commandsNextConfiguration = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { ConfigJson.Prefix },
                EnableDms = false,
                EnableMentionPrefix = true,
                EnableDefaultHelp = false
            };
            // Defining and registering bot prefix commands
            PrefixCommands = DiscordClient.UseCommandsNext(commandsNextConfiguration);
            RegisterAllBotPrefixCommands();

            // Defining and registering bot slash commands
            SlashCommands = DiscordClient.UseSlashCommands();
            RegisterAllBotSlashCommands();

            // Connecting to the bot
            await DiscordClient.ConnectAsync();

            // This code block will be executed when the bot is ready and connected to Discord.
            var channel = await DiscordClient.GetChannelAsync(737541664775602269); // My main personal server's channel
            if (channel != null) await channel.SendMessageAsync("Olá!! Agora eu tô online e prontíssima pra ajudar! 🥳🎉🎉");

            await Task.Delay(-1);
        }

        private void RegisterAllBotPrefixCommands()
        {
            PrefixCommands.RegisterCommands<CommandManager.Nasa>();
            PrefixCommands.RegisterCommands<CommandManager.KwiGpt>();
            PrefixCommands.RegisterCommands<CommandManager.Info>();
            PrefixCommands.RegisterCommands<CommandManager.Theme.Tramontina>();
            PrefixCommands.RegisterCommands<CommandManager.Birthday>();
        }
        private void RegisterAllBotSlashCommands()
        {
            SlashCommands.RegisterCommands<CommandManager.Info>(737541664318554143);
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
