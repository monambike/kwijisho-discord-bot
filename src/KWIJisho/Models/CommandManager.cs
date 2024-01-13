using System.Collections.Generic;
using System.Linq;

namespace KWIJisho
{
    /// <summary>
    /// Represent the class that manage commands.
    /// </summary>
    internal static partial class CommandManager
    {
        internal static List<CommandGroup> CommandGroups { get; set; } = new List<CommandGroup>();

        internal static CommandGroup InfoGroup { get { return new CommandGroup("Informational"); } }
        internal static CommandGroup DictionaryGroup { get { return new CommandGroup("Dictionary"); } }
        internal static CommandGroup ThemeGroup { get { return new CommandGroup("Theme"); } }
    }

    /// <summary>
    /// Represents a single Discord command.
    /// </summary>
    internal class Command
    {
        internal string Name { get; set; }

        internal string Description { get; set; }

        internal Command(string name, string description, CommandGroup group)
        {
            Name = name;
            Description = description;

            var selectedCommandGroup = CommandManager.CommandGroups.Where(commandGroup => commandGroup.Name == group.Name).FirstOrDefault();
            selectedCommandGroup.Commands.Add(this);
        }
    }

    /// <summary>
    /// Represents a group and holds a list with a set of Discord commands.
    /// </summary>
    internal partial class CommandGroup
    {
        internal string Name { get; set; }

        internal List<Command> Commands { get; set; } = new List<Command>();

        internal CommandGroup(string name)
        {
            Name = name;
            if (!CommandManager.CommandGroups.Any(commandGroup => commandGroup.Name == name))
                CommandManager.CommandGroups.Add(this);
        }

        public override string ToString() => Name;
    }
}
