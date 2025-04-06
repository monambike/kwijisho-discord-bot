// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus.Entities;
using KWiJisho.Config;
using System.IO;

namespace KWiJisho.Utils
{
    /// <summary>
    /// Utility class providing default embeds for KWiJisho Discord bot.
    /// </summary>
    public class DiscordCustomEmbed
    {
        /// <summary>
        /// Generates a pre-configured <see cref="DiscordEmbed"/> with optional KWiJisho thumbnail.
        /// </summary>
        /// <param name="title">The title of the embed.</param>
        /// <param name="description">The description/body text of the embed.</param>
        /// <param name="useKWiJishoThumbnail">Whether to include the default KWiJisho thumbnail image in the embed.</param>
        /// <returns>A <see cref="DiscordEmbed"/> with the specified content and optional thumbnail.</returns>
        public static DiscordEmbed GetDiscordEmbed(string title, string description, bool useKWiJishoThumbnail = false)
        {
            // Getting image name and image's full path.
            var fileName = $"256x256-ayaya.ico";
            var imagePath = Path.GetFullPath($@"Resources\Images\{fileName}");

            // If solicited for using a thumbnail, get it from the image path; otherwhise, it wont use a thumbnail.
            var thumbnail = useKWiJishoThumbnail == true ? new DiscordEmbedBuilder.EmbedThumbnail { Url = imagePath } : null;

            // Creating the discord embed builder.
            var discordEmbedBuilder = new DiscordEmbedBuilder()
            {
                Color = ConfigJson.DefaultColor.DiscordColor,
                Title = title,
                Description = description,
                Thumbnail = thumbnail
            };

            // Returning the DiscordEmbed already builded.
            return discordEmbedBuilder.Build();
        }
    }
}
