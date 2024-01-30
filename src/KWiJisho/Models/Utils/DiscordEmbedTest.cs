using DSharpPlus.Entities;
using System.IO;

namespace KWiJisho.Models.Utils
{
    internal class DiscordEmbedTest
    {
        internal static DiscordEmbed GetDiscordEmbed(string title, string description, bool useKWiJishoThumbnail = false)
        {
            // Getting image name and image's full path
            var fileName = $"256x256-ayaya.ico";
            var imagePath = Path.GetFullPath($"{fileName}");

            var thumbnail = (useKWiJishoThumbnail == true) ? new DiscordEmbedBuilder.EmbedThumbnail { Url = imagePath } : null;

            var discordEmbedBuilder = new DiscordEmbedBuilder()
            {
                Color = ConfigJson.DefaultColor.DiscordColor,
                Title = title, Description = description,
                Thumbnail = thumbnail
            };

            return discordEmbedBuilder.Build();
        }
    }
}
