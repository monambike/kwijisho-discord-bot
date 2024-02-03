using DSharpPlus.Entities;
using Newtonsoft.Json;

namespace KWiJisho.Modules.Utils
{
    /// <summary>
    /// Represents an RGB color with red, green, and blue components.
    /// </summary>
    internal class Color(byte red, byte green, byte blue)
    {
        /// <summary>
        /// Gets or sets the red component of the RGB color.
        /// </summary>
        [JsonProperty("red")]
        internal byte Red { get; set; } = red;

        /// <summary>
        /// Gets or sets the green component of the RGB color.
        /// </summary>
        [JsonProperty("green")]
        internal byte Green { get; set; } = green;

        /// <summary>
        /// Gets or sets the blue component of the RGB color.
        /// </summary>
        [JsonProperty("blue")]
        internal byte Blue { get; set; } = blue;

        /// <summary>
        /// Gets a Discord color representation based on the RGB components.
        /// </summary>
        internal DiscordColor DiscordColor => new(Red, Green, Blue);

        /// <summary>
        /// Gets a Hexadecimal color representation based on the RGB components.
        /// </summary>
        internal string Hexadecimal => $"{Red:X2}{Green:X2}{Blue:X2}";
    }
}
