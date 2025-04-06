// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using Newtonsoft.Json;
using System;

namespace KWiJisho.APIs
{
    // Partial implementation of NasaApi
    public static partial class NasaApi
    {
        // Partial implementation of NasaApi.Apod
        public static partial class Apod
        {

            /// <summary>
            /// Represents the APOD Json response for a NASA's API HTTP request.
            /// </summary>
            public class ApodResponse
            {
                /// <summary>
                /// Title of the current Astronomy Picture of the Day.
                /// </summary>
                [JsonProperty("title")]
                public required string Title { get; set; }

                /// <summary>
                /// Explanation of the current Astronomy Picture of the Day.
                /// </summary>
                [JsonProperty("explanation")]
                public required string Explanation { get; set; }

                /// <summary>
                /// Date of the current Astronomy Picture of the Day.
                /// </summary>
                [JsonProperty("date")]
                public required DateTime Date { get; set; }

                /// <summary>
                /// Image URL of the current Astronomy Picture of the Day.
                /// </summary>
                [JsonProperty("url")]
                public required string Url { get; set; }

                /// <summary>
                /// Copyright of the current Astronomy Picture of the Day.
                /// </summary>
                [JsonProperty("copyright")]
                public required string Copyright { get; set; }

                /// <summary>
                /// Media type of the current Astronomy Picture of the Day.
                /// </summary>
                [JsonProperty("media_type")]
                public required string MediaType { get; set; }

                /// <summary>
                /// Represents errors ocurred on Json response for a NASA's API HTTP request.
                /// </summary>
                public class Error
                {
                    /// <summary>
                    /// Error code for the current Astronomy Picture of the Day.
                    /// </summary>
                    [JsonProperty("code")]
                    public required string Code { get; set; }

                    /// <summary>
                    /// Error message for the current Astronomy Picture of the Day.
                    /// </summary>
                    [JsonProperty("msg")]
                    public required string Message { get; set; }

                    /// <summary>
                    /// Error application service version for the current Astronomy Picture of the Day.
                    /// </summary>
                    [JsonProperty("service_version")]
                    public required string ServiceVersion { get; set; }
                }
            }
        }
    }
}
