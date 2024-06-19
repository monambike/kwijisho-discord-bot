// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using KWiJisho.Config;
using System.Threading.Tasks;

namespace KWiJisho.APIs
{
    /// <summary>
    /// This class provides methods for interacting with the NASA's API using
    /// HTTP requests.
    /// API Documentation: <a href="https://api.nasa.gov/"/>
    /// </summary>
    public static partial class NasaApi
    {
        /// <summary>
        /// This class provides methods for interacting with the APOD (Astronomy Picture of
        /// the Day) API using HTTP requests.
        /// </summary>
        public static partial class Apod
        {
            /// <summary>
            /// Retrieves the Astronomy Picture of the Day (APOD) through a request to NASA's API.
            /// </summary>
            /// <returns>An asynchronous <see cref="Task"/> that represents the operation. The task result contains an <see cref="ApodResponse"/> object.</returns>
            public static async Task<ApodResponse> GetApodAsync() => await GetUsingNasaApiAsync("planetary/apod");

            /// <summary>
            /// Makes a generic request to NASA's API with the specified endpoint.
            /// </summary>
            /// <param name="request">The endpoint for the API request.</param>
            /// <returns>An asynchronous task that represents the operation. The task result contains the API response as a string.</returns>
            private static async Task<ApodResponse> GetUsingNasaApiAsync(string request) => await HttpService.GetAsync<ApodResponse>($"{request}?api_key={ConfigJson.NasaToken}");
        }
    }
}
