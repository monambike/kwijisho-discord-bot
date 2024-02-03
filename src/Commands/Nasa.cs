using DSharpPlus.Entities;
using KWiJisho.APIs;
using System.Threading.Tasks;

namespace KWiJisho.Commands
{
    /// <summary>
    /// Provides methods for NASA prefix and slash commands.
    /// </summary>
    internal static class Nasa
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

            // Creating a Discord embed builder for the APOD message.
            var discordEmbedBuilder = Models.Nasa.BuildEnglishApodMessageAsync(response);

            // Sending the generated Discord embed builder to the specified channel.
            await discordChannel.SendMessageAsync(discordEmbedBuilder);
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
            var discordEmbedBuilder = await Models.Nasa.BuildPortugueseApodMessageAsync(response);

            // Sending the generated Discord embed builder to the specified channel.
            await discordChannel.SendMessageAsync(discordEmbedBuilder);
        }
    }
}
