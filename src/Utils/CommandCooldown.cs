using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KWiJisho.Utils
{
    /// <summary>
    /// A utility class that manages cooldown for executing commands and methods.
    /// </summary>
    /// <param name="maxUses">The max uses allowed before the wait of time to cooldown reset.</param>
    /// <param name="resetAfter">The time to reset, when it reached the end allow user to execute the command again.</param>
    /// <param name="cooldownPurpose">The purpose of this cooldown for user explanation.</param>
    internal class CommandCooldown(int maxUses, TimeSpan resetAfter, string cooldownPurpose)
    {
        /// <summary>
        /// Max uses allowed before the wait of time to cooldown reset.
        /// </summary>
        internal int MaxUses { get; init; } = maxUses;

        /// <summary>
        /// Time to reset and allow new command uses.
        /// </summary>
        internal TimeSpan TimeToReset { get; init; } = resetAfter;

        /// <summary>
        /// A brief explanation of the purpose of the cooldown.
        /// </summary>
        internal string CooldownPurpose { get; init; } = cooldownPurpose;

        /// <summary>
        /// The requests made for this cooldown instance.
        /// </summary>
        internal List<DateTime> Requests { get; set; } = [];

        /// <summary>
        /// Checks if user can execute the action or if is necessary to wait.
        /// </summary>
        /// <param name="discordChannel">The channel where the message will be sent.</param>
        /// <returns>A <see cref="bool"/> saying if the requested action could be executed or not.</returns>
        internal bool CanExecute(DiscordChannel discordChannel)
        {
            // Getting the difference time from the first request and current time.
            var waitFromFirstRequest = DateTime.UtcNow - Requests.FirstOrDefault();

            // If still in the request wait enought for the reset, clear the requests
            // log for this cooldown instance.
            if (Requests.Count != 0 && waitFromFirstRequest > TimeToReset) Requests.Clear();

            // Getting if user can executed the command based on max uses at the current cooldown time span.
            bool canExecute = Requests.Count < MaxUses;

            // If user can execute the command, add it to the cooldown.
            if (canExecute) Requests.Add(DateTime.UtcNow);
            // If not send a message to him.
            else
            {
                // Getting the remaining time that user must to wait.
                TimeSpan remainingTime = TimeToReset - waitFromFirstRequest;

                // Building the message that tells how much time is remaining.
                var remainingTimeMessage = GetTimeRemainingString(remainingTime);

                // Building the header of the cooldown message.
                var cooldownMessage = KWiJishoPermission.CooldownCustomErrorMessage(MaxUses, TimeToReset.Minutes, "mudar o tema do servidor");

                // Sends the message saying that the code won't be executed to its cooldown.
                discordChannel.SendMessageAsync($"Lamento amiguinho eu preciso de um tempo.. (╥﹏╥) {Environment.NewLine + cooldownMessage + " " + remainingTimeMessage}");
            }

            // Returning if the user can execute the command due to cooldown or not.
            return canExecute;
        }

        /// <summary>
        /// Gets a string representation of the provided time remaining.
        /// </summary>
        /// <param name="timeSpan">The time remaining to get the representation.</param>
        /// <returns>The <see cref="string"/> representation of how much time is remaining.</returns>
        internal static string GetTimeRemainingString(TimeSpan timeSpan)
        {
            // Getting the time remaining strings
            var timeRemainingStrings = GetTimeRemainingStrings(timeSpan);

            // Initializing variable that will hold the string to be returned
            var timeRemainingString = "Você ainda precisa esperar ";

            for (int index = 0; index < timeRemainingStrings.Count(); index++)
            {
                // Setting the current string index
                var currentStringIndex = index + 1;

                // Appends a "," or " e " depending of the current string index in the list count
                if (currentStringIndex > 1) timeRemainingString += (currentStringIndex < timeRemainingStrings.Count()) ? ", " : " e ";

                // Place the string at the current string index
                timeRemainingString += timeRemainingStrings.ElementAt(index);
            }
            // Returning the content with a final dot
            return timeRemainingString += ".";
        }

        /// <summary>
        /// Gets a list containing string representations of the provided time remaining.
        /// </summary>
        /// <param name="timeSpan">The time remaining to get the strings accordingly.</param>
        /// <returns>The <see cref="IEnumerable{T}"/> containing a list of <see cref="string"/> representing how
        /// much time is remaining for each <see cref="TimeSpan"/> property.</returns>
        internal static IEnumerable<string> GetTimeRemainingStrings(TimeSpan timeSpan)
        {
            // Getting string for minutes (if avaiable).
            if (timeSpan.Minutes != 0) yield return $"{timeSpan.Minutes} {(timeSpan.Minutes == 1 ? "minutinho" : "minutinhos")}";

            // Getting string for seconds (if avaiable).
            if (timeSpan.Seconds != 0) yield return $"{timeSpan.Seconds} {(timeSpan.Seconds == 1 ? "segundinho" : "segundinhos")}";
        }
    }
}
