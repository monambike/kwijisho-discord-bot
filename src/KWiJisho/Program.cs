using KWiJisho.Modules;

namespace KWiJisho
{
    /// <summary>
    /// The main entry point class of the application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The main entry point method of the application.
        /// </summary>
        internal static void Main()
        {
            // Creating a new instance of the Discord bot class
            var bot = new Bot();

            // Initializing the Discord bot application and waiting for the inifinite task
            // result on the awaiter.
            bot.RunAsync().GetAwaiter().GetResult();
        }
    }
}
