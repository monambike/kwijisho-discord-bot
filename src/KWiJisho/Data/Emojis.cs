﻿// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using KWiJisho.Entities;
using System.Collections.Generic;

namespace KWiJisho.Data
{
    /// <summary>
    /// Static class that provides class, properties and methods for handling Discord emojis.
    /// </summary>
    public static class DiscordEmojis
    {
        // Every added emoji will come to this list.
        public static List<Emoji> AnimatedEmojis = [];

        public static List<EmojiCatMeme> AnimatedMemeEmojis = [];
        // Cat memes.
        public static EmojiCatMeme MemeChipiCat = new("cat_chipichipi", 1200609170567745566);
        public static EmojiCatMeme MemeAppleCat = new("cat_happycat_apple", 1200610022556704788);
        public static EmojiCatMeme MemeBananaCat = new("cat_happycat_banana", 1200609322573512795);
        public static EmojiCatMeme MemeHappyCat = new("cat_happycat_happy", 1200608921660960768);


        public static List<EmojiCatParty> AnimatedPartyEmojis = [];
        // Cat party emojis.
        public static EmojiCatParty CatCoke = new("cat_coke", 1200610043901509672);
        // Cat party dance emojis.
        public static EmojiCatParty CatParty1 = new("cat_party_1", 1200610219240198224);
        public static EmojiCatParty CatParty2 = new("cat_party_2", 1200608971145355327);
        public static EmojiCatParty CatParty3 = new("cat_party_3", 1200609989488816249);

    }
}
