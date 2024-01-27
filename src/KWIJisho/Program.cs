using KWiJisho.Models;

namespace KWiJisho
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
