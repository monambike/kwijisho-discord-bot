// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using KWiJisho.Config;
using KWiJisho.Data;
using KWiJisho.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace KWiJisho.Events
{
    /// <summary>
    /// Provides a set of events and methods fired when a user enters and leaves a discord
    /// server.
    /// </summary>
    public class EventNotificationService
    {
        /// <summary>
        /// Handles the event when a new member joins the Discord server. Sends a welcome message
        /// with a image and a random string.
        /// </summary>
        /// <param name="sender">The discord client instance.</param>
        /// <param name="e">Event arguments containing information about the guild member.</param>
        /// <returns>A <see cref="Task"/> representing the assynchronous operation.</returns>
        public static async Task OnGuildMemberAddedAsync(DiscordClient sender, GuildMemberAddEventArgs e)
        {
            ArgumentNullException.ThrowIfNull(sender);
            // Getting welcome image info.
            var fileName = $"500x500-welcome.gif";
            var imagePath = Path.GetFullPath($"Resources/Images/Tramontina/{fileName}");

            // Making the Discord Embed Builder with the message body.
            var discordEmbedBuilder = new DiscordEmbedBuilder
            {
                Title = $"BEM-VINDO",
                Description = GetRandomWelcomeMessage(e.Member.Mention),
                Color = ConfigJson.DefaultColor.DiscordColor
            }.WithImageUrl($"attachment://{imagePath}").Build();

            using var fileStream = new FileStream(imagePath, FileMode.Open);
            // Creating message builder and attaching the message embed builder and image file.
            var discordMessageBuilder = new DiscordMessageBuilder()
                .AddEmbed(discordEmbedBuilder).AddFile(fileName, fileStream);

            // Sending the message on welcome channel.
            await e.Guild.GetChannel(Servers.Tramontina.WelcomeChannelId).SendMessageAsync(discordMessageBuilder);
        }

        /// <summary>
        /// Handles the event when a member leaves the Discord server. Sends a goodbye message
        /// with a image and a random string.
        /// </summary>
        /// <param name="sender">The discord client instance.</param>
        /// <param name="e">Events arguments containing information about the guild member.</param>
        /// <returns>A <see cref="Task"/> representing the assynchronous operation.</returns>
        public static async Task OnGuildMemberRemovedAsync(DiscordClient sender, GuildMemberRemoveEventArgs e)
        {
            // If senders is null throws an exception.
            ArgumentNullException.ThrowIfNull(sender);

            // Getting welcome image info.
            var fileName = $"1173x315-goodbye.png";
            var imagePath = Path.GetFullPath($"Resources/Images/Tramontina/{fileName}");

            // Making the discord embed builder with the message body content and image file.
            var discordEmbedBuilder = new DiscordEmbedBuilder
            {
                Title = @$"""ACHO QUE ISSO É UM ADEUS""",
                Description = GetRandomGoodbyeMessage(e.Member.Mention),
                Color = ConfigJson.DefaultColor.DiscordColor
            }.WithImageUrl($"attachment://{imagePath}").Build();

            using var fileStream = new FileStream(imagePath, FileMode.Open);
            // Creating message builder and attaching the message embed builder and image file.
            var discordMessageBuilder = new DiscordMessageBuilder()
                .AddEmbed(discordEmbedBuilder).AddFile(fileName, fileStream);

            // Sending the message on welcome channel
            await e.Guild.GetChannel(Servers.Tramontina.WelcomeChannelId).SendMessageAsync(discordMessageBuilder);
        }

        /// <summary>
        /// Gets a random string welcome message.
        /// </summary>
        /// <param name="user">The user that will receive the welcome message.</param>
        /// <returns>The string containing the welcome message .</returns>
        public static string GetRandomWelcomeMessage(string user) => GetRandomMessage(NotificationMessages.WelcomeMessages, user);

        /// <summary>
        /// Gets a random string goodbye message.
        /// </summary>
        /// <param name="user">The user that will receive the welcome message.</param>
        /// <returns>The string containing the goodbye message .</returns>
        public static string GetRandomGoodbyeMessage(string user) => GetRandomMessage(NotificationMessages.GoodbyeMessages, user);

        /// <summary>
        /// Gets a random string for any provided message type.
        /// </summary>
        /// <param name="messages">The message list.</param>
        /// <param name="user">The user that will be part of the provided message type</param>
        /// <returns>The string containing the message.</returns>
        public static string GetRandomMessage(List<string> messages, string user) => UtilCollections.GetRandomValueFromList(messages).Replace("{user}", user);
    }
}
