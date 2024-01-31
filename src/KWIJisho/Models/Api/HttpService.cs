using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace KWiJisho.Models.Api
{
    /// <summary>
    /// A simple HTTP service for making GET requests and retrieving the response content.
    /// </summary>
    internal static class HttpService
    {
        /// <summary>
        /// Sends an asynchronous GET request to the specified URI and returns the response content as a string.
        /// </summary>
        /// <param name="httpClient">The HttpClient instance to use for the request.</param>
        /// <param name="strRequest">The URI of the resource to request.</param>
        /// <returns>A Task representing the asynchronous operation. The result contains the response content as a string.</returns>
        internal static async Task<string> GetAsync(HttpClient httpClient, string strRequest)
        {
            // Tries to make the request with the base address and the request
            // for URI validation
            Uri.TryCreate($"{httpClient.BaseAddress}{strRequest}", UriKind.Absolute, out Uri request);

            // If current URI is valid, makes the requisition and returns the response
            using HttpResponseMessage response = await httpClient.GetAsync(request);

            // Returning assynchronously the content response as string
            return await response.Content.ReadAsStringAsync();
        }
    }
}
