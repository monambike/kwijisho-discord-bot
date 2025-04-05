﻿// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus.Entities;
using KWiJisho.APIs;
using KWiJisho.Data;
using KWiJisho.Entities;
using KWiJisho.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace KWiJisho.Models
{
    /// <summary>
    /// Class that provides a set of utilities for birthday tasks.
    /// </summary>
    public static class Birthday
    {
        /// <summary>
        /// Gets a list of users ordered by how closer are their birthday.
        /// </summary>
        /// <returns>A <see cref="List{T}"/> containing a group of <see cref="User"/> and their <see cref="User.Birthday"/> ordered by their birthday date.</returns>
        public static List<User> GetBirthdayList()
        {
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
        public static User? GetNextUserToMakeBirthday(DiscordGuild discordGuild)
        {
            // Retrieving users by their closest birthday.
            var users = GetBirthdayList();

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
            // If not possible to return any user, returns null
            return null;
        }

        /// <summary>
        /// Method responsible for generating a birthday message.
        /// </summary>
        /// <param name="user">The user that to get the generated birthday message.</param>
        /// <returns>A <see cref="string"/> containing the generated birthday message.</returns>
        /// <exception cref="NotImplementedException">Thrown if the upcoming birthday date is not yet implemented on this
        /// current method.</exception>
        public static async Task<string> GenerateBirthdayMessageAsync(User user)
        {
            // Getting days remaining for registered user birthday.
            var daysRemaining = GetBirthdayDaysRemaining(user);
            var upcomingBirthdayDate = GetBirthdayUpcomingDate(daysRemaining);
            return upcomingBirthdayDate switch
            {
                BirthdayUpcomingDate.Today => await GenerateTodayHappyBirthdayMessageAsync(user),
                BirthdayUpcomingDate.Tomorrow => GenerateTomorrowBirthdayReminder(user),
                BirthdayUpcomingDate.InSomeDays => GenerateInSomeDaysBirthdayReminder(user, daysRemaining),
                BirthdayUpcomingDate.Unspecified  => await GenerateUnspecifiedHappyBirthdayMessageAsync(user),
                _ => throw new NotImplementedException()
            };
        }

        internal static async Task<string> GenerateUnspecifiedHappyBirthdayMessageAsync(User user)
        {
            var chatGptMessage = await ChatGPT.GetKWiJishoPromptAsync($"dê uma mensagem de aniversário especial para {user.Nickname} sem assumir que é hoje");
            var bodyBirthdayMessage = (chatGptMessage is null) ? "" : Environment.NewLine + Environment.NewLine + chatGptMessage;

            var birthdayMessage =
                $"Não sei dizer se o seu aniversário é hoje por alguns problemas nos meus fiozinhos 😵‍💫 mas... QUEREMOS DESEJAR MESMO ASSIM!! " +
                $"Feliz aniversário!! 🥳🎉 {"PARABÉNSS!!!!".ToDiscordBold()} " +
                $"{bodyBirthdayMessage}" +
                $"Feliz Aniversário {user.NicknameVariation} 🎂❤️" +
                $"{Environment.NewLine + Environment.NewLine}" +
                $"Gente vem cá! <@&{Servers.Tramontina.BirthdayRoleId}>, lembraram de desejar feliz aniversário para {user.FirstName}?";

            return birthdayMessage;
        }

        internal static async Task<string> GenerateTodayHappyBirthdayMessageAsync(User user)
        {
            var chatGptMessage = await ChatGPT.GetKWiJishoPromptAsync($"dê uma mensagem de aniversário especial para {user.Nickname}");
            var bodyBirthdayMessage = (chatGptMessage is null) ? "" : Environment.NewLine + Environment.NewLine + chatGptMessage;

            var birthdayMessage =
                $"Hoje é seu aniversário!! 🥳🎉 {"PARABÉNSS!!!!".ToDiscordBold()} " +
                $"Feliz Aniversário {user.NicknameVariation} 🎂❤️" +
                $"{bodyBirthdayMessage}" +
                $"{Environment.NewLine + Environment.NewLine}" +
                $"Gente vem cá! <@&{Servers.Tramontina.BirthdayRoleId}>, {user.FirstName} fez aniversário hoje!";

            return birthdayMessage;
        }

        private static string GenerateTomorrowBirthdayReminder(User user)
            => $"{"Amanhã".ToDiscordBold()} já é o seu aniversário {user.Nickname}! 🥳🎉 Mal posso esperar!!";

        private static string GenerateInSomeDaysBirthdayReminder(User user, int daysRemaining)
            => $"Faltam apenas {(daysRemaining + " dias").ToDiscordBold()} pra {user.Nickname} fazer aniversário!! 👀 Tô ansiosa!!";


        /// <summary>
        /// Get how many days are remaning for the user's birthday.
        /// </summary>
        /// <param name="discordUser">The user to get how many years are remaining for they
        /// birthday.</param>
        /// <returns>A <see cref="double"/> containing the number of days remaining for they birthday.</returns>
        public static int GetBirthdayDaysRemaining(User discordUser)
            => (discordUser.Birthday.Date - DateTime.Now.Date).Days;

        /// <summary>
        /// Gets the <see cref="BirthdayUpcomingDate"/> based on how much days are remaining for it.
        /// </summary>
        /// <param name="daysRemaning">How many days are remaining for the birthday.</param>
        /// <returns>The <see cref="BirthdayUpcomingDate"/> based on how many days are remaning.</returns>
        /// <exception cref="NotImplementedException">Thrown if the upcoming birthday date is not yet implemented
        /// on this current method.</exception>
        public static BirthdayUpcomingDate GetBirthdayUpcomingDate(double daysRemaning)
        {
            return daysRemaning switch
            {
                0 => BirthdayUpcomingDate.Today,
                1 => BirthdayUpcomingDate.Tomorrow,
                > 1 => BirthdayUpcomingDate.InSomeDays,
                _ => throw new NotImplementedException()
            };
        }

        /// <summary>
        /// Represents options for categorizing upcoming birthday dates.
        /// </summary>
        public enum BirthdayUpcomingDate
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
            InSomeDays,

            /// <summary>
            /// Indicates a birthday with an unknown or unspecified date, but that stills deserves a happy birthday.
            /// </summary>
            Unspecified
        }
    }
}
