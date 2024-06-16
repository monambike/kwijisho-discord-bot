using KWiJisho.Entities;

namespace KWiJisho.Data
{
    internal class Servers
    {
        public static Server Tramontina = new()
        {
            GuildId = 692588978959941653,
            WelcomeChannelId = 842222447410544650,
            NewsChannelId = 1203090940701446214
        };

        public static Server Personal = new()
        {
            GuildId = 737541664318554143,
            WelcomeChannelId = 737541664775602269
        };
    }
}
