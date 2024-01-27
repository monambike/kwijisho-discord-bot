﻿using DSharpPlus.EventArgs;
using System.Threading.Tasks;

namespace KWiJisho.Models.Events
{
    internal class MessageReceived
    {
        //    internal static async Task OnMessageReceivedAsync(DiscordClient sender, MessageCreateEventArgs e)
        //    {
        //        ArgumentNullException.ThrowIfNull(sender);

        //        if (e.Author.IsBot) return; // Ignore messages from other bots

        //        await ValidateMentionedUsers(e);
        //    }

        internal static async Task ValidateMentionedUsersAsync(MessageCreateEventArgs e)
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
