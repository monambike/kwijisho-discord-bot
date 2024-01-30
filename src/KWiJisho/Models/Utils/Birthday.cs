using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KWiJisho.Models.Utils
{
    internal static class Birthday
    {
        internal static List<DiscordUsers.User> GetUsersByClosestBirthday()
        {
            // Get today's date
            var dateTimeNow = DateTime.Now.Date;
            foreach (var user in DiscordUsers.Users)
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

            var result = DiscordUsers.Users.OrderBy(user => user.Birthday.Date).ToList();
            return result;
        }

        internal static DiscordUsers.User GetDiscordUserByClosestBirthday(DiscordGuild discordGuild)
        {
            var users = GetUsersByClosestBirthday();
            foreach (var user in users)
            {
                // Try to return the closest birthday person in the current discord server
                try
                {
                    var tryGetUser = discordGuild.GetMemberAsync(user.Id).Result;
                    // If possible, return user instead of member
                    return user;
                }
                // If not possible, goes to the next user
                catch { continue; }
            }
            return null;
        }

        internal static double GetDaysRemainingByUser(DiscordUsers.User discordUser)
        {
            return (discordUser.Birthday.Date - DateTime.Now.Date).TotalDays;
        }

        internal enum UpcomingBirthdayDate { Today, Tomorrow, InSomeDays }
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

        internal static string GenerateBirthdayMessage(DiscordUsers.User discordUser)
        {
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
