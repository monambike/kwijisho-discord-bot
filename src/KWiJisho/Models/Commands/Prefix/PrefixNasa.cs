using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using ExtensionMethods;
using KWiJisho.Models.Apis;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace KWiJisho.Models.Commands.Prefix
{
    internal partial class PrefixCommandManager
    {
        internal class PrefixNasa : BaseCommandModule
        {
            internal PrefixCommand apod = new(nameof(apod), $"(APOD - Astronomy Picture of the Day) Te trago a imagem do dia fresquinha diretamente do site da Nasa! Com uma descrição traduzida por mim é claro uwu", Astronomy);
            [Command(nameof(apod))]
            internal async Task ApodAsync(CommandContext commandContext)
            {
                var message = await GenerateApodDiscordMessageBuilderAsync(commandContext);
                await commandContext.Channel.SendMessageAsync(message);
            }


            internal PrefixCommand apodResume = new(nameof(apodResume), $"Te trago o mesmo conteúdo do comando {"!apod".ToDiscordBold()} mas mais fácil e divertido de ler! (Texto Resumido)", Astronomy);
            [Command(nameof(apodResume))]
            internal async Task ApodResumeAsync(CommandContext commandContext)
            {
                var message = await GenerateApodDiscordMessageBuilderAsync(commandContext);
                await commandContext.Channel.SendMessageAsync(message);
            }

            private async Task<DiscordEmbedBuilder> GenerateApodDiscordMessageBuilderAsync(CommandContext commandContext)
            {
                // Getting the picture of the day
                var response = await NasaApi.Apod.GetAsync();
                
                // Translate title
                var translatedTitle = await OpenAiApi.GetPromptTranslateToPortugueseAsync(response.Title);

                // Translate explanation with the specified translation type
                await commandContext.TriggerTypingAsync();
                var summarizedExplanation = await OpenAiApi.GetPromptSummarizeTextAsync(response.Explanation);
                var translatedExplanation = await OpenAiApi.GetPromptTranslateToPortugueseAsync(summarizedExplanation);
                var formattedExplanation = translatedExplanation.Replace(". ", "." + Environment.NewLine);

                // Creating copyright message
                var copyright = string.IsNullOrEmpty(response.Copyright) ? "(sem copyright)" : response.Copyright;

                // Creating embed builder
                var discordEmbedBuilder = new DiscordEmbedBuilder
                {
                    Title = $"(IMAGEM DO DIA) {translatedTitle.ToUpper()}",
                    Color = ConfigJson.DefaultColor.DiscordColor,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = $"Copyright: {copyright} • Data deste APOD: {response.Date.ToString("d", new System.Globalization.CultureInfo("pt-BR"))}",
                    }
                }.WithImageUrl(response.Url).AddField("👇🏻 Leia essa histórinha que tem a ver com a imagem", Environment.NewLine + formattedExplanation);

                return discordEmbedBuilder;
            }

            private async Task<DiscordEmbedBuilder> BuildApodMessage(CommandContext commandContext)
            {
                var thisApodDateText = "Data deste APOD";
                var copyrightText = "(sem copyright)";
                var cultureInfo = new System.Globalization.CultureInfo("pt-BR");

                DateTime dateContent = DateTime.Now;
                var titleContent = "";
                var copyrightContent = "";
                var explanationContent = "";
                var urlContent = "";

                // Creating copyright message
                var copyright = string.IsNullOrEmpty(copyrightContent) ? copyrightText : copyrightContent;

                // Creating embed builder
                var discordEmbedBuilder = new DiscordEmbedBuilder
                {
                    Title = $"(IMAGEM DO DIA) {titleContent.ToUpper()}",
                    Color = ConfigJson.DefaultColor.DiscordColor,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = $"Copyright: {copyright} • {thisApodDateText}: {dateContent.Date.ToString("d", cultureInfo)}",
                    }
                }.WithImageUrl(urlContent).AddField("👇🏻 Leia essa histórinha que tem a ver com a imagem", Environment.NewLine + explanationContent);

                return discordEmbedBuilder;
            }

            internal class ApodBuilder
            {
                internal string ApodDateText { get; set; }
                internal string CopyrightText { get; set; }
                internal CultureInfo CultureInfo { get; set; }

                internal DateTime DateContent { get; set; }
                internal string ExplanationContent { get; set; }
                internal string UrlContent { get; set; }

            }
        }
    }
}
