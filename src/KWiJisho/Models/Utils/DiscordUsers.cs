using DSharpPlus.Entities;
using System;
using System.Collections.Generic;

namespace KWiJisho.Models.Utils
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

    /// <summary>
    /// Represents a Discord user along with their identifier and additional information.
    /// </summary>
    internal class User(ulong id, DateTime born, string identifier)
    {
        /// <summary>
        /// The unique identifier of the user.
        /// </summary>
        internal ulong Id => id;

        /// <summary>
        /// The date when the user was born.
        /// </summary>
        internal DateTime Born => born;

        /// <summary>
        /// The custom identifier or nickname associated with the user.
        /// </summary>
        internal string Identifier => identifier;

        /// <summary>
        /// Gets or sets the user's birthday (if applicable).
        /// </summary>
        internal DateTime Birthday { get; set; }

        /// <summary>
        /// Tries to return a <see cref="DiscordMember"/> representing the user in the specified server.
        /// If the user is not found, returns null.
        /// </summary>
        /// <param name="discordGuild">The Discord guild where the user is expected to be a member.</param>
        /// <returns>Returns <see cref="DiscordMember"/> if found; otherwise, returns null.</returns>
        internal DiscordMember GetUserDiscordMember(DiscordGuild discordGuild)
        {
            // Tries to return a user
            try { return discordGuild.GetMemberAsync(Id).Result; }
            // If not possible because the user wasn't found, return null
            catch { return null; }
        }
    }
}
