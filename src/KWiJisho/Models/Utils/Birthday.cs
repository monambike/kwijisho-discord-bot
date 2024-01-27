using ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KWiJisho.Models.Utils
{
    internal static class Birthday
    {
        internal static List<User> Users =
        [
            new("eita_tami", new DateTime(2002, 06, 14)),
            new("fellippeo", new DateTime(2001, 08, 15)),
            new("gabstend", new DateTime(2001, 03, 30)),
            new("galo_lpc", new DateTime(2001, 05, 09)),
            new("haruna1686", new DateTime(2004, 09, 05)),
            new("p0sc4t", new DateTime(2000, 11, 08)),
            new("monambike", new DateTime(2002, 11, 24)),
            new("darksidevision", new DateTime(2003, 03, 21)),
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

        internal class User(string discordUsername, DateTime birthday)
        {
            public string Username { get; set; } = discordUsername;

            public DateTime Born { get; set; } = birthday;

            public DateTime Birthday { get; set; }
        }
    }
}
