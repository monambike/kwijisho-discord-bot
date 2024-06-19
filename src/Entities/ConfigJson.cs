// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace KWiJisho.Entities
{
    /// <summary>
    /// Class that represents the ConfigJson file for configuring the discord bot.
    /// </summary>
    public struct ConfigJson
    {
        /// <summary>
        /// Gets or initializes the Discord bot token for the KWiJisho module.
        /// </summary>
        [JsonProperty("kwijishoToken")]
        public string KWiJishoToken { get; init; }

        /// <summary>
        /// Gets or initializes the API token for accessing ChatGPT's services.
        /// </summary>
        [JsonProperty("chatgptToken")]
        public string ChatGptToken { get; init; }

        /// <summary>
        /// Gets or initializes the API token for accessing NASA's services.
        /// </summary>
        [JsonProperty("nasaToken")]
        public string NasaToken { get; init; }

        /// <summary>
        /// Asynchronously deserializes the content of the config.json file into a <see cref="ConfigJson"/> instance.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation, yielding a <see cref="ConfigJson"/> instance.</returns>
        public static async Task<ConfigJson> DeserializeConfigJsonFileAsync()
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
