using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace KWiJisho.Entities
{
    /// <summary>
    /// Class that represents the ConfigJson file for configuring the discord bot.
    /// </summary>
    internal struct ConfigJson
    {
        /// <summary>
        /// Gets or sets the Discord bot token for the KWiJisho module.
        /// </summary>
        [JsonProperty("kwijishoToken")]
        internal string KWiJishoToken { get; set; }

        /// <summary>
        /// Gets or sets the API token for accessing ChatGPT's services.
        /// </summary>
        [JsonProperty("chatgptToken")]
        internal string ChatGptToken { get; set; }

        /// <summary>
        /// Gets or sets the API token for accessing NASA's services.
        /// </summary>
        [JsonProperty("nasaToken")]
        internal string NasaToken { get; set; }

        /// <summary>
        /// Gets or sets the prefix used for Discord bot commands.
        /// </summary>
        [JsonProperty("prefix")]
        internal string Prefix { get; set; }

        /// <summary>
        /// Gets or sets the activity status message for the Discord bot.
        /// </summary>
        [JsonProperty("activity")]
        internal string Activity { get;set; }

        /// <summary>
        /// Gets or sets the default color used for Discord bot embeds.
        /// </summary>
        [JsonProperty("defaultColor")]
        internal Color DefaultColor { get; set; }

        /// <summary>
        /// Asynchronously deserializes the content of the config.json file into a <see cref="ConfigJson"/> instance.
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
