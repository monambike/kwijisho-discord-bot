using System.Collections.Generic;

namespace KWIJisho
{
    internal static partial class Commands
    {
        public static List<Command> DiscordCommands { get; set; } = new List<Command>();


        public class Command
        {
            public string Name { get; set; }
            public string Description { get; set; }

            public Command(string name, string description)
            {
                Name = name;
                Description = description;

                DiscordCommands.Add(this);
            }
        }
    }
}
