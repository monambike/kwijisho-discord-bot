using DSharpPlus.Entities;
using Newtonsoft.Json;
using OpenAI_API.Moderation;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace KWIJisho.Models
{
    internal struct ConfigJson
    {
        // Those properties are for easier data manipulation since instances are not necessary,
        // because there's only one config.json file.
        internal static string KWIJishoToken { get; set; }

        internal static string ChatGptToken { get; set; }

        internal static string NasaToken { get; set; }

        internal static string Prefix { get; set; }

        internal static string Activity { get; set; }

        internal static Color DefaultColor { get; set; }

        // Even thought we don't need instances because there's only one config.json file, deserializing
        // needs a new instance object. Those properties are for handling those deserializing values
        // Note: Notice that they update the internal static properties on their value set.
        private string _configJsonKWIJishoToken;
        [JsonProperty("kwijishoToken")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "It's being used in Json deserialization.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0251:Make member 'readonly'", Justification = "It's being set on Json file.")]
        private string ConfigJsonKWIJishoToken
        {
            get => _configJsonKWIJishoToken; set
            {
                _configJsonKWIJishoToken = value;

                var kwijishoToken = Environment.GetEnvironmentVariable("KWIJISHO_TOKEN");
                KWIJishoToken = kwijishoToken ?? _configJsonKWIJishoToken;
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
                _configJsonChatGptToken = value;

                var chatgptToken = Environment.GetEnvironmentVariable("CHATGPT_TOKEN");
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
                _configJsonNasaToken = value;

                var nasaToken = Environment.GetEnvironmentVariable("NASA_TOKEN");
                NasaToken = nasaToken ?? _configJsonNasaToken;
            }
        }

        private string _configJsonPrefix;
        [JsonProperty("prefix")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "It's being used in Json deserialization, since 'DeserializeObject' needs non-static property. And it's value is being bypassed to its static property.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0251:Make member 'readonly'", Justification = "It's being set on Json file.")]
        private string CPrefix { get => _configJsonPrefix; set => _configJsonPrefix = Prefix = value; }

        private string _configJsonActivity;
        [JsonProperty("activity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "It's being used in Json deserialization, since 'DeserializeObject' needs non-static property. And it's value is being bypassed to its static property.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0251:Make member 'readonly'", Justification = "It's being set on Json file.")]
        private string ConfigJsonActivity { get => _configJsonActivity; set => _configJsonActivity = Activity = value; }

        private Color _configJsonPurpleColor;
        [JsonProperty("purpleColor")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "It's being used in Json deserialization, since 'DeserializeObject' needs non-static property. And it's value is being bypassed to its static property.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0251:Make member 'readonly'", Justification = "It's being set on Json file.")]
        private Color ConfigJsonPurpleColor { get => _configJsonPurpleColor; set => _configJsonPurpleColor = DefaultColor = value; }

        internal static async Task<ConfigJson> DeserializeConfigJsonFileAsync()
        {

            var json = string.Empty;

            using (var fileSteam = File.OpenRead("config.json"))
            using (var streamReader = new StreamReader(fileSteam, new UTF8Encoding(false)))
                json = await streamReader.ReadToEndAsync().ConfigureAwait(false);

            var result = JsonConvert.DeserializeObject<ConfigJson>(json);
            return result;
        }

        internal class Color
        {
            [JsonProperty("red")]
            internal byte Red { get; set; }

            [JsonProperty("green")]
            internal byte Green { get; set; }

            [JsonProperty("blue")]
            internal byte Blue { get; set; }

            internal DiscordColor DiscordColor => new(Red, Green, Blue);
        }
    }
}
