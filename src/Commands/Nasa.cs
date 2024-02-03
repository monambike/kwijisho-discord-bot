using DSharpPlus.Entities;
using KWiJisho.APIs;
using System.Threading.Tasks;

namespace KWiJisho.Commands
{
    internal static class Nasa
    {
        internal static async Task ApodAsync(DiscordChannel discordChannel)
        {
            // Getting the picture of the day.
            var response = await NasaApi.Apod.GetApodAsync();

            // Creating embed builder.
            var discordEmbedBuilder = Models.Nasa.BuildEnglishApodMessageAsync(response);

            // Sending the generated discord embed builder.
            await discordChannel.SendMessageAsync(discordEmbedBuilder);
        }

        internal static async Task ApodResumeAsync(DiscordChannel discordChannel)
        {
            // Getting the picture of the day.
            var response = await NasaApi.Apod.GetApodAsync();

            // Creating embed builder.
            var discordEmbedBuilder = await Models.Nasa.BuildPortgueseApodMessageAsync(response);

            // Sending the generated discord embed builder.
            await discordChannel.SendMessageAsync(discordEmbedBuilder);
        }
    }
}
