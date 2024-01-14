using KWIJisho.Models;
using System.IO;
using System;

namespace KWIJisho
{
    internal class Program
    {
        internal static void Main()
        {
            // Sets the default directory for being the "src/KWIJisho" folder
            Directory.SetCurrentDirectory("../..");

            // Initializing the bot application
            var bot = new Bot();
            bot.RunAsync().GetAwaiter().GetResult();
        }
    }
}
