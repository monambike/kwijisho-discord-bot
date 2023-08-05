using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace KWIJisho
{
    internal class Bot
    {
        internal DiscordClient DiscordClient { get; private set; }
        internal CommandsNextExtension Commands { get; private set; }


        internal async Task RunAsync()
        {
            var configJson = DeserializeConfigJsonFileAsync();
            var configJsonResult = configJson.Result;
            
            var discordConfiguration = new DiscordConfiguration
            {
                Token = configJsonResult.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Debug,
                Intents = DiscordIntents.All
            };

            DiscordClient = new DiscordClient(discordConfiguration);

            DiscordClient.Ready += OnClientReady;

            var commandsNextConfiguration = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { configJsonResult.Prefix },
                EnableDms = false,
                EnableMentionPrefix = true,
                EnableDefaultHelp = false
            };

            Commands = DiscordClient.UseCommandsNext(commandsNextConfiguration);
            RegisterAllBotCommands();

            await DiscordClient.ConnectAsync();

            // This code block will be executed when the bot is ready and connected to Discord.
            var channel = await DiscordClient.GetChannelAsync(737541664775602269);
            if (channel != null) await channel.SendMessageAsync("Olá!! Agora eu tô online e pronta para servir. 😉");

            await Task.Delay(-1);
        }

        private void RegisterAllBotCommands()
        {
            Commands.RegisterCommands<Commands.Dictionary>();
            Commands.RegisterCommands<Commands.Info>();
        }

        private async Task<ConfigJson> DeserializeConfigJsonFileAsync()
        {

            var json = string.Empty;

            using (var fileSteam = File.OpenRead(@"..\..\config.json"))
                using (var streamReader = new StreamReader(fileSteam, new UTF8Encoding(false)))
                    json = await streamReader.ReadToEndAsync().ConfigureAwait(false);
            
            var result = JsonConvert.DeserializeObject<ConfigJson>(json);
            return result;
        }

        private Task OnClientReady(object sender, ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }
    }
}
