using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using KWIJisho.Models.Commands;
using System.IO;
using System.Linq;
using System.Threading;
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
                Token = ConfigJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Debug,
                Intents = DiscordIntents.All
            };

            // DiscordClients instance and events
            DiscordClient = new DiscordClient(discordConfiguration);
            DiscordClient = RegisterAllBotEvents(DiscordClient);

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
            Commands.RegisterCommands<CommandManager.Info>();
            Commands.RegisterCommands<CommandManager.Theme.Tramontina>();
        }

        private DiscordClient RegisterAllBotEvents(DiscordClient discordConfiguration)
        {
            // Events
            DiscordClient.Ready += OnClientReady;
            DiscordClient.MessageCreated += OnMessageReceived;
            DiscordClient.ComponentInteractionCreated += OnComponentInteractionCreatedAsync;
            DiscordClient.GuildMemberAdded += OnGuildMemberAddedAsync;
            DiscordClient.GuildMemberRemoved += OnGuildMemberRemovedAsync;

            return DiscordClient;
        }

        private Task OnClientReady(object sender, ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }

        private async Task CopyServerName(ComponentInteractionCreateEventArgs e)
        {
            // Use a thread in STA mode to set the clipboard text
            Thread thread = new Thread(() =>
            {
                // Getting the message content, in other words, the discord server name to be copied
                var message = e.Message.Embeds.FirstOrDefault().Description;
                // Gets text into the clipboard
                //Clipboard.SetText(message);
            });
            // Settings thread to Single-Threaded Apartment and running code
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            // Feedback message from the bot that you got the server name on clipboard
            await e.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                new DiscordInteractionResponseBuilder().WithContent("Prontinho, o nome do servidor tá no seu Ctrl+C Ctrl+V! ;D"));
        }

        private async Task ValidateMentionedUsers(MessageCreateEventArgs e)
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
