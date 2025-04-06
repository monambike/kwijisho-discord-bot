// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

namespace KWiJisho.Utils
{
    /// <summary>
    /// Represents metadata for logging actions performed by users or background processes.
    /// </summary>
    public class LogContext
    {
        /// <summary>
        /// The context type of the log context.
        /// Examples: "Command" for user commands, "Job" for automated tasks.
        /// </summary>
        public string? ContextType { get; set; }

        /// <summary>
        /// The action from the log context.
        /// Examples: "!help", "/happy-birthday", "BirthdayCheck" (in case of birthday check triggered by the job)
        /// </summary>
        public string? Action { get; set; }

        /// <summary>
        /// The issuer ID of the log context.
        /// If the action was performed by a user, this should contain their user ID.
        /// </summary>
        public ulong IssuerId { get; set; }

        /// <summary>
        /// The guild ID of the log context.
        /// </summary>
        public ulong GuildId { get; set; }
    }
}
