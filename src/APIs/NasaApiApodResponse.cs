// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using Newtonsoft.Json;
using System;

namespace KWiJisho.APIs
{
    internal static partial class NasaApi
    {
        internal static partial class Apod
        {

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

                /// <summary>
                /// Media type of the current Astronomy Picture of the Day.
                /// </summary>
                [JsonProperty("media_type")]
                internal required string MediaType { get; set; }

                /// <summary>
                /// Represents errors ocurred on Json response for a NASA's API HTTP request.
                /// </summary>
                internal class Error
                {
                    /// <summary>
                    /// Error code for the current Astronomy Picture of the Day.
                    /// </summary>
                    [JsonProperty("code")]
                    internal required string Code { get; set; }

                    /// <summary>
                    /// Error message for the current Astronomy Picture of the Day.
                    /// </summary>
                    [JsonProperty("msg")]
                    internal required string Message { get; set; }

                    /// <summary>
                    /// Error application service version for the current Astronomy Picture of the Day.
                    /// </summary>
                    [JsonProperty("service_version")]
                    internal required string ServiceVersion { get; set; }
                }
            }
        }
    }
}
