using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using ExtensionMethods;
using KWIJisho.Models.Apis;
using System;
using System.Threading.Tasks;

namespace KWIJisho.Models.Commands
{
    internal partial class CommandManager
    {
        internal class Nasa : BaseCommandModule
        {
            internal Command apod = new(nameof(apod), $"(APOD - Astronomy Picture of the Day) Te trago a imagem do dia fresquinha diretamente do site da Nasa! Com uma descrição traduzida por mim é claro uwu", AstronomyGroup);
            [Command(nameof(apod))]
            internal async Task ApodAsync(CommandContext commandContext)
            {
                var message = await GenerateApodDiscordMessageBuilderAsync(commandContext, OpenAiApi.TranslationType.Translate);
                await commandContext.Channel.SendMessageAsync(message);
            }


            internal Command apodResume = new(nameof(apodResume), $"Te trago o mesmo conteúdo do comando {"!apod".ToDiscordBold()} mas mais fácil e divertido de ler! (Texto Resumido)", AstronomyGroup);
            [Command(nameof(apodResume))]
            internal async Task ApodResumeAsync(CommandContext commandContext)
            {
                var message = await GenerateApodDiscordMessageBuilderAsync(commandContext, OpenAiApi.TranslationType.TranslateAndResume);
                await commandContext.Channel.SendMessageAsync(message);
            }

            private async Task<DiscordEmbedBuilder> GenerateApodDiscordMessageBuilderAsync(CommandContext commandContext, OpenAiApi.TranslationType translationType)
            {
                // Getting the picture of the day
                var response = await NasaApi.Apod.GetAsync();

                // Translate title
                var translatedTitle = await OpenAiApi.GetPromptTranslateToPortugueseAsync(response.Title, OpenAiApi.TranslationType.Translate);

                // Translate explanation with the specified translation type
                await commandContext.TriggerTypingAsync();
                var translatedExplanation = await OpenAiApi.GetPromptTranslateToPortugueseAsync(response.Explanation, translationType);

                // Creating embed builder
                var discordEmbedBuilder = new DiscordEmbedBuilder
                {
                    Title = $"(Imagem do Dia) {translatedTitle}",
                    ImageUrl = response.Url,
                    Color = ConfigJson.DefaultColor.DiscordColor,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = $"Copyright: {response.Copyright} • Data deste APOD: {response.Date.ToString("d", new System.Globalization.CultureInfo("pt-BR"))}",
                    }
                }.AddField("👨🏻‍🚀 Contexto da Imagem:", translatedExplanation);

                return discordEmbedBuilder;
            }
        }
    }
}
