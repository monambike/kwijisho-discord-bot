using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace KWiJisho.Models.Apis
{
    /// <summary>
    /// API Documentation: https://api.nasa.gov/
    /// </summary>
    internal static class NasaApi
    {
        /// <summary>
        /// HttpClient for making api requests.
        /// </summary>
        readonly static HttpClient HttpClient = new() { BaseAddress = new Uri("https://api.nasa.gov") };

        internal static class Apod
        {
            /// <summary>
            /// Makes a image request for APOD, also known as Astronomy Picture of the Day.
            /// </summary>
            internal static async Task<ApodResponseJson> GetAsync()
            {
                var response = await GetUsingNasaApiAsync("planetary/apod");
                return JsonConvert.DeserializeObject<ApodResponseJson>(response);
            }

            /// <summary>
            /// Makes a request for the nasa API.
            /// </summary>
            /// <param name="request"></param>
            /// <returns></returns>
            private static async Task<string> GetUsingNasaApiAsync(string request) => await HttpService.GetAsync(HttpClient, $"{request}?api_key={ConfigJson.NasaToken}");

            internal class ApodResponseJson
            {
                /// <summary>
                /// Title of the current Astronomy Picture of the Day.
                /// </summary>
                [JsonProperty("title")]
                internal string Title { get; set; }

                /// <summary>
                /// Explanation of the current Astronomy Picture of the Day.
                /// </summary>
                [JsonProperty("explanation")]
                internal string Explanation { get; set; }

                /// <summary>
                /// Date of the current Astronomy Picture of the Day.
                /// </summary>
                [JsonProperty("date")]
                internal DateTime Date { get; set; }

                /// <summary>
                /// Image URL of the current Astronomy Picture of the Day.
                /// </summary>
                [JsonProperty("url")]
                internal string Url { get; set; }

                /// <summary>
                /// Copyright of the current Astronomy Picture of the Day.
                /// </summary>
                [JsonProperty("copyright")]
                internal string Copyright { get; set; }
            }
        }

    }
}
