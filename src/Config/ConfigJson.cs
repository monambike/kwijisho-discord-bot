using KWiJisho.Data;
using KWiJisho.Entities;
using System;
using System.Drawing;
using static KWiJisho.Commands.Prefix.PrefixCommandManager;

namespace KWiJisho.Config
{
    internal class ConfigJson
    {
        /// <summary>
        /// Gets or sets the command prefix for the bot.
        /// </summary>
        internal static string Prefix => "!";

        /// <summary>
        /// Gets or sets the default bot activity.
        /// </summary>
        internal static string Activity => $"{Prefix}{nameof(PrefixInfo.help)} | Sou a Kawaii Jisho! 💫";

        /// <summary>
        /// Gets or sets the default color for bot messages.
        /// </summary>
        internal static Entities.Color DefaultColor => Colors.KWiJishoColor;

        /// <summary>
        /// Gets or sets the API token for KWiJisho.
        /// </summary>
        internal static string KWiJishoToken { get; set; }

        /// <summary>
        /// Gets or sets the API token for ChatGPT.
        /// </summary>
        internal static string ChatGptToken { get; set; }

        /// <summary>
        /// Gets or sets the API token for NASA.
        /// </summary>
        internal static string NasaToken { get; set; }

        /// <summary>
        /// Set values from ConfigJson file.
        /// </summary>
        /// <param name="configJson">The instance of the ConfigJson file.</param>
        internal static void SetValuesFromConfigJson(Entities.ConfigJson configJson)
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
