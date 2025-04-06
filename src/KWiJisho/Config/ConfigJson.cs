// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using KWiJisho.Data;
using System;
using static KWiJisho.Commands.Prefix.PrefixCommandManager;

namespace KWiJisho.Config
{
    /// <summary>
    /// Represents sets of configurations for the Discord bot that can also be retrieved by the JSON file.
    /// </summary>
    public class ConfigJson
    {
        /// <summary>
        /// Gets or sets the command prefix for the bot.
        /// </summary>
        public static string Prefix => "!";

        /// <summary>
        /// Gets or sets the default bot activity.
        /// </summary>
        public static string Activity => $"{Prefix}{nameof(PrefixInfo.help)} | Sou a Kawaii Jisho! 💫";

        /// <summary>
        /// Gets or sets the enable of ChatGPT.
        /// </summary>
        public static bool EnableChatGpt => true;

        /// <summary>
        /// Gets or sets the model of ChatGPT.
        /// </summary>
        public static string ChatGptModel = "gpt-4-0125-preview";

        /// <summary>
        /// Gets or sets the default color for bot messages.
        /// </summary>
        public static Entities.Color DefaultColor => Colors.KWiJishoColor;

        /// <summary>
        /// Gets or sets the API token for KWiJisho.
        /// </summary>
        public static string KWiJishoToken { get; set; } = null!;

        /// <summary>
        /// Gets or sets the API token for ChatGPT.
        /// </summary>
        public static string ChatGptToken { get; set; } = null!;

        /// <summary>
        /// Gets or sets the API token for NASA.
        /// </summary>
        public static string NasaToken { get; set; } = null!;

        /// <summary>
        /// Set values from ConfigJson file.
        /// </summary>
        /// <param name="configJson">The instance of the ConfigJson file.</param>
        public static void SetValuesFromConfigJson(Entities.ConfigJson configJson)
        {
            // Getting KWiJisho's Discord bot Token
            KWiJishoToken = Environment.GetEnvironmentVariable("KWIJISHO_TOKEN") ?? configJson.KWiJishoToken;

            // Getting ChatGPT's Token
            ChatGptToken = Environment.GetEnvironmentVariable("KWIJISHO_CHATGPT_TOKEN") ?? configJson.ChatGptToken;

            // Getting NASA's Token
            NasaToken = Environment.GetEnvironmentVariable("KWIJISHO_NASA_TOKEN") ?? configJson.NasaToken;
        }
    }
}
