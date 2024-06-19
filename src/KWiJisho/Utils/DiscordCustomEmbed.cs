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
