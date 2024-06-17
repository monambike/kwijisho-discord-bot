using KWiJisho.Entities;

namespace KWiJisho.Data
{
    internal class Servers
    {
        public static Server Tramontina = new()
        {
            GuildId = 692588978959941653,
            GeneralChannelId = 692588978959941656,
            WelcomeChannelId = 842222447410544650,
            NewsChannelId = 1203090940701446214,
            BirthdayRoleId = 1252121891284582521
        };

        public static Server Personal = new()
        {
            GuildId = 737541664318554143,
            GeneralChannelId = 737541664775602269,
            WelcomeChannelId = 737541664775602269
        };
    }
}
