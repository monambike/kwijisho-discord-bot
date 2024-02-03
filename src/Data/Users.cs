using KWiJisho.Entities;
using System;
using System.Collections.Generic;

namespace KWiJisho.Data
{
    /// <summary>
    /// Provides a static class for managing Discord users along with their identifiers.
    /// </summary>
    internal static class DiscordUsers
    {
        /// <summary>
        /// The list of users along with their identifiers.
        /// </summary>
        internal static List<User> Users =>
        [
            new(748963722088677376, new DateTime(2002, 06, 14), "Dark"), // eita_tami
            new(256137979335016448, new DateTime(2001, 08, 15), "Orlando"), // fellippeo
            new(221419309333741569, new DateTime(2001, 03, 30), "Biel"), // gabstend
            new(301152393821814794, new DateTime(2001, 05, 09), "Luiz"), // galo_lpc
            new(421101332326383618, new DateTime(2004, 09, 05), "Lívia"), // haruna1686
            new(331920695359569922, new DateTime(2000, 11, 08), "Posca"), // p0sc4t
            new(207556639719555072, new DateTime(2002, 11, 24), "Vini"), // monambike
            new(737573340851470348, new DateTime(2003, 03, 21), "Betty"), // darksidevision
            new(737535848102363259, KWiJishoInfo.Created.Date, "KWiJisho") // KWiJisho Bot
        ];
    }
}
