using DSharpPlus.Entities;
using KWiJisho.Data;
using KWiJisho.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KWiJisho.Utils
{
    /// <summary>
    /// Class that provides a set of utilities for birthday tasks.
    /// </summary>
    internal static class Birthday
    {
        /// <summary>
        /// Gets a list of users ordered by how closer are their birthday.
        /// </summary>
        /// <returns>A <see cref="List{T}"/> containing a group of <see cref="User"/> and their <see cref="User.Birthday"/>.</returns>
        internal static List<User> GetUsersOrderByClosestBirthday()
        {
            // Get today's date.
            var dateTimeNow = DateTime.Now.Date;

            // For each registered user in the list of users
            foreach (var user in DiscordUsers.Users)
            {
                // Check if its the current month and day already passed.
                var currentMonthDayPassed = user.Born.Date.Month == dateTimeNow.Date.Month && user.Born.Date.Day < dateTimeNow.Date.Day;

                // Check if birthday month already passed.
                var monthPassed = user.Born.Date.Month < dateTimeNow.Date.Month;

                // If user birthday occurs to be before today's day, and it's before or in the current month,
                // it means it already passed and will happen next year.
                var yearToHappenBirthday = currentMonthDayPassed || monthPassed
                    ? dateTimeNow.Year + 1 : dateTimeNow.Year;

                // Setting birthday date.
                user.Birthday = new DateTime(yearToHappenBirthday, user.Born.Date.Month, user.Born.Date.Day);
            }

            // Orders the list by users birthday date.
            var result = DiscordUsers.Users.OrderBy(user => user.Birthday.Date).ToList();

            // Returns the result.
            return result;
        }

        /// <summary>
        /// Tries to get the user if they are presents in current Discord server.
        /// </summary>
        /// <param name="discordGuild"></param>
        /// <returns>Returns the <see cref="User"/> if avaiable in current Discord server; otherwise, returns <see langword="null"/>.</returns>
        internal static User TryGetDiscordUserByClosestBirthday(DiscordGuild discordGuild)
        {
            // Retrieving users by their closest birthday.
            var users = GetUsersOrderByClosestBirthday();

            // For each user in the list of users.
            foreach (var user in users)
            {
                // Try to return the closest birthday person in the current discord server.
                try
                {
                    // Tries to get the user from current Discord server guild.
                    var tryGetUser = discordGuild.GetMemberAsync(user.Id).Result;

                    // If possible, return user instead of member.
                    return user;
                }
                // If not possible, goes to the next user.
                catch { continue; }
            }
            return null;
        }

        /// <summary>
        /// Get how many days are remaning for the user's birthday.
        /// </summary>
        /// <param name="discordUser">The user to get how many years are remaining for they
        /// birthday.</param>
        /// <returns>A <see cref="double"/> containing the number of days remaining for they birthday.</returns>
        internal static double GetDaysRemainingByUser(User discordUser)
            => (discordUser.Birthday.Date - DateTime.Now.Date).TotalDays;

        /// <summary>
        /// Represents options for categorizing upcoming birthday dates.
        /// </summary>
        internal enum UpcomingBirthdayDate
        {
            /// <summary>
            /// Indicates an upcoming birthday today.
            /// </summary>
            Today,

            /// <summary>
            /// Indicates an upcoming birthday tomorrow.
            /// </summary>
            Tomorrow,

            /// <summary>
            /// Indicates an upcoming birthday in the future, beyond tomorrow.
            /// </summary>
            InSomeDays
        }

        /// <summary>
        /// Gets the <see cref="UpcomingBirthdayDate"/> based on how much days are remaining for it.
        /// </summary>
        /// <param name="daysRemaning">How many days are remaining for the birthday.</param>
        /// <returns>The <see cref="UpcomingBirthdayDate"/> based on how many days are remaning.</returns>
        /// <exception cref="NotImplementedException">Thrown if the upcoming birthday date is not yet implemented
        /// on this current method.</exception>
        internal static UpcomingBirthdayDate GetUpcomingBirthdayDate(double daysRemaning)
        {
            return daysRemaning switch
            {
                0 => UpcomingBirthdayDate.Today,
                1 => UpcomingBirthdayDate.Tomorrow,
                > 1 => UpcomingBirthdayDate.InSomeDays,
                _ => throw new NotImplementedException()
            };
        }

        /// <summary>
        /// Method responsible for generating a birthday message.
        /// </summary>
        /// <param name="discordUser">The user that to get the generated birthday message.</param>
        /// <returns>A <see cref="string"/> containing the generated birthday message.</returns>
        /// <exception cref="NotImplementedException">Thrown if the upcoming birthday date is not yet implemented on this
        /// current method.</exception>
        internal static string GenerateBirthdayMessage(User discordUser)
        {
            // Getting days remaining for registered user birthday.
            var daysRemaning = GetDaysRemainingByUser(discordUser);
            var upcomingBirthdayDate = GetUpcomingBirthdayDate(daysRemaning);
            return upcomingBirthdayDate switch
            {
                UpcomingBirthdayDate.Today => $"Hoje é seu aniversário!! 🥳🎉 {"PARABÉNSS!!!!".ToDiscordBold()} Feliz Aniversário 🎂",
                UpcomingBirthdayDate.Tomorrow => $"{"Amanhá".ToDiscordBold()} já é o aniversário. 🥳🎉",
                UpcomingBirthdayDate.InSomeDays => $"Faltam apenas {(daysRemaning + " dias").ToDiscordBold()} para o aniversário!! 👀 Tô ansiosa!!",
                _ => throw new NotImplementedException()
            };
        }
    }
}
