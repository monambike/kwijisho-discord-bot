using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;

namespace KWiJisho.Models.Utils
{
    internal static class DiscordUsers
    {

        internal static List<User> Users =
        [
            new(748963722088677376, new DateTime(2002, 06, 14)), // eita_tami
            new(256137979335016448, new DateTime(2001, 08, 15)), // fellippeo
            new(221419309333741569, new DateTime(2001, 03, 30)), // gabstend
            new(301152393821814794, new DateTime(2001, 05, 09)), // galo_lpc
            new(421101332326383618, new DateTime(2004, 09, 05)), // haruna1686
            new(331920695359569922, new DateTime(2000, 11, 08)), // p0sc4t
            new(207556639719555072, new DateTime(2002, 11, 24)), // monambike
            new(737573340851470348, new DateTime(2003, 03, 21)), // darksidevision
        ];
        internal class User(ulong id, DateTime born)
        {
            internal ulong Id { get; set; } = id;

            internal DateTime Born { get; set; } = born;

            internal DateTime Birthday { get; set; }

            /// <summary>
            /// Tries to return a user in the server. If not possible to return
            /// because the users wasn't found, return null.
            /// </summary>
            /// <param name="commandContext"></param>
            /// <returns>Returns <see cref="DiscordMember"/>. If not found, returns null.</returns>
            internal DiscordMember GetUserDiscordMember(CommandContext commandContext)
            {
                // Tries to return a user
                try { return commandContext.Guild.GetMemberAsync(Id).Result; }
                // If not possible because the user wasn't found, return null
                catch { return null; }
            }
        }
    }
}
