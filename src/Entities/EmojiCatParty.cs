// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using KWiJisho.Data;

namespace KWiJisho.Entities
{
    /// <summary>
    /// Represents a custom Discord emoji for cat party emojis, inheriting from the base <see cref="Emoji"/> class.
    /// </summary>
    internal record EmojiCatParty : Emoji
    {
        /// <summary>
        /// Initializes a new instance of <see cref="EmojiCatParty"/> with its emoji name and Id.
        /// </summary>
        /// <param name="name">The <see cref="EmojiCatParty"/> emoji name.</param>
        /// <param name="id">The <see cref="EmojiCatParty"/> emoji Id.</param>
        public EmojiCatParty(string name, ulong id) : base(name, id)
            => DiscordEmojis.AnimatedPartyEmojis.Add(this);
    }
}
