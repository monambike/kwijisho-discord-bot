using DSharpPlus.Entities;
using KWiJisho.APIs;
using System.Threading.Tasks;

namespace KWiJisho.Commands
{
    /// <summary>
    /// Provides methods for NASA prefix and slash commands.
    /// </summary>
    internal static class CommandNasa
    {
        /// <summary>
        /// Retrieves and sends the Astronomy Picture of the Day (APOD) to the specified Discord channel.
        /// </summary>
        /// <param name="discordChannel">The Discord channel where the APOD message will be sent.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>

        internal static async Task ApodAsync(DiscordChannel discordChannel)
        {
            // Getting the Astronomy Picture of the Day.
            var response = await NasaApi.Apod.GetApodAsync();

            // Creating the apod builder.
            var apodBuilder = Models.Nasa.BuildEnglishApodMessageAsync(response);

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
        
        /// <summary>
        /// Retrieves and sends the translated version of the Astronomy Picture of the Day (APOD) to the specified Discord channel.
        /// </summary>
        /// <param name="discordChannel">The Discord channel where the translated APOD message will be sent.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        internal static async Task ApodResumeAsync(DiscordChannel discordChannel)
        {
            // Getting the Astronomy Picture of the Day.
            var response = await NasaApi.Apod.GetApodAsync();

            // Creating a Discord embed builder for the translated APOD message.
            var apodBuilder = await Models.Nasa.BuildPortugueseApodMessageAsync(response);

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
