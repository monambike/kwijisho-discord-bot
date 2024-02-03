using DSharpPlus.Entities;
using KWiJisho.Modules.Utils;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace KWiJisho.Modules
{
    /// <summary>
    /// Class that represents the ConfigJson file for configuring the discord bot.
    /// </summary>
    internal struct ConfigJson
    {
        // Those properties are for easier data manipulation since instances are not necessary,
        // since there's only one config.json file.

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
        /// Gets or sets the command prefix for the bot.
        /// </summary>
        internal static string Prefix { get; set; }

        /// <summary>
        /// Gets or sets the default bot activity.
        /// </summary>
        internal static string Activity { get; set; }

        /// <summary>
        /// Gets or sets the default color for bot messages.
        /// </summary>
        internal static Color DefaultColor { get; set; }

        // Even thought we don't need instances because there's only one config.json file, deserializing
        // needs a new instance object.
        // Note: Notice that they update the internal static properties on their value set.

        private string _configJsonKWiJishoToken;
        [JsonProperty("kwijishoToken")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "It's being used in Json deserialization.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0251:Make member 'readonly'", Justification = "It's being set on Json file.")]
        private string ConfigJsonKWiJishoToken
        {
            get => _configJsonKWiJishoToken; set
            {
                // Getting the token from the config json file
                _configJsonKWiJishoToken = value;
                // Tries to get the token from the environment variable
                var kwijishoToken = Environment.GetEnvironmentVariable("KWIJISHO_TOKEN");
                // If the token from the environment variables is null, value will be set using the config json file values
                KWiJishoToken = kwijishoToken ?? _configJsonKWiJishoToken;
            }
        }

        private string _configJsonChatGptToken;
        [JsonProperty("chatgptToken")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "It's being used in Json deserialization.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0251:Make member 'readonly'", Justification = "It's being set on Json file.")]
        private string ConfigJsonChatGptToken
        {
            get => _configJsonChatGptToken; set
            {
                // Getting the token from the config json file
                _configJsonChatGptToken = value;
                // Tries to get the token from the environment variable
                var chatgptToken = Environment.GetEnvironmentVariable("KWIJISHO_CHATGPT_TOKEN");
                // If the token from the environment variables is null, value will be set using the config json file values
                ChatGptToken = chatgptToken ?? _configJsonChatGptToken;
            }
        }

        private string _configJsonNasaToken;
        [JsonProperty("nasaToken")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "It's being used in Json deserialization.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0251:Make member 'readonly'", Justification = "It's being set on Json file.")]
        private string ConfigJsonNasaToken
        {
            get => _configJsonNasaToken; set
            {
                // Getting the token from the config json file
                _configJsonNasaToken = value;
                // Tries to get the token from the environment variable
                var nasaToken = Environment.GetEnvironmentVariable("KWIJISHO_NASA_TOKEN");
                // If the token from the environment variables is null, value will be set using the config json file values
                NasaToken = nasaToken ?? _configJsonNasaToken;
            }
        }

        private string _configJsonPrefix;
        [JsonProperty("prefix")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "It's being used in Json deserialization.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0251:Make member 'readonly'", Justification = "It's being set on Json file.")]
        private string ConfigJsonPrefix { get => _configJsonPrefix; set => _configJsonPrefix = Prefix = value; }

        private string _configJsonActivity;
        [JsonProperty("activity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "It's being used in Json deserialization.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0251:Make member 'readonly'", Justification = "It's being set on Json file.")]
        private string ConfigJsonActivity { get => _configJsonActivity; set => _configJsonActivity = Activity = value; }

        private Color _configJsonPurpleColor;
        [JsonProperty("purpleColor")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "It's being used in Json deserialization.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0251:Make member 'readonly'", Justification = "It's being set on Json file.")]
        private Color ConfigJsonPurpleColor { get => _configJsonPurpleColor; set => _configJsonPurpleColor = DefaultColor = value; }

        /// <summary>
        /// Asynchronously deserializes the content of the config.json file into a <see cref="ConfigJson"/> instance. The <see langword="static"/> properties
        /// from <see cref="ConfigJson"/> will receive the values.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation, yielding a <see cref="ConfigJson"/> instance.</returns>
        internal static async Task<ConfigJson> DeserializeConfigJsonFileAsync()
        {
            // The variable to hold the JSON content.
            var json = string.Empty;

            // Opening config.json file and reading its content.
            using (var fileSteam = File.OpenRead("config.json"))
            using (var streamReader = new StreamReader(fileSteam, new UTF8Encoding(false)))
                // Getting Json from the read and setting into the variable.
                json = await streamReader.ReadToEndAsync().ConfigureAwait(false);
            // Deserializing Json from the string and returning as result.
            return JsonConvert.DeserializeObject<ConfigJson>(json);
        }
    }
}
