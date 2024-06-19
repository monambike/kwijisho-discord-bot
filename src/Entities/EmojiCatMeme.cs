// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using KWiJisho.Data;

namespace KWiJisho.Entities
{
    /// <summary>
    /// Represents a custom Discord emoji for cat meme emojis, inheriting from the base <see cref="Emoji"/> class.
    /// </summary>
    public record EmojiCatMeme : Emoji
    {
        /// <summary>
        /// Initializes a new instance of <see cref="EmojiCatMeme"/> with its emoji name and Id.
        /// </summary>
        /// <param name="name">The <see cref="EmojiCatMeme"/> emoji name.</param>
        /// <param name="id">The <see cref="EmojiCatMeme"/> emoji Id.</param>
        public EmojiCatMeme(string name, ulong id) : base(name, id)
            => DiscordEmojis.AnimatedMemeEmojis.Add(this);
    }
}
