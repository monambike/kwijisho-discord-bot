using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using System;
using System.Threading.Tasks;

namespace KWiJisho.Modules.Events
{
    /// <summary>
    /// Provides a set of events and methods fired when a interaction is made.
    /// </summary>
    internal class Buttons
    {
        /// <summary>
        /// Handles the event when a component interaction is created, like button clicks.
        /// </summary>
        /// <param name="sender">The Discord client instance.</param>
        /// <param name="e">Event arguments containing information about the component interaction.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="sender"/> is <see langword="null"/>.</exception>
        internal static async Task OnComponentInteractionCreatedAsync(DiscordClient sender, ComponentInteractionCreateEventArgs e)
        {
            // Throws a argument null exception if the sender is null.
            ArgumentNullException.ThrowIfNull(sender);

            // Handling interactions with Id incoming from the event arguments.
            switch (e.Id)
            {
                // Copies server name suggestion.
                case "copy_server_name_suggestion": await CopyServerNameAsync(e); break;
            }
        }

        /// <summary>
        /// Copies the server name from the interaction's message and puts it into the clipboard.
        /// Sends a feedback message to the user.
        /// </summary>
        /// <param name="e">Event arguments containing information about the component interaction.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        internal static async Task CopyServerNameAsync(ComponentInteractionCreateEventArgs e)
        {
            // Getting the message content, in other words, the discord server name to be copied.
            var message = e.Message.Embeds[0].Description;

            // Gets text into the clipboard.
            TextCopy.ClipboardService.SetText(message);

            // Feedback message from the bot that you got the server name on clipboard.
            await e.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                new DiscordInteractionResponseBuilder().WithContent("Prontinho, o nome do servidor tá no seu Ctrl+C Ctrl+V! ;D"));
        }
    }
}
