// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus.Entities;
using System.Threading.Tasks;
using static KWiJisho.APIs.NasaApi.Apod;
using static KWiJisho.Models.Nasa;

namespace KWiJisho.Commands
{
    /// <summary>
    /// Provides methods for NASA prefix and slash commands.
    /// </summary>
    public static class CommandNasa
    {
        /// <summary>
        /// Retrieves and sends the english version of the Astronomy Picture of the Day (APOD) to the specified Discord channel.
        /// </summary>
        /// <param name="discordChannel">The Discord channel where the APOD message will be sent.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>

        public static async Task ApodEnglishAsync(DiscordChannel discordChannel)
        {
            await Apod(discordChannel, ApodLanguage.English);
        }
        
        /// <summary>
        /// Retrieves and sends the portuguese version of the Astronomy Picture of the Day (APOD) to the specified Discord channel.
        /// </summary>
        /// <param name="discordChannel">The Discord channel where the translated APOD message will be sent.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task ApodPortugueseAsync(DiscordChannel discordChannel)
        {
            await Apod(discordChannel, ApodLanguage.Portuguese);
        }

        private static async Task Apod(DiscordChannel discordChannel, ApodLanguage apodLanguage)
        {
            // Getting the Astronomy Picture of the Day.
            var response = await GetApodAsync();

            // Creating a Discord embed builder for the translated APOD message.
            var apodBuilder = await BuildApodMessageAsync(apodLanguage, response);

            // Creating the discord embed builder from the apod builder.
            var discordEmbedBuilder = apodBuilder.GetDiscordEmbedBuilder();

            // If it's an image, attaching the image to the embbed.
            if (apodBuilder.MediaType == "image")
                discordEmbedBuilder.ImageUrl = apodBuilder.MediaUrl;

            //Sending the generated Discord embed builder to the specified channel.
            await discordChannel.SendMessageAsync(discordEmbedBuilder);

            // If it's a video, sending the video link as separated message after sending the embbed.
            if (apodBuilder.MediaType == "video")
                await discordChannel.SendMessageAsync(apodBuilder.VideoField);
        }
    }
}
