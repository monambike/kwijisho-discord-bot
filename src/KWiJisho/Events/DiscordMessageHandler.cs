// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus;
using DSharpPlus.EventArgs;
using KWiJisho.APIs;
using KWiJisho.Utils;
using System.Linq;
using System.Threading.Tasks;

namespace KWiJisho.Events
{
    /// <summary>
    /// Provides a set of events and methods for when a message is received.
    /// </summary>
    public class DiscordMessageHandler
    {
        public static async Task OnMessageCreatedAsync(DiscordClient sender, MessageCreateEventArgs e)
        {
            if (e.Author.IsBot) return;

            var contentWithoutMentions = e.Message.Content.RemoveDiscordMention(sender);
            var response = await ChatGPT.GetKWiJishoPromptAsync(contentWithoutMentions) ?? BotResponses.ChatGptError;
            if (e.Message.MentionedUsers.Any(u => u.Id == sender.CurrentUser.Id)) await e.Message.RespondAsync(response);
        }

        public static async Task ValidateMentionedUsersAsync(MessageCreateEventArgs e)
        {
            // Get the username of the message author.
            string authorName = e.Author.Username;

            // Get mentioned user.
            if (e.MentionedUsers.Count == 1)
            {
                string mentionedName = e.MentionedUsers[0].Username;
                // Someone mentioned another user.
                await e.Message.RespondAsync($"Desculpa me intrometer, eu nem ia falar nada não {authorName} mas o {mentionedName} é um tremendo de um babaca.. 😶");
                await e.Message.RespondAsync($"A");
                await e.Channel.SendMessageAsync($"Aah!.. Ele tá ai!.. E-eu não tinha reparado.. 😳 O-oi {mentionedName} tudo bem com você?! A gente tava falando de você agora pouco 👀🙈");
            }
            else if (e.MentionedUsers.Count > 1)
            {
                string mentionedName = e.MentionedUsers[0].Username;
                // Someone mentioned more than a user.
                await e.Message.RespondAsync($"Desculpa me intrometer, eu nem ia falar nada não {authorName} mas o {mentionedName} é um tremendo de um babaca.. 😶");
            }
        }
    }
}
