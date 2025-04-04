// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus.Entities;
using KWiJisho.Models;
using System;
using System.Threading.Tasks;

namespace KWiJisho.Entities
{
    /// <summary>
    /// Represents a Discord user along with their identifier and additional information.
    /// </summary>
    public class User(ulong id, DateTime born, string firstName, string nickname, string nicknameVariation)
    {
        /// <summary>
        /// The unique identifier of the user.
        /// </summary>
        public ulong Id => id;

        /// <summary>
        /// The date when the user was born.
        /// </summary>
        public DateTime Born => born;

        /// <summary>
        /// The user age.
        /// </summary>
        public int Age 
        {
            get
            {
                // Save today's date.
                var dateTimeNow = DateTime.Today;

                // Calculate the age.
                var age = dateTimeNow.Year - born.Year;

                // Go back to the year in which the person was born in case of a leap year
                if (born.Date > dateTimeNow.AddYears(-age)) age--;

                return age;
            }
        }

        /// <summary>
        /// The user real first name.
        /// </summary>
        public string FirstName => firstName;

        /// <summary>
        /// The custom identifier or nickname associated with the user.
        /// </summary>
        public string Nickname => nickname;
        /// <summary>
        /// The custom identifier or nickname associated with the user.
        /// </summary>
        public string NicknameVariation => nicknameVariation;

        public DiscordGuild? DiscordGuild { get; private set; }

        public DiscordMember? DiscordMember { get; private set; }

        /// <summary>
        /// Gets or sets the user's birthday (if applicable).
        /// </summary>
        public DateTime Birthday
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
        public async Task<DiscordMember?> TryGetDiscordMemberAsync(DiscordGuild discordGuild)
        {
            try
            {
                // Tries to return a user.
                return await discordGuild.GetMemberAsync(Id);
            }
            catch (DSharpPlus.Exceptions.NotFoundException)
            {
                // If not possible because the user wasn't found, return null.
                return null;
            }
        }

        /// <summary>
        /// Sets the user's associated Discord guild and Discord member.
        /// </summary>
        /// <param name="discordGuild">The Discord guild.</param>
        public async Task AssignGuildToUserAsync(DiscordGuild discordGuild)
        {
            DiscordMember = await TryGetDiscordMemberAsync(discordGuild);
            
            if (DiscordMember is not null)
                DiscordGuild = discordGuild;
        }

        /// <summary>
        /// Clears the user's associated Discord guild and member information.
        /// </summary>
        public void UnassignDiscordGuild()
        {
            DiscordGuild = null;
            DiscordMember = null;
        }
    }
}
