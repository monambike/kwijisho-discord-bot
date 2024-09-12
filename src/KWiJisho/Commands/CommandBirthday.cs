// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus.Entities;
using KWiJisho.Entities;
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
        public static async Task CommandNextBirthdayAsync(DiscordChannel discordChannel, DiscordGuild discordGuild)
            => await SendBirthdayMessageAsync(discordChannel, discordGuild);

        /// <summary>
        /// Sends a list of upcoming birthdays in the specified Discord channel.
        /// </summary>
        /// <param name="discordChannel">The Discord channel where the message will be sent.</param>
        /// <param name="discordGuild">The Discord guild containing the users.</param>
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

        public static async Task SendBirthdayMessageAsync(DiscordChannel discordChannel, DiscordGuild discordGuild, bool sendOnlyIfTodayBirthday = false)
        {
            // Getting closest birthday from user present in the server and its member info
            var user = GetNextUserToMakeBirthday(discordGuild);

            // If the user is null, send a message to the Discord channel indicating inability to find the next user in the birthday list.
            if (user is null)
            {
                // Sends the message.
                await discordChannel.SendMessageAsync($"O usuário não está na lista de aniversariantes!");
                // Return to the method.
                return;
            }

            // Tries to get detailed discord info about the user found.
            await user.AssignGuildToUserAsync(discordGuild);

            // If the user is null, send a message to the Discord channel indicating inability to find the next user in the birthday list.
            if (user.DiscordMember is null)
            {
                // Sends the message that birthday user wasn't found.
                await discordChannel.SendMessageAsync($"O usuário não está nesse servidor!");
                // Return to the method.
                return;
            }

            // Gets how many days are maining for the user's birthday.
            var daysRemaining = GetBirthdayDaysRemaining(user);
            var upcomingDate = GetBirthdayUpcomingDate(daysRemaining);

            // If sending only if today is the birthday and it's not today, log and return.
            if (sendOnlyIfTodayBirthday && upcomingDate is not BirthdayUpcomingDate.Today)
            {
                await KWiJishoLogs.DefaultLog.AddInfoAsync(KWiJishoLog.Module.Birthday, $@"Birthday message wasn't sent. There's not birthday today.");
                return;
            }

            await GenerateBirthdayMessage(discordChannel, user, upcomingDate);
        }

        public static async Task GenerateBirthdayMessage(DiscordChannel discordChannel, User user, BirthdayUpcomingDate upcomingDate)
        {
            // Generates birthday message according how many days are remaining for its birthday.
            var message = await GenerateBirthdayMessageAsync(user);

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
                    Text = $"Aniversariante: {user.FirstName} • Aniversário: {user.Birthday:dd/MM/yyyy}"
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
