using DSharpPlus.Entities;
using KWiJisho.Config;
using KWiJisho.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KWiJisho.Commands
{
    /// <summary>
    /// Provides methods for birthday prefix and slash commands.
    /// </summary>
    internal static class CommandBirthday
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
        internal static async Task ExecuteNextBirthdayAsync(DiscordChannel discordChannel, DiscordGuild discordGuild)
        {
            await SendBirthdayMessage(discordChannel, discordGuild);
        }

        internal static async Task SendBirthdayMessage(DiscordChannel discordChannel, DiscordGuild discordGuild, bool sendOnlyIfTodayBirthday = false)
        {
            // Getting closest birthday from user present in the server and its member info
            var user = Models.Birthday.GetNextUserToMakeBirthday(discordGuild);

            // If the user is null, send a message to the Discord channel indicating inability to find the next user in the birthday list.
            if (user is null)
            {
                // Sends the message.
                await SendMessageBirthdayUserNotFound(discordChannel);
                // Return to the method.
                return;
            }

            // Tries to get detailed discord info about the user found.
            var discordMember = user.GetUserDiscordMember(discordGuild);

            // If the user is null, send a message to the Discord channel indicating inability to find the next user in the birthday list.
            if (discordMember is null)
            {
                // Sends the message that birthday user wasn't found.
                await SendMessageBirthdayUserNotFound(discordChannel);
                // Return to the method.
                return;
            }

            // Gets how many days are maining for the user's birthday.
            var daysRemaining = Models.Birthday.GetBirthdayDaysRemaining(user);
            var upcomingDate = Models.Birthday.GetBirthdayUpcomingDate(daysRemaining);

            // Return method if flagged to show only if user's birthday is today, and the birthday is not today.
            if (sendOnlyIfTodayBirthday && upcomingDate is not Models.Birthday.BirthdayUpcomingDate.Today)
            {
                await KWiJishoLogs.DefaultLog.AddInfoAsync(KWiJishoLog.Module.Birthday, $@"Birthday message wasn't sent because today is not birthday of ""{discordMember.Username}"".");
                return;
            }

            // Generates birthday message according how many days are remaining for its birthday.
            var message = await Models.Birthday.GenerateBirthdayMessage(user);

            // Gets the image name and image's full path.
            var fileName = $"500x500-happybirthday-{upcomingDate.ToString().ToLower()}.png";
            var imagePath = Path.GetFullPath($"Resources/Images/Birthday/{fileName}");

            // Initializes discord embed builder message
            var discordEmbedBuilder = new DiscordEmbedBuilder
            {
                Color = ConfigJson.DefaultColor.DiscordColor,
                Title = "ANIVERSARIANTE",
                Description = $@"O próximo aniversariante é.. {user.Nickname}!! {message}",
                Footer = new DiscordEmbedBuilder.EmbedFooter
                {
                    Text = $"Aniversariante: {user.FirstName} • Aniversário: {user.Birthday:dd/MM/yyyy}"
                }
            }
            .WithThumbnail($"attachment://{fileName}")
            .WithImageUrl(discordMember.AvatarUrl).Build();

            using var fileStream = new FileStream(imagePath, FileMode.Open);
            // Attachs file and embed to the message and sending it to the discord channel.
            await discordChannel.SendMessageAsync(new DiscordMessageBuilder()
                .AddEmbed(discordEmbedBuilder)
                // Adds the image gif of karen kujou happy talking.
                .AddFile(fileName, fileStream));
        }

        /// <summary>
        /// Sends a list of upcoming birthdays in the specified Discord channel.
        /// </summary>
        /// <param name="discordChannel">The Discord channel where the message will be sent.</param>
        /// <param name="discordGuild">The Discord guild containing the users.</param>
        internal static async Task ExecuteBirthdayListAsync(DiscordChannel discordChannel, DiscordGuild discordGuild)
        {
            // Getting list of users and their birthday
            var users = Models.Birthday.GetBirthdayList();

            // Initializing discord embed builder
            var discordEmbedBuilder = new DiscordEmbedBuilder
            {
                Color = ConfigJson.DefaultColor.DiscordColor,
                Title = "LISTA DE ANIVERSARIANTES",
            };

            // Adding a field for every month that someone makes birthday.
            foreach (var month in Months)
            {
                // Getting users that make birthday at the current month
                var usersBirthdayThisMonth = users.Where(user => user.Birthday.Month == month.Key);

                // Building a string that will hold all users that will make birthday at current month.
                var usersBirthdayThisMonthString = string.Empty;
                foreach (var userBirthdayThisMonth in usersBirthdayThisMonth)
                {
                    // Getting discord member information
                    var member = userBirthdayThisMonth.GetUserDiscordMember(discordGuild);
                    if (member is not null)
                    {
                        // Formatting member name
                        var name = member.DisplayName == member.Username ? member.DisplayName : $"{member.DisplayName} ({member.Username})";

                        // Formatting user field with name and birthday month and day.
                        usersBirthdayThisMonthString += $"{userBirthdayThisMonth.Birthday:dd/MM} - {name}{Environment.NewLine}";
                    }
                }

                // If wasn't found no users or there's no birthday users this month, go to the next month.
                if (string.IsNullOrEmpty(usersBirthdayThisMonthString)) continue;

                // Adding field with all birthday users of this month.
                discordEmbedBuilder.AddField($"Aniversariantes de {month.Value.ToUpper()}", usersBirthdayThisMonthString);
            }

            // Sending the builded embed message
            await discordChannel.SendMessageAsync(discordEmbedBuilder);
        }

        private static async Task SendMessageBirthdayUserNotFound(DiscordChannel discordChannel) =>
            await discordChannel.SendMessageAsync("Eu sinto muito.. :( eu não consegui encontrar nesse servidor o próximo usuário da lista a fazer aniversário.");
    }
}
