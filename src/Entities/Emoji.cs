// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using KWiJisho.Data;

namespace KWiJisho.Entities
{
    /// <summary>
    /// Class responsible for creating and handling custom Discord emojis.
    /// </summary>
    internal record Emoji
    {
        /// <summary>
        /// The discord emoji name.
        /// </summary>
        internal string Name { get; init; }

        /// <summary>
        /// The discord emoji unique identifier.
        /// </summary>
        internal ulong Id { get; init; }

        /// <summary>
        /// The discord emoji code responsible for outputing it into the chat.
        /// <c>&lt;a:{name}:{id}&gt;</c>
        /// </summary>
        internal string Code => $"<a:{Name}:{Id}>";


        /// <summary>
        /// Initializes a new instance of <see cref="Emoji"/> class with the specified
        /// name and identifier.
        /// </summary>
        /// <param name="name">The name of the Discord emoji.</param>
        /// <param name="id">The unique identifier of the Discord emoji.</param>
        internal Emoji(string name, ulong id)
        {
            // Setting values to the properties.
            (Name, Id) = (name, id);

            // Adding emoji to the main emoji list.
            DiscordEmojis.AnimatedEmojis.Add(this);
        }

        /// <summary>
        /// Converts the Discord emoji to its string representation, which is the Discord emoji code.
        /// </summary>
        /// <returns>The Discord emoji code in the format "<c>&lt;a:{<see cref="Name"/>}:{<see cref="Id"/>}&gt;</c>".</returns>
        public override string ToString() => Code;
    }
}
