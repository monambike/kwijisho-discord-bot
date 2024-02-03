using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using KWiJisho.APIs;
using KWiJisho.Utils;
using System;
using System.Threading.Tasks;
using static KWiJisho.APIs.NasaApi.Apod;

namespace KWiJisho.Commands.Prefix
{
    /// <summary>
    /// Provides a set of commands and classes for managing commands in a Discord server.
    /// </summary>
    internal partial class PrefixCommandManager
    {
        /// <summary>
        /// Represents a set of prefix NASA commands.
        /// </summary>
        internal class PrefixNasa : BaseCommandModule
        {
            /// <summary>
            /// Represent the command to get the APOD at its current default state.
            /// </summary>
            internal PrefixCommand apod = new(nameof(apod), $"(APOD - Astronomy Picture of the Day) Te trago a imagem do dia fresquinha diretamente do site da Nasa! Com uma descrição traduzida por mim é claro uwu", Astronomy);

            /// <summary>
            /// Represent the command to get the APOD resumed and translated to portuguese.
            /// </summary>
            internal PrefixCommand apodResume = new(nameof(apodResume), $"Te trago o mesmo conteúdo do comando {"!apod".ToDiscordBold()} mas mais fácil e divertido de ler! (Texto Resumido)", Astronomy);

            /// <summary>
            /// Represents the asynchronous prefix APOD method callend when user asks for the APOD command.
            /// </summary>
            /// <param name="commandContext">The command context from the current command call.</param>
            /// <returns>A <see cref="Task"/> from the current asynchronous task.</returns>
            [Command(nameof(apod))]
            internal async Task ApodAsync(CommandContext commandContext)
            {
                // Triggering typing async so user understand that the bot is processing.
                await commandContext.TriggerTypingAsync();

                // Getting the picture of the day.
                var response = await GetApodAsync();

                // Creating embed builder.
                var discordEmbedBuilder = BuildEnglishApodMessageAsync(response);

                // Sending the generated discord embed builder.
                await commandContext.Channel.SendMessageAsync(discordEmbedBuilder);
            }

            /// <summary>
            /// Represents the asynchronous prefix APOD resume method callend when user asks for the APOD resume command.
            /// </summary>
            /// <param name="commandContext">The command context from the current command call.</param>
            /// <returns>A <see cref="Task"/> from the current asynchronous task.</returns>
            /// <returns></returns>
            [Command(nameof(apodResume))]
            internal async Task ApodResumeAsync(CommandContext commandContext)
            {
                // Triggering typing async so user understand that the bot is processing.
                await commandContext.TriggerTypingAsync();

                // Getting the picture of the day.
                var response = await GetApodAsync();

                // Creating embed builder.
                var discordEmbedBuilder = await BuildPortgueseApodMessageAsync(response);

                // Sending the generated discord embed builder.
                await commandContext.Channel.SendMessageAsync(discordEmbedBuilder);
            }

            /// <summary>
            /// Asynchronously builds a english apod message with the apod response.
            /// </summary>
            /// <param name="apodResponse">The APOD response content to build the embed.</param>
            /// <returns>The <see cref="DiscordEmbedBuilder"/>.returns>
            internal static DiscordEmbedBuilder BuildEnglishApodMessageAsync(ApodResponse apodResponse)
                => new ApodBuilder
                {
                    // Title
                    TitleField = "IMAGE OF THE DAY",
                    TitleContent = apodResponse.Title,
                    // Explanation
                    ExplanationField = "Read the following history according to the image",
                    ExplanationContent = apodResponse.Explanation,
                    // Image
                    ImageUrlContent = apodResponse.Url,
                    // Copyright
                    CopyrightContent = apodResponse.Copyright,
                    NullCopyrightField = "no copyright",
                    // Date
                    DateField = "APOD date",
                    DateContent = apodResponse.Date,
                    // Culture info
                    DateFormat = "MM/dd/yyyy"
                }.GetDiscordEmbedBuilder();

