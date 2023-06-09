namespace KWIJisho
{
    internal class Program
    {
        internal static void Main()
        {
            var bot = new Bot();

            bot.RunAsync().GetAwaiter().GetResult();
        }
    }
}
