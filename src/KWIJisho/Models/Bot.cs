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
        internal DiscordClient Client { get; private set; }
        internal CommandsNextExtension Commands { get; private set; }


        internal async Task RunAsync()
        {
            var configJson = DeserializeConfigJsonFileAsync();
            var configJsonResult = configJson.Result;
            
            var discordConfiguration = new DiscordConfiguration
            {
                Token = configJsonResult.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true
            };

            Client = new DiscordClient(discordConfiguration);

            Client.Ready += OnClientReady;

            var commandsNextConfiguration = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { configJsonResult.Prefix },
                EnableDms = false,
                EnableMentionPrefix = true,
                EnableDefaultHelp = false
            };

            Commands = Client.UseCommandsNext(commandsNextConfiguration);
            RegisterAllBotCommands();

            await Client.ConnectAsync();
            await Task.Delay(-1);
        }


        private void RegisterAllBotCommands()
        {
            Commands.RegisterCommands<Commands.Dictionary.Word>();
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
