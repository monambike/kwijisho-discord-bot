using KWiJisho.Modules;

namespace KWiJisho
{
    internal class Program
    {
        /// <summary>
        /// The main entry point of the application.
        /// </summary>
        internal static void Main()
        {
            // Initializing the Discord bot application and waiting for the inifinite task
            // result on the awaiter
            var bot = new Bot();
            bot.RunAsync().GetAwaiter().GetResult();
        }
    }
}
