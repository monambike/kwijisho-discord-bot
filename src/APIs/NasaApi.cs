using System;
using System.Net.Http;

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
    }
}
