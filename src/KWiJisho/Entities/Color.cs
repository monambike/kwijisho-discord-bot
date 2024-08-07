﻿// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus.Entities;
using Newtonsoft.Json;

namespace KWiJisho.Entities
{
    /// <summary>
    /// Represents an RGB color with red, green, and blue components.
    /// </summary>
    public class Color(byte red, byte green, byte blue)
    {
        /// <summary>
        /// Gets or sets the red component of the RGB color.
        /// </summary>
        [JsonProperty("red")]
        public byte Red { get; init; } = red;

        /// <summary>
        /// Gets or sets the green component of the RGB color.
        /// </summary>
        [JsonProperty("green")]
        public byte Green { get; init; } = green;

        /// <summary>
        /// Gets or sets the blue component of the RGB color.
        /// </summary>
        [JsonProperty("blue")]
        public byte Blue { get; init; } = blue;

        /// <summary>
        /// Gets a Discord color representation based on the RGB components.
        /// </summary>
        public DiscordColor DiscordColor => new(Red, Green, Blue);

        /// <summary>
        /// Gets a Hexadecimal color representation based on the RGB components.
        /// </summary>
        public string Hexadecimal => $"{Red:X2}{Green:X2}{Blue:X2}";
    }
}
