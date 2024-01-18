using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using ExtensionMethods;
using KWIJisho.Models.Apis;
using System;
using System.Net.Security;
using System.Threading.Tasks;

namespace KWIJisho.Models.Commands
{
    internal partial class CommandManager
    {
        internal class Nasa : BaseCommandModule
        {
            internal Command apod = new(nameof(apod), $"(APOD - Astronomy Picture of the Day) Te trago a imagem do dia fresquinha diretamente do site da Nasa! Com uma descrição traduzida por mim é claro uwu", AstronomyGroup);
            [Command(nameof(apod))]

            internal async Task Apod(CommandContext commandContext)
            {
                // Show's that the bot is "typing" while it process everything
                await commandContext.TriggerTypingAsync();

                // Getting Astronomy Picture of the Day
                var response = await NasaApi.Apod.Get();

                // Translate title and explanation
                var translatedTitle = await OpenAiApi.GetPromptTranslatorToPortugueseAsync(response.Title);
                var translatedExplanation = await OpenAiApi.GetPromptTranslatorToPortugueseAsync(response.Explanation);

                // Creating embed builder
                var firstDiscordEmbedBuilder = new DiscordEmbedBuilder
                {
                    Title = translatedTitle,
                    Description = $"Contexto da Imagem:{Environment.NewLine}{translatedExplanation}",
                    ImageUrl = response.Url,
                    Color = ConfigJson.DefaultColor.DiscordColor,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = $"Copyright: {response.Copyright} • Data deste APOD: {response.Date.ToString("d", new System.Globalization.CultureInfo("pt-BR"))}",
                    }
                };

                // Sending the first message
                await commandContext.Channel.SendMessageAsync(new DiscordMessageBuilder()
                    .AddEmbed(firstDiscordEmbedBuilder));
            }
        }
    }
}
