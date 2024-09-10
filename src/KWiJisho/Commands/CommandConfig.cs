// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus.Entities;
using KWiJisho.Config;
using KWiJisho.Database.Services;
using KWiJisho.Entities;
using KWiJisho.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static KWiJisho.Database.Services.ServerService;

namespace KWiJisho.Commands
{
    internal class CommandConfig
    {
        public static async Task CommandLinkChannelAsync(DiscordChannel interactionContext, DiscordChannel discordChannel, ChannelLink channelLink)
        {
            await UpdateServerChannelByEnumAsync(interactionContext.Guild.Id, channelLink, discordChannel.Id);

            // Adding the prompt into a embed builder.
            var discordEmbedBuilder = new DiscordEmbedBuilder
            {
                Description = $@"Foi vinculado o canal ""{discordChannel.Name}"" em ""{channelLink}"" com sucesso!",
                Color = Config.ConfigJson.DefaultColor.DiscordColor
            };

            await interactionContext.SendMessageAsync(discordEmbedBuilder);
        }

        public static async Task CommandUnlinkChannelAsync(DiscordChannel interactionContext, ChannelLink channelLink)
        {
            await UpdateServerChannelByEnumAsync(interactionContext.Guild.Id, channelLink, null);

            // Adding the prompt into a embed builder.
            var discordEmbedBuilder = new DiscordEmbedBuilder
            {
                Description = $@"Foi removido o vínculo do canal ""{channelLink}"" com sucesso!",
                Color = Config.ConfigJson.DefaultColor.DiscordColor
            };

            await interactionContext.SendMessageAsync(discordEmbedBuilder);
        }


        public static async Task CommandSlashViewChannelsAsync(DiscordChannel interactionContext)
        {
            var serverChannel = await ServerChannelService.GetServerChannelByServerGuidAsync(interactionContext.Guild.Id);

            Dictionary<ChannelLink, ulong?> channels = new()
            {
                { ChannelLink.WelcomeChannel, serverChannel?.WelcomeChannelGuid},
                { ChannelLink.GoodbyeChannel, serverChannel?.GoodbyeChannelGuid},
                { ChannelLink.NewsChannel, serverChannel?.NewsChannelGuid},
                { ChannelLink.LogChannel, serverChannel?.LogChannelGuid}
            };

            string description = string.Empty;
            foreach (var channel in channels)
                description += $"{(channel.Value is null ? "❌" : "✅")} {channel.Key}:".ToDiscordBold() + $" {(channel.Value is null ? "<unlinked>" : channel.Value)} {Environment.NewLine} {Environment.NewLine}";

            // Adding the prompt into a embed builder.
            var discordEmbedBuilder = new DiscordEmbedBuilder
            {
                Title = "Config: Channel Links",
                Description = description,
                Color = Config.ConfigJson.DefaultColor.DiscordColor,
                Footer = new DiscordEmbedBuilder.EmbedFooter
                {
                    Text = $@"List of settings for ""{interactionContext.Guild.Name}"" server."
                }
            }
            .WithThumbnail(interactionContext.Guild.IconUrl)
            .Build();

            await interactionContext.SendMessageAsync(discordEmbedBuilder);
        }
    }
}