            /// <summary>
            /// Asynchronously builds a portuguese apod message with the apod response.
            /// </summary>
            /// <param name="apodResponse">The APOD response content to build the embed.</param>
            /// <returns>A <see cref="Task{TResult}"/> containing the <see cref="DiscordEmbedBuilder"/>.</returns>
            internal static async Task<DiscordEmbedBuilder> BuildPortgueseApodMessageAsync(ApodResponse apodResponse)
            {
                // Translate the title from the APOD response.
                var translatedTitle = await OpenAiApi.GetPromptTranslateToPortugueseAsync(apodResponse.Title);
                // The final formatted title ready for use.
                var title = translatedTitle;

                // Summarize the detailed explanation from the APOD response.
                var summarizedExplanation = await OpenAiApi.GetPromptSummarizeTextAsync(apodResponse.Explanation);
                // Translate the summarized explanation to Portuguese.
                var translatedExplanation = await OpenAiApi.GetPromptTranslateToPortugueseAsync(summarizedExplanation);
                // Format the translated explanation by adding new lines after each sentence.
                var formattedExplanation = translatedExplanation.Replace(". ", "." + Environment.NewLine);
                // The final formatted explanation ready for use.
                var explanation = formattedExplanation;

                // Building the portuguese APOD message
                var portugueseApodBuilder = new ApodBuilder
                {
                    // Title
                    TitleField = "IMAGEM DO DIA",
                    TitleContent = title,
                    // Explanation
                    ExplanationField = "Leia essa histórinha que tem a ver com a imagem",
                    ExplanationContent = explanation,
                    // Image
                    ImageUrlContent = apodResponse.Url,
                    // Copyright
                    CopyrightContent = apodResponse.Copyright,
                    NullCopyrightField = "sem copyright",
                    // Date
                    DateField = "Data deste APOD",
                    DateContent = apodResponse.Date,
                    // Culture info
                    DateFormat = "dd/MM/yyyy"
                }.GetDiscordEmbedBuilder();

                // Returning the portuguese APOD message
                return portugueseApodBuilder;
            }

            /// <summary>
            /// Builder class for creating a Discord embed for Astronomy Picture of the Day (APOD) information.
            /// </summary>
            internal class ApodBuilder
            {
                private string _nullCopyrightField;

                /// <summary>
                /// Gets or initializes the title field for the APOD embed.
                /// </summary>
                internal required string TitleField { get; init; }

                /// <summary>
                /// Gets or initializes the title content for the APOD embed.
                /// </summary>
                internal required string TitleContent { get; init; }

                /// <summary>
                /// Gets or initializes the explanation field for the APOD embed.
                /// </summary>
                internal required string ExplanationField { get; init; }

                /// <summary>
                /// Gets or initializes the explanation content for the APOD embed.
                /// </summary>
                internal required string ExplanationContent { get; init; }

                /// <summary>
                /// Gets or initializes the image URL content for the APOD embed.
                /// </summary>
                internal required string ImageUrlContent { get; init; }

                /// <summary>
                /// Gets or initializes the copyright content for the APOD embed.
                /// </summary>
                internal required string CopyrightContent { get; init; }

                /// <summary>
                /// Gets or initializes the placeholder for null copyright field in the APOD embed.
                /// </summary>
                internal required string NullCopyrightField { get => $"({_nullCopyrightField})"; init { _nullCopyrightField = value; }}

                /// <summary>
                /// Gets or initializes the date field for the APOD embed.
                /// </summary>
                internal required string DateField { get; init; }

                /// <summary>
                /// Gets or initializes the date content for the APOD embed.
                /// </summary>
                internal DateTime DateContent { get; init; }

                /// <summary>
                /// Gets or initializes the date format for the date in the APOD embed.
                /// </summary>
                internal required string DateFormat { get; init; }

                /// <summary>
                /// Builds and returns a Discord embed builder based on the provided APOD information.
                /// </summary>
                /// <returns>A <see cref="DiscordEmbedBuilder"/> representing the APOD information in embed format.</returns>
                internal DiscordEmbedBuilder GetDiscordEmbedBuilder()
                {
                    // Creating copyright message.
                    var copyright = string.IsNullOrEmpty(CopyrightContent) ? NullCopyrightField : CopyrightContent;

                    // Creating embed builder.
                    var discordEmbedBuilder = new DiscordEmbedBuilder
                    {
                        Title = $"({TitleField}) {TitleContent.ToUpper()}",
                        Color = Data.ConfigJson.DefaultColor.DiscordColor,
                        Footer = new DiscordEmbedBuilder.EmbedFooter
                        {
                            Text = $"Copyright: {copyright} • {DateField}: {DateContent.Date.ToString(DateFormat)}",
                        }
                    }.WithImageUrl(ImageUrlContent).AddField($"👇🏻 {ExplanationField}", $"{Environment.NewLine}{ExplanationContent}");

                    // Returning the formatted discord embed builder
                    return discordEmbedBuilder;
                }
            }
        }
    }
}
