using Newtonsoft.Json;
using System;
using System.Net.Http;
using static KWiJisho.APIs.NasaApi.Apod;
using System.Threading.Tasks;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        /// HttpClient responsible for making api requests.
        /// </summary>
        readonly static HttpClient HttpClient = new() { BaseAddress = new Uri("https://api.nasa.gov") };

        /// <summary>
        /// 
        /// </summary>
        /// <returns>An asynchronous <see cref="Task"/> that represents the operation. The task result contains an <see cref="ApodResponse"/> object.</returns>
        internal static async Task<ApodResponse> GetHttpCatByStringAsync(string myString)
        {
            // Tries to find a enum with the provided string.
            var myEnum = (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), myString);

            // Make an asynchronous request to Cat's API to get the HTTP Cat data.
            var response = await GetHttpCatAsync(myEnum);

            // Deserialize the string JSON response into an ApodResponseJson object.
            return JsonConvert.DeserializeObject<ApodResponse>(response);
        }

        /// <summary>
        /// Makes a generic request to Cat's API with the specified endpoint.
        /// </summary>
        /// <param name="httpStatusCode">The endpoint for the API request.</param>
        /// <returns>An asynchronous task that represents the operation. The task result contains the API response as a string.</returns>
        private static async Task<string> GetHttpCatAsync(HttpStatusCode httpStatusCode) => await HttpService.GetAsync(HttpClient, $"{httpStatusCode}");

    }
}
