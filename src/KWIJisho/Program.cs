using KWIJisho.Models;

namespace KWIJisho
{
    internal class Program
    {
        internal static void Main()
        {
            // Initializing the bot application
            var bot = new Bot();
            bot.RunAsync().GetAwaiter().GetResult();
        }
    }
}
