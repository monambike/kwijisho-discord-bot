// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

namespace KWiJisho.Entities
{
    /// <summary>
    /// Represents a Discord server.
    /// </summary>
    public class Server
    {
        /// <summary>
        /// Server guild Id.
        /// </summary>
        public required ulong GuildId { get; init; }

        /// <summary>
        /// Server general channeld Id.
        /// </summary>
        public ulong GeneralChannelId { get; init; }

        /// <summary>
        /// Server welcome channeld Id.
        /// </summary>
        public ulong WelcomeChannelId { get; init; }

        /// <summary>
        /// Server news channel Id.
        /// </summary>
        public ulong NewsChannelId { get; init; }

        /// <summary>
        /// Server birthday role Id.
        /// </summary>
        public ulong BirthdayRoleId { get; init; }

        /// <summary>
        /// Server log channel Id.
        /// </summary>
        public ulong LogChannelId { get; init; }
    }
}
