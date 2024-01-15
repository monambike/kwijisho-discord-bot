using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using KWIJisho.Models.Commands;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace KWIJisho.Models
{
    internal class Bot
    {
        internal DiscordClient DiscordClient { get; private set; }
        internal CommandsNextExtension Commands { get; private set; }

        internal async Task RunAsync()
        {
            await ConfigJson.DeserializeConfigJsonFileAsync();
            
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
            // Events
            DiscordClient.Ready += OnClientReady;
            DiscordClient.MessageCreated += OnMessageReceived;
            DiscordClient.ComponentInteractionCreated += OnComponentInteractionCreatedAsync;

            var commandsNextConfiguration = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { ConfigJson.Prefix },
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
            Commands.RegisterCommands<CommandManager.Info>();
            Commands.RegisterCommands<CommandManager.Theme.Tramontina>();
        }

        private Task OnClientReady(object sender, ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }

        private async Task OnComponentInteractionCreatedAsync(DiscordClient sender, ComponentInteractionCreateEventArgs e)
        {
            /// Handling interactions with Id incoming from <param name="e"></param>
            switch (e.Id)
            {
                // Copies server name suggestion
                //case "copy_server_name_suggestion": await CopyServerName(e); break;
            }
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

        private async Task OnMessageReceived(DiscordClient sender, MessageCreateEventArgs e)
        {
            if (e.Author.IsBot) return; // Ignore messages from other bots

            await ValidateMentionedUsers(e);
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
