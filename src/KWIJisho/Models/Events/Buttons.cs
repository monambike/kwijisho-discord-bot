using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

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
                case "copy_server_name_suggestion": await CopyServerName(e); break;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "This bot is only targeted for windows.")]
        internal static async Task CopyServerName(ComponentInteractionCreateEventArgs e)
        {
            // Use a thread in STA mode to set the clipboard text
            Thread thread = new(() =>
            {
                // Getting the message content, in other words, the discord server name to be copied
                var message = e.Message.Embeds[0].Description;
                // Gets text into the clipboard
                Clipboard.SetText(message);
            });
            // Settings thread to Single-Threaded Apartment and running code
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            // Feedback message from the bot that you got the server name on clipboard
            await e.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                new DiscordInteractionResponseBuilder().WithContent("Prontinho, o nome do servidor tá no seu Ctrl+C Ctrl+V! ;D"));
        }

    }
}
