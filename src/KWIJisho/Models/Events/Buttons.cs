using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using System;
using System.Threading.Tasks;

namespace KWIJisho.Models.Events
{
    internal class Buttons
    {
        internal static async Task OnComponentInteractionCreatedAsync(DiscordClient sender, ComponentInteractionCreateEventArgs e)
        {
            ArgumentNullException.ThrowIfNull(sender);
            /// Handling interactions with Id incoming from <param name="e"></param>
            switch (e.Id)
            {
                // Copies server name suggestion
                case "copy_server_name_suggestion": await CopyServerNameAsync(e); break;
            }
        }

        internal static async Task CopyServerNameAsync(ComponentInteractionCreateEventArgs e)
        {
            // Getting the message content, in other words, the discord server name to be copied
            var message = e.Message.Embeds[0].Description;
            // Gets text into the clipboard
            TextCopy.ClipboardService.SetText(message);

            // Feedback message from the bot that you got the server name on clipboard
            await e.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                new DiscordInteractionResponseBuilder().WithContent("Prontinho, o nome do servidor tá no seu Ctrl+C Ctrl+V! ;D"));
        }
    }
}
