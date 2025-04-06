// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using KWiJisho.Data;
using KWiJisho.Database.Services;
using KWiJisho.Entities;
using KWiJisho.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConfigJson = KWiJisho.Config.ConfigJson;

namespace KWiJisho.Events
{
    /// <summary>
    /// Provides a set of events and methods fired when a user enters and leaves a discord
    /// server.
    /// </summary>
    public class DiscordGuildHandler
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

            var welcomeBuilder = new GuildEvents().WelcomeBuilder;
            welcomeBuilder.UpdateDescription(e.Member);

            var discordMessageBuilder = CreateDiscordMessageBuilder(welcomeBuilder);

            var server = await ServerChannelService.GetServerChannelByServerGuidAsync(e.Guild.Id);
            if (server is null) return;

            // Sending the message on welcome channel.
            await e.Guild.GetChannel(server.WelcomeChannelGuid.GetValueOrDefault()).SendMessageAsync(discordMessageBuilder);
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

            var goodbyeBuilder = new GuildEvents().GoodbyeBuilder;
            goodbyeBuilder.UpdateDescription(e.Member);

            var discordMessageBuilder = CreateDiscordMessageBuilder(goodbyeBuilder);

            var server = await ServerChannelService.GetServerChannelByServerGuidAsync(e.Guild.Id);
            if (server is null) return;

            // Sending the message on welcome channel
            await e.Guild.GetChannel(server.GoodbyeChannelGuid.GetValueOrDefault()).SendMessageAsync(discordMessageBuilder);
        }

        /// <summary>
        /// Creates a <see cref="DiscordMessageBuilder"/> with a customized embed based on the provided <see cref="GuildEventEmbedBuilder"/>.
        /// Includes user avatar as thumbnail, an optional image, and footer with username, ID, and timestamp.
        /// </summary>
        /// <param name="builderGuildAction">The embed builder object containing message data like title, description, user, and image URL.</param>
        /// <returns>A ready-to-send <see cref="DiscordMessageBuilder"/> with embed and optional image.</returns>
        public static DiscordMessageBuilder CreateDiscordMessageBuilder(GuildEventEmbedBuilder builderGuildAction)
        {
            // Making the discord embed builder with the message body content and image file.
            var discordEmbedBuilder = new DiscordEmbedBuilder
            {
                Title = builderGuildAction.Title,
                Description = builderGuildAction.Description,
                Color = ConfigJson.DefaultColor.DiscordColor,
                Footer = new DiscordEmbedBuilder.EmbedFooter
                {
                    Text = $"Username: {builderGuildAction.DiscordUser.Username} • Id: {builderGuildAction.DiscordUser.Id} • Date: {DateTime.Now:dd/MM/yyyy HH:mm:ss}",
                }
            }
            .WithThumbnail(builderGuildAction.DiscordUser.AvatarUrl)
            .WithImageUrl(builderGuildAction.ImageUrl).Build();

            // Creating message builder and attaching the message embed builder and image file.
            var discordMessageBuilder = new DiscordMessageBuilder()
                .AddEmbed(discordEmbedBuilder);

            return discordMessageBuilder;

        }

        /// <summary>
        /// Represents types of guild-related events such as when a user joins, leaves, or boosts the server.
        /// </summary>
        public enum GuildEventType
        {
            /// <summary>
            /// Represents when a user leaves the server.
            /// </summary>
            Welcome,

            /// <summary>
            /// Represents when a user leaves the server.
            /// </summary>
            Goodbye,

            /// <summary>
            /// Represents when a user boosts the server.
            /// </summary>
            Boost
        }

        public static string GetRandomMessageByType(GuildEventType updateType, string user)
        {
            return updateType switch
            {
                GuildEventType.Welcome => GetRandomWelcomeMessage(user),
                GuildEventType.Goodbye => GetRandomGoodbyeMessage(user),
                _ => throw new NotImplementedException()
            };
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
