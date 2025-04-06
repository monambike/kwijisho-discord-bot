// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus.Entities;
using KWiJisho.Data;
using KWiJisho.Entities;
using KWiJisho.Scheduling;
using KWiJisho.Utils;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KWiJisho.Models.Birthday;

namespace KWiJisho.Commands
{
    /// <summary>
    /// Provides methods for birthday prefix and slash commands.
    /// </summary>
    public static class CommandBirthday
    {
        /// <summary>
        /// Represents the year months in Portuguese.
        /// </summary>
        private static Dictionary<int, string> Months => new()
        {
            { 01, "Janeiro" },
            { 02, "Fevereiro" },
            { 03, "Março" },
            { 04, "Abril" },
            { 05, "Maio" },
            { 06, "Junho" },
            { 07, "Julho" },
            { 08, "Agosto" },
            { 09, "Setembro" },
            { 10, "Outubro" },
            { 11, "Novembro" },
            { 12, "Dezembro" }
        };

        /// <summary>
        /// Sends a birthday message for the next upcoming birthday in the specified Discord channel.
        /// </summary>
        /// <param name="discordChannel">The Discord channel where the message will be sent.</param>
        /// <param name="discordGuild">The Discord guild containing the users.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task CommandNextBirthdayAsync(DiscordChannel discordChannel, DiscordGuild discordGuild)
            => await NextBirthdayMessageAsync(discordChannel, discordGuild);

        /// <summary>
        /// Sends a birthday message a specified Discord user no matter if it is their birthday today.
        /// </summary>
        /// <param name="discordChannel">The Discord channel where the message will be sent.</param>
        /// <param name="discordGuild">The Discord guild containing the users.</param>
        /// <param name="discordGuild">The Discord user to receive the birthday message.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task CommandHappyBirthdayAsync(DiscordChannel discordChannel, DiscordGuild discordGuild, DiscordUser discordUser)
            => await HappyBirthdayMessageAsync(discordChannel, discordGuild, discordUser);

        /// <summary>
        /// Sends a list of upcoming birthdays in the specified Discord channel.
        /// </summary>
        /// <param name="discordChannel">The Discord channel where the message will be sent.</param>
        /// <param name="discordGuild">The Discord guild containing the users.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task CommandBirthdayListAsync(DiscordChannel discordChannel, DiscordGuild discordGuild)
        {
            // Getting list of users and their birthday
            var birthdayList = GetBirthdayList();

            // Initializing discord embed builder
            var discordEmbedBuilder = new DiscordEmbedBuilder
            {
                Color = Config.ConfigJson.DefaultColor.DiscordColor,
                Title = "LISTA DE ANIVERSARIANTES",
            };

            // Adding a field for every month that someone makes birthday.
            foreach (var month in Months)
            {
                // Getting users that make birthday at the current month
                var birthdayListThisMonth = birthdayList.Where(user => user.Birthday.Month == month.Key);

                // Building a string that will hold all users that will make birthday at current month.
                var usersBirthdayThisMonthBuilder = new StringBuilder();
                foreach (var user in birthdayListThisMonth)
                {
                    // Assigning discord member information
                    await user.AssignGuildToUserAsync(discordGuild);

                    if (user.DiscordMember is not null)
                    {
                        // Formatting member name
                        var nickname = user.DiscordMember.DisplayName == user.DiscordMember.Username ? user.DiscordMember.DisplayName : $"{user.DiscordMember.DisplayName}, {user.DiscordMember.Username}";

                        // Formatting user field with name and birthday month and day.
                        usersBirthdayThisMonthBuilder.AppendLine($"{user.Birthday:dd/MM} - {user.FirstName} ({nickname})");
                    }
                }

                // If wasn't found no users or there's no birthday users this month, go to the next month.
                if (string.IsNullOrEmpty(usersBirthdayThisMonthBuilder.ToString())) continue;

                // Adding field with all birthday users of this month.
                discordEmbedBuilder.AddField($"Aniversariantes de {month.Value.ToUpper()}", usersBirthdayThisMonthBuilder.ToString());
            }

            // Sending the builded embed message
            await discordChannel.SendMessageAsync(discordEmbedBuilder);
        }

        /// <summary>
        /// Sends a birthday message for a specific user if today is their birthday or it's upcoming.
        /// </summary>
        /// <param name="discordChannel">The channel where the message will be sent.</param>
        /// <param name="discordGuild">The guild where the user is located.</param>
        /// <param name="discordUser">The Discord user to check and celebrate.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task HappyBirthdayMessageAsync(DiscordChannel discordChannel, DiscordGuild discordGuild, DiscordUser discordUser)
        {
            var user = DiscordUsers.Users.FirstOrDefault(user => user.Id == discordUser.Id);
            if (!await IsValidBirthdayUserAsync(user, discordGuild, discordChannel)) return;

            // Gets how many days are maining for the user's birthday.
            var daysRemaining = GetBirthdayDaysRemaining(user);
            var upcomingDate = GetBirthdayUpcomingDate(daysRemaining);

            var message = string.Empty;

            message = upcomingDate is BirthdayUpcomingDate.Today
                ? await GenerateTodayHappyBirthdayMessageAsync(user)
                : await GenerateUnspecifiedHappyBirthdayMessageAsync(user);

            await BuildBirthdayMessage(discordChannel, user, upcomingDate, message);
        }

