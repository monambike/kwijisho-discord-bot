using DSharpPlus.Entities;
using KWiJisho.Config;
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
    internal static class Birthday
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
            // Getting closest birthday from user present in the server and its member info
            var user = Models.Birthday.GetNextUserToMakeBirthday(discordGuild);
            var discordMember = user.GetUserDiscordMember(discordGuild);

            // Getting how many days are maining for the user's birthday
            var daysRemaining = Models.Birthday.GetBirthdayDaysRemaining(user);
            var upcomingDate = Models.Birthday.GetBirthdayUpcomingDate(daysRemaining);

            // Generating birthday message according how many days are remaining for its birthday
            var message = Models.Birthday.GenerateBirthdayMessage(user);

            // Getting image name and image's full path.
            var fileName = $"500x500-happybirthday-{upcomingDate.ToString().ToLower()}.png";
            var imagePath = Path.GetFullPath($"Resources/Images/Birthday/{fileName}");

            // Initinializing discord embed builder message
            var discordEmbedBuilder = new DiscordEmbedBuilder
            {
                Color = ConfigJson.DefaultColor.DiscordColor,
                Title = "PRÓXIMO ANIVERSARIANTE",
                Description = $@"O próximo aniversariante é.. {discordMember.Username}!! {message}"
            }
            .WithImageUrl($"attachment://{imagePath}").Build();

            using var fileStream = new FileStream(imagePath, FileMode.Open);
            // Attaching file and embed to the message and sending it to the discord channel
            await discordChannel.SendMessageAsync(new DiscordMessageBuilder()
                .AddEmbed(discordEmbedBuilder)
                // The image gif of karen kujou happy talking.
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

            // Getting image name and image's full path.
            var fileName = $"500x281-talking.gif";
            var imagePath = Path.GetFullPath($"Resources/Images/{fileName}");

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
    }
}
