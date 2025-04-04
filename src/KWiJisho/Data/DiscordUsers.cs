﻿// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using KWiJisho.Entities;
using System;
using System.Collections.Generic;

namespace KWiJisho.Data
{
    /// <summary>
    /// Provides a static class for managing Discord users along with their identifiers.
    /// </summary>
    public static class DiscordUsers
    {
        /// <summary>
        /// The list of users along with their identifiers.
        /// </summary>
        public static List<User> Users =>
        [
            new(748963722088677376, new DateTime(2002, 06, 14), "Felipe", "Dark", "tamizinho"), // eita_tami
            new(256137979335016448, new DateTime(2001, 08, 15), "Fellippe", "Orlando", "orlandinho"), // fellippeo
            new(221419309333741569, new DateTime(2001, 03, 30), "Gabriel", "Biel", "bielzinho"), // gabstend
            new(301152393821814794, new DateTime(2001, 05, 09), "Luiz", "Luizão", "louiszon"), // galo_lpc
            new(421101332326383618, new DateTime(2004, 09, 05), "Lívia", "Haru", "haruzinha"), // haruna1686
            new(331920695359569922, new DateTime(2000, 11, 08), "Filipe", "Posca", "posquete"), // p0sc4t
            new(207556639719555072, new DateTime(2002, 11, 24), "Vinícius", "Vini", "vinicin"), // monambike
            new(737573340851470348, new DateTime(2003, 03, 21), "Elizabeth", "Betty", "bettyzinha"), // darksidevision
            new(352948739029336074, new DateTime(2002, 10, 29), "Matheus", "Mello", "mellinho"), // mello4906
            new(484119743339560971, new DateTime(2004, 07, 19), "Maria", "Mari", "freirezika"), // freirezoka
            new(737535848102363259, KWiJisho.Created.Date, "KWiJisho", "KWiJisho", "esquizobot") // KWiJisho Bot
        ];
    }
}
