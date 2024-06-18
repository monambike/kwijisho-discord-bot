// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus.Entities;
using KWiJisho.APIs;
using KWiJisho.Config;
using KWiJisho.Utils;
using System;
using System.Threading.Tasks;
using static KWiJisho.APIs.NasaApi.Apod;

namespace KWiJisho.Models
{
    internal class Nasa
    {
        /// <summary>
        /// Asynchronously builds a english apod message with the apod response.
        /// </summary>
        /// <param name="apodResponse">The APOD response content to build the embed.</param>
        /// <returns>The <see cref="DiscordEmbedBuilder"/>.</returns>
        internal static ApodBuilder BuildEnglishApodMessageAsync(ApodResponse apodResponse)
            => new()
            {
                // Title
                TitleField = "IMAGE OF THE DAY",
                TitleContent = apodResponse.Title,
                // Explanation
                ExplanationField = "Read the following history related to the image",
                ExplanationContent = apodResponse.Explanation,
                // Media
                VideoField = $"{"APOD Video (YouTube)".ToDiscordBold() + Environment.NewLine}Check out this {"YouTube video".ToDiscordLink(apodResponse.Url)} from today's APOD!",
                MediaUrl = apodResponse.Url,
                MediaType = apodResponse.MediaType,
                // Copyright
                CopyrightContent = apodResponse.Copyright,
                NullCopyrightField = "no copyright",
                // Date
                DateField = "APOD date",
                DateContent = apodResponse.Date,
                // Culture info
                DateFormat = "MM/dd/yyyy"
            };

        /// <summary>
        /// Asynchronously builds a portuguese apod message with the apod response.
        /// </summary>
        /// <param name="apodResponse">The APOD response content to build the embed.</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the <see cref="DiscordEmbedBuilder"/>.</returns>
        internal static async Task<ApodBuilder> BuildPortugueseApodMessageAsync(ApodResponse apodResponse)
        {
            // Translate the title from the APOD response.
            var translatedTitle = await ChatGPT.GetPromptTranslateToPortugueseAsync(apodResponse.Title);
            // The final formatted title ready for use.
            var title = translatedTitle;

            // Summarize the detailed explanation from the APOD response.
            var summarizedExplanation = await ChatGPT.GetPromptSummarizeTextAsync(apodResponse.Explanation);
            // Translate the summarized explanation to Portuguese.
            var translatedExplanation = await ChatGPT.GetPromptTranslateToPortugueseAsync(summarizedExplanation);
            // Format the translated explanation by adding new lines after each sentence.
            var formattedExplanation = translatedExplanation;
            // The final formatted explanation ready for use.
            var explanation = formattedExplanation;

            // Building and returning the portuguese APOD message
            return new()
            {
                // Title
                TitleField = "IMAGEM DO DIA",
                TitleContent = title,
                // Explanation
                ExplanationField = "Leia essa histórinha que tem a ver com a imagem",
                ExplanationContent = explanation,
                // Media
                VideoField = $"{"Vídeo da APOD (YouTube)".ToDiscordBold() + Environment.NewLine}Confira esse {"vídeo do YouTube".ToDiscordLink(apodResponse.Url)} da APOD de hoje!",
                MediaUrl = apodResponse.Url,
                MediaType = apodResponse.MediaType,
                // Copyright
                CopyrightContent = apodResponse.Copyright,
                NullCopyrightField = "sem copyright",
                // Date
                DateField = "Data deste APOD",
                DateContent = apodResponse.Date,
                // Culture info
                DateFormat = "dd/MM/yyyy"
            };
        }

        /// <summary>
        /// Builder class for creating a Discord embed for Astronomy Picture of the Day (APOD) information.
        /// </summary>
        internal class ApodBuilder
        {
            private string _nullCopyrightField = null!;

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
            /// Gets or initializes the video URL field for the APOD embed.
            /// </summary>
            internal required string VideoField { get; init; }

            /// <summary>
            /// Gets or initializes the content URL for the APOD embed.
            /// </summary>
            internal required string MediaUrl { get; init; }

            /// <summary>
            /// Gets or initializes the media type for the APOD embed.
            /// </summary>
            internal required string MediaType { get; init; }

            /// <summary>
            /// Gets or initializes the copyright content for the APOD embed.
            /// </summary>
            internal required string CopyrightContent { get; init; }

            /// <summary>
            /// Gets or initializes the placeholder for null copyright field in the APOD embed.
            /// </summary>
            internal required string NullCopyrightField { get => $"({_nullCopyrightField})"; init { _nullCopyrightField = value; } }

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
                var copyright = string.IsNullOrEmpty(CopyrightContent) ? NullCopyrightField : CopyrightContent.Replace("\n", "");

                // Formatting explanation content, where's a full stop makes a new paragraph
                var explanationContent = ExplanationContent.Replace(". ", "." + Environment.NewLine + Environment.NewLine);

                // Creating embed builder.
                var discordEmbedBuilder = new DiscordEmbedBuilder
                {
                    Title = $"({TitleField}) {TitleContent.ToUpper()}",
                    Description = $"👇🏻 {ExplanationField.ToDiscordBold().ToUpper()}{Environment.NewLine + Environment.NewLine}{explanationContent}",
                    Color = ConfigJson.DefaultColor.DiscordColor,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = $"Copyright: {copyright} • {DateField}: {DateContent.Date.ToString(DateFormat)}",
                    }
                };

                // Returning the formatted discord embed builder
                return discordEmbedBuilder;
            }
        }
    }
}
