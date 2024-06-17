using DSharpPlus.Entities;
using System;

namespace KWiJisho.Entities
{
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
        internal DateTime Birthday
        {
            get
            {
                // Get today's date.
                var dateTimeNow = DateTime.Now.Date;

                // Check if its the current month and day already passed.
                var currentMonthDayPassed = Born.Date.Month == dateTimeNow.Date.Month && Born.Date.Day < dateTimeNow.Date.Day;

                // Check if birthday month already passed.
                var monthPassed = Born.Date.Month < dateTimeNow.Date.Month;

                // If user birthday occurs to be before today's day, and it's before or in the current month,
                // it means it already passed and will happen next year.
                var yearToHappenBirthday = currentMonthDayPassed || monthPassed
                    ? dateTimeNow.Year + 1 : dateTimeNow.Year;

                // Setting birthday date.
                return new DateTime(yearToHappenBirthday, Born.Date.Month, Born.Date.Day);
            }
        }

        /// <summary>
        /// Tries to return a <see cref="DiscordMember"/> representing the user in the specified server.
        /// If the user is not found, returns <see langword="null"/>. 
        /// </summary>
        /// <param name="discordGuild">The Discord guild where the user is expected to be a member.</param>
        /// <returns>Returns <see cref="DiscordMember"/> if found; otherwise, returns <see langword="null"/>.</returns>
        internal DiscordMember? GetUserDiscordMember(DiscordGuild discordGuild)
        {
            // Tries to return a user.
            try { return discordGuild.GetMemberAsync(Id).Result; }
            // If not possible because the user wasn't found, return null.
            catch { return null; }
        }
    }
}
