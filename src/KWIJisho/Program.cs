using KWIJisho.Models;
using System.IO;

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
