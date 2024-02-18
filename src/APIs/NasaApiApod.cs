using KWiJisho.Config;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace KWiJisho.APIs
{
    /// <summary>
    /// This class provides methods for interacting with the NASA's API using
    /// HTTP requests.
    /// API Documentation: <a href="https://api.nasa.gov/"/>
    /// </summary>
    internal static partial class NasaApi
    {
        /// <summary>
        /// This class provides methods for interacting with the APOD (Astronomy Picture of
        /// the Day) API using HTTP requests.
        /// </summary>
        internal static class Apod
        {
            /// <summary>
            /// Retrieves the Astronomy Picture of the Day (APOD) through a request to NASA's API.
            /// </summary>
            /// <returns>An asynchronous <see cref="Task"/> that represents the operation. The task result contains an <see cref="ApodResponse"/> object.</returns>
            internal static async Task<ApodResponse> GetApodAsync() => await GetUsingNasaApiAsync("planetary/apod");

            /// <summary>
            /// Makes a generic request to NASA's API with the specified endpoint.
            /// </summary>
            /// <param name="request">The endpoint for the API request.</param>
            /// <returns>An asynchronous task that represents the operation. The task result contains the API response as a string.</returns>
            private static async Task<ApodResponse> GetUsingNasaApiAsync(string request) => await HttpService.GetAsync<ApodResponse>($"{request}?api_key={ConfigJson.NasaToken}");

            /// <summary>
            /// Represents the APOD Json response for a NASA's API HTTP request.
            /// </summary>
            internal class ApodResponse
            {
                /// <summary>
                /// Title of the current Astronomy Picture of the Day.
                /// </summary>
                [JsonProperty("title")]
                internal required string Title { get; set; }

                /// <summary>
                /// Explanation of the current Astronomy Picture of the Day.
                /// </summary>
                [JsonProperty("explanation")]
                internal required string Explanation { get; set; }

                /// <summary>
                /// Date of the current Astronomy Picture of the Day.
                /// </summary>
                [JsonProperty("date")]
                internal required DateTime Date { get; set; }

                /// <summary>
                /// Image URL of the current Astronomy Picture of the Day.
                /// </summary>
                [JsonProperty("url")]
                internal required string Url { get; set; }

                /// <summary>
                /// Copyright of the current Astronomy Picture of the Day.
                /// </summary>
                [JsonProperty("copyright")]
                internal required string Copyright { get; set; }
            }
        }
    }
}
