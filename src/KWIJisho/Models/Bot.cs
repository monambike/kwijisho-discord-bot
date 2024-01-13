using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using Newtonsoft.Json;
using System;
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
            DiscordClient.MessageCreated += OnMessageReceived;

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
            if (channel != null) await channel.SendMessageAsync("Olá!! Agora eu tô online e prontíssima pra ajudar! 🥳🎉🎉");

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
        private async Task OnMessageReceived(DiscordClient sender, MessageCreateEventArgs e)
        {
            if (e.Author.IsBot) return; // Ignore messages from other bots

            await ValidateMentionedUsers(sender, e);
        }

        private async Task ValidateMentionedUsers(DiscordClient sender, MessageCreateEventArgs e)
        {
            // Get the username of the message author
            string authorName = e.Author.Username;

            // Get mentioned user
            if (e.MentionedUsers.Count == 1)
            {
                string mentionedName = e.MentionedUsers[0].Username;
                // Someone mentioned another user
                await e.Message.RespondAsync($"Desculpa me intrometer, eu nem ia falar nada não {authorName} mas o {mentionedName} é um tremendo de um babaca.. 😶");
                await e.Message.RespondAsync($"A");
                await e.Channel.SendMessageAsync($"Aah!.. Ele tá ai!.. E-eu não tinha reparado.. 😳 O-oi {mentionedName} tudo bem com você?! A gente tava falando de você agora pouco 👀🙈");
            }
            else if (e.MentionedUsers.Count > 1)
            {
                string mentionedName = e.MentionedUsers[0].Username;
                // Someone mentioned more than a user
                await e.Message.RespondAsync($"Desculpa me intrometer, eu nem ia falar nada não {authorName} mas o {mentionedName} é um tremendo de um babaca.. 😶");
            }
        }
    }
}
