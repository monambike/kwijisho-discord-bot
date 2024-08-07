﻿// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using System;
using System.Net.Http;

namespace KWiJisho.APIs
{
    /// <summary>
    /// This class provides methods for interacting with the Cat's API using
    /// HTTP requests.
    /// API GitHub Repository: <a href="https://github.com/httpcats/http.cat"/>
    /// </summary>
    public static class CatApi
    {
        /// <summary>
        /// HttpClient responsible for making api requests.
        /// </summary>
        readonly static HttpClient HttpClient = new() { BaseAddress = new Uri("https://http.cat") };
    }
}