        /// <summary>
        /// Checks if a user is valid and exists within the current server context.
        /// </summary>
        /// <param name="user">The user object to validate.</param>
        /// <param name="discordGuild">The guild where the user should be a member.</param>
        /// <param name="discordChannel">The channel to send error messages, if needed.</param>
        /// <returns>True if the user is valid and in the server; false otherwise.</returns>
        public static async Task<bool>IsValidBirthdayUserAsync(User? user, DiscordGuild discordGuild, DiscordChannel discordChannel)
        {
            // If the user is null, send a message to the Discord channel indicating inability to find the next user in the birthday list.
            if (user is null)
            {
                await discordChannel.SendMessageAsync($"O usuário não está na lista de aniversariantes!");
                return false;
            }

            // Tries to get detailed discord info about the user found.
            await user.AssignGuildToUserAsync(discordGuild);

            // If the user is null, send a message to the Discord channel indicating inability to find the next user in the birthday list.
            if (user.DiscordMember is null)
            {
                await discordChannel.SendMessageAsync($"O usuário não está nesse servidor!");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Sends a message about the next upcoming birthday in the server.
        /// </summary>
        /// <param name="discordChannel">The channel where the message will be sent.</param>
        /// <param name="discordGuild">The guild to search users from.</param>
        /// <param name="sendOnlyIfTodayBirthday">If true, sends the message only if the birthday is today.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task NextBirthdayMessageAsync(DiscordChannel discordChannel, DiscordGuild discordGuild, bool sendOnlyIfTodayBirthday = false)
        {
            // Getting closest birthday from user present in the server and its member info
            var user = GetNextUserToMakeBirthday(discordGuild);
            if (!await IsValidBirthdayUserAsync(user, discordGuild, discordChannel)) return;

            // Gets how many days are maining for the user's birthday.
            var daysRemaining = GetBirthdayDaysRemaining(user);
            var upcomingDate = GetBirthdayUpcomingDate(daysRemaining);

            // If sending only if today is the birthday and it's not today, log and return.
            if (sendOnlyIfTodayBirthday && upcomingDate is not BirthdayUpcomingDate.Today)
            {
                await Logs.DefaultLog.AddInfoAsync(Log.Module.Birthday, BirthdayJob.LogContext, $@"Birthday message wasn't sent. There's not birthday today.");
                return;
            }

            await GenerateBirthdayMessage(discordChannel, user, upcomingDate);
        }

        /// <summary>
        /// Generates and sends a birthday message to a Discord channel.
        /// </summary>
        /// <param name="discordChannel">The channel where the message will be sent.</param>
        /// <param name="user">The user to celebrate.</param>
        /// <param name="upcomingDate">How close the birthday is (e.g., today, tomorrow).</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task GenerateBirthdayMessage(DiscordChannel discordChannel, User user, BirthdayUpcomingDate upcomingDate)
        {
            // Generates birthday message according how many days are remaining for its birthday.
            var message = await GenerateBirthdayMessageAsync(user);
            await BuildBirthdayMessage(discordChannel, user, upcomingDate, message);
        }

        /// <summary>
        /// Builds and sends the final birthday embed message, including image and details.
        /// </summary>
        /// <param name="discordChannel">The channel where the message will be sent.</param>
        /// <param name="user">The birthday user.</param>
        /// <param name="upcomingDate">The upcoming date type for the birthday.</param>
        /// <param name="message">The birthday message to display.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task BuildBirthdayMessage(DiscordChannel discordChannel, User user, BirthdayUpcomingDate upcomingDate, string message)
        {
            // Gets the image name and image's full path.
            var fileName = $"500x500-happybirthday-{upcomingDate.ToString().ToLower()}.png";
            var imagePath = Path.GetFullPath($"Resources/Images/Birthday/{fileName}");

            // Initializes discord embed builder message
            var discordEmbedBuilder = new DiscordEmbedBuilder
            {
                Color = Config.ConfigJson.DefaultColor.DiscordColor,
                Title = "ANIVERSARIANTE",
                Description = $@"O próximo aniversariante é.. {user.Nickname}!! {message}",
                Footer = new DiscordEmbedBuilder.EmbedFooter
                {
                    Text = $"Aniversariante: {user.FirstName} • Aniversário: {user.Birthday:dd/MM/yyyy} • Idade: {user.Age} anos"
                }
            }
            .WithThumbnail($"attachment://{fileName}")
            .WithImageUrl(user.DiscordMember?.AvatarUrl)
            .Build();

            using var fileStream = new FileStream(imagePath, FileMode.Open);
            // Attachs file and embed to the message and sending it to the discord channel.
            await discordChannel.SendMessageAsync(new DiscordMessageBuilder()
                .AddEmbed(discordEmbedBuilder)
                // Adds the image gif of karen kujou happy talking.
                .AddFile(fileName, fileStream));
        }
    }
}
