namespace KWiJisho.Entities
{
    internal class Server()
    {
        /// <summary>
        /// Server guild Id.
        /// </summary>
        internal required ulong GuildId { get; init; }

        /// <summary>
        /// Server general channeld Id.
        /// </summary>
        internal ulong GeneralChannelId { get; init; }

        /// <summary>
        /// Server welcome channeld Id.
        /// </summary>
        internal ulong WelcomeChannelId { get; init; }

        /// <summary>
        /// Server news channel Id.
        /// </summary>
        internal ulong NewsChannelId { get; init; }

        /// <summary>
        /// Server birthday role Id.
        /// </summary>
        internal ulong BirthdayRoleId { get; init; }
    }
}
