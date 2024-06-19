// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using Monambike.Core.Services;
using System;
using System.Net;
using System.Threading.Tasks;
using static KWiJisho.APIs.NasaApi.Apod;

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
        /// Gets the HTTP service instance for making API requests.
        /// </summary>
        private static HttpService HttpService => new("https://api.nasa.gov");

        /// <summary>
        /// 
        /// </summary>
        /// <returns>An asynchronous <see cref="Task"/> that represents the operation. The task result contains an <see cref="ApodResponse"/> object.</returns>
        public static async Task<ApodResponse> GetHttpCatByStringAsync(string myString)
        {
            // Tries to find a enum with the provided string.
            var myEnum = (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), myString);

            // Make an asynchronous request to Cat's API to get the HTTP Cat data.
            return await GetHttpCatAsync(myEnum);
        }

        /// <summary>
        /// Makes a generic request to Cat's API with the specified endpoint.
        /// </summary>
        /// <param name="httpStatusCode">The endpoint for the API request.</param>
        /// <returns>An asynchronous task that represents the operation. The task result contains the API response as a string.</returns>
        private static async Task<ApodResponse> GetHttpCatAsync(HttpStatusCode httpStatusCode) => await HttpService.GetAsync<ApodResponse>(httpStatusCode.ToString());

    }
}
