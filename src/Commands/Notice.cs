using DSharpPlus;
using DSharpPlus.Entities;
using KWiJisho.Data;
using KWiJisho.Entities;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace KWiJisho.Commands
{
    /// <summary>
    /// Provides methods for notice prefix and slash commands.
    /// </summary>
    internal class Notice
    {
        /// <summary>
        /// Represents the Yellow color.
        /// </summary>
        internal static Color Yellow = new(255, 231, 94);

        /// <summary>
        /// Method responsible for sending and creating a "news" in the news channel.
        /// </summary>
        /// <param name="discordClient"></param>
        /// <param name="discordMember"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="sendInTramontina"></param>
        /// <param name="discordAttachments"></param>
        /// <returns></returns>
        internal static async Task ExecuteNewsAsync(DiscordClient discordClient, DiscordMember discordMember, string title, string description, bool sendInTramontina, DiscordAttachment discordAttachments = null)
        {
            // Making the DiscordEmbedBuilder with the news content.
            var discordEmbedBuilder = new DiscordEmbedBuilder
            {
                Color = Yellow.DiscordColor,
                Title = $"NOVO: {title.ToUpper()}",
                Description = description
            }
            .WithFooter($"Enviado por: {discordMember.Username} • Data: {DateTime.Now.ToString(new CultureInfo("pt-BR"))}");

            // If there's an attachment.
            if (discordAttachments is not null)
                // Attach it into the message
                discordEmbedBuilder.WithImageUrl(discordAttachments.Url);

            // Getting the guildId according to wich server should be sent the message
            var guildId = sendInTramontina ? ServerTramontina.NewsChannelId : ServerPersonal.KWiJishoChannelId;

            // Getting channel based on chosen guild Id.
            var discordChannel = await discordClient.GetChannelAsync(guildId);

            // Sending the response in the selected channel.
            await discordChannel.SendMessageAsync(discordEmbedBuilder);
        }
    }
}
