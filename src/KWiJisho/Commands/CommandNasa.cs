// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus.Entities;
using System.Net.Http;
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
        /// Property to manage HTTP requests, using a singleton pattern for efficiency and performance.
        /// </summary>
        private static HttpClient HttpClient => new();

        /// <summary>
        /// Retrieves and sends the english version of the Astronomy Picture of the Day (APOD) to the specified Discord channel.
        /// </summary>
        /// <param name="discordChannel">The Discord channel where the APOD message will be sent.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task ApodEnglishAsync(DiscordChannel discordChannel)
        {
            await ApodAsync(discordChannel, ApodLanguage.English);
        }
        
        /// <summary>
        /// Retrieves and sends the portuguese version of the Astronomy Picture of the Day (APOD) to the specified Discord channel.
        /// </summary>
        /// <param name="discordChannel">The Discord channel where the translated APOD message will be sent.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task ApodPortugueseAsync(DiscordChannel discordChannel)
        {
            await ApodAsync(discordChannel, ApodLanguage.Portuguese);
        }

        private static async Task ApodAsync(DiscordChannel discordChannel, ApodLanguage apodLanguage)
        {
            // Gets the Astronomy Picture of the Day.
            var response = await GetApodAsync();

            // Creates a Discord embed builder for the translated APOD message.
            var apodBuilder = await BuildApodMessageAsync(apodLanguage, response);

            // Creates the discord embed builder from the apod builder.
            var discordEmbedBuilder = apodBuilder.GetDiscordEmbedBuilder();

            var messageBuilder = new DiscordMessageBuilder().AddEmbed(discordEmbedBuilder);

            // If it's an image, attachs the image to the embed.
            if (apodBuilder.MediaType == "image")
                messageBuilder = await BuildDiscordMessageWithImageAsync(discordEmbedBuilder, apodBuilder.MediaUrl);

            //Sending the generated Discord embed builder to the specified channel.
            await discordChannel.SendMessageAsync(messageBuilder);

            // If it's a video, sending the video link as separated message after sending the embbed.
            if (apodBuilder.MediaType == "video")
                await discordChannel.SendMessageAsync(apodBuilder.VideoField);
        }

        /// <summary>
        /// Builds a Discord message with an embed and an attached image file.
        /// </summary>
        /// <param name="discordEmbedBuilder">The embed builder containing the message content.</param>
        /// <param name="imageUrl">The URL of the image to download and attach.</param>
        /// <returns>A <see cref="DiscordMessageBuilder"/> with the embed and the image file attached.</returns>
        private async static Task<DiscordMessageBuilder> BuildDiscordMessageWithImageAsync(DiscordEmbedBuilder discordEmbedBuilder, string imageUrl)
        {
            // The name used to attach the image file to the message.
            string imageName = "apod_image.png";
            discordEmbedBuilder.ImageUrl = $"attachment://{imageName}";

            // Using HTTP Client instance to download the image as a stream from the given URL.
            var imageStream = await HttpClient.GetStreamAsync(imageUrl);

            // Build the Discord message with the embed and the attached image file.
            var messageBuilder = new DiscordMessageBuilder()
                .AddEmbed(discordEmbedBuilder)
                .AddFile(imageName, imageStream);

            // Returns the built message.
            return messageBuilder;
        }
    }
}
