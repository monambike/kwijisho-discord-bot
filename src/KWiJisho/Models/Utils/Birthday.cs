using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KWiJisho.Models.Utils
{
    internal static class Birthday
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

        internal static List<User> GetUsersByClosestBirthday()
        {
            // Get today's date
            //var dateTimeNow = DateTime.Now.Date;
            var dateTimeNow = new DateTime(2024, 09, 04);
            //var dateTimeNow = new DateTime(2024, 09, 05);
            //var dateTimeNow = new DateTime(2024, 09, 06);
            foreach (var user in Users)
            {
                // Current month and day already passed
                var currentMonthDayPassed = user.Born.Date.Month == dateTimeNow.Date.Month
                    && user.Born.Date.Day < dateTimeNow.Date.Day;
                // Birthday month already passed
                var monthPassed = user.Born.Date.Month < dateTimeNow.Date.Month;

                // If user birthday occurs to be before today's day, and it's before or in the current month,
                // it means it already passed and will happen next year
                var yearToHappenBirthday = currentMonthDayPassed || monthPassed
                    ? dateTimeNow.Year + 1 : dateTimeNow.Year;

                // Setting birthday date
                user.Birthday = new DateTime(yearToHappenBirthday, user.Born.Date.Month, user.Born.Date.Day);
            }

            var result = Users.OrderBy(user => user.Birthday.Date).ToList();
            return result;
        }

        internal static User GetUserByClosestBirthday() => GetUsersByClosestBirthday().FirstOrDefault();

        internal static double GetDaysRemainingByClosestBirthday()
        {
            var user = GetUserByClosestBirthday();
            //var dateTimeNow = DateTime.Now.Date;
            //var dateTimeNow = new DateTime(2024, 09, 04);
            var dateTimeNow = new DateTime(2024, 09, 05);
            //var dateTimeNow = new DateTime(2024, 09, 06);
            return (user.Birthday.Date - dateTimeNow.Date).TotalDays;
        }

        internal enum UpcomingBirthdayDate { Today, Tomorrow, SomeDays }
        internal static UpcomingBirthdayDate GetUpcomingBirthdayDateByClosestBirthday()
        {
            var daysRemaning = GetDaysRemainingByClosestBirthday();

            return daysRemaning switch
            {
                 0 => UpcomingBirthdayDate.Today,
                 1 => UpcomingBirthdayDate.Tomorrow,
                 > 1 => UpcomingBirthdayDate.SomeDays,
                _ => throw new NotImplementedException()
            };
        }

        internal static string GenerateBirthdayMessage()
        {
            var daysRemaning = GetDaysRemainingByClosestBirthday();
            var upcomingBirthdayDate = GetUpcomingBirthdayDateByClosestBirthday();

            return upcomingBirthdayDate switch
            {
                UpcomingBirthdayDate.Today => $"Hoje é seu aniversário!! 🥳🎉 {"PARABÉNSS!!!!".ToDiscordBold()} Feliz Aniversário 🎂",
                UpcomingBirthdayDate.Tomorrow => $"{"Amanhá".ToDiscordBold()} já é o aniversário. 🥳🎉",
                UpcomingBirthdayDate.SomeDays => $"Faltam apenas {(daysRemaning + "dias").ToDiscordBold()} para o aniversário!! 👀 Tô ansiosa!!",
                _ => throw new NotImplementedException()
            };
        }

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
