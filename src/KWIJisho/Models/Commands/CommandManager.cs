using ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KWiJisho.Models.Commands
{
    /// <summary>
    /// Represent the class that manage commands.
    /// </summary>
    internal static partial class CommandManager
    {
        internal static List<CommandGroup> CommandGroups { get; set; } = [];

        internal static CommandGroup AstronomyGroup { get { return new CommandGroup("Nasa e Astronomia"); } }
        internal static CommandGroup ChatGptGroup { get { return new CommandGroup("ChatGpt (Estilo KWiJisho 🌟)"); } }
        internal static CommandGroup InfoGroup { get { return new CommandGroup("Informações Adicionais"); } }
        internal static CommandGroup ThemeGroup { get { return new CommandGroup("Mudar Tema do Servidor"); } }
        internal static CommandGroup BirthdayGroup { get { return new CommandGroup("Aniversário"); } }
    }

    /// <summary>
    /// Represents a single Discord command.
    /// </summary>
    internal class Command
    {
        internal string Name { get; set; }

        internal string Description { get; set; }

        internal bool OwnerCommand { get; set; } = false;

        internal Command(string name, string description, CommandGroup group)
        {
            Name = name;
            Description = description;

            var selectedCommandGroup = CommandManager.CommandGroups.Where(commandGroup => commandGroup.Name == group.Name).FirstOrDefault();
            selectedCommandGroup.Commands.Add(this);
        }

        internal Command(string name, string description, CommandGroup group, bool ownerCommand)
            : this(name, $"{description}{Environment.NewLine}" + "Esse comando só pode ser executado pelo dono do bot".ToDiscordItalic(), group)
        {
            OwnerCommand = ownerCommand;
        }
    }

    /// <summary>
    /// Represents a group and holds a list with a set of Discord commands.
    /// </summary>
    internal partial class CommandGroup
    {
        internal string Name { get; set; }

        internal List<Command> Commands { get; set; } = [];

        internal CommandGroup(string name)
        {
            Name = name;
            if (!CommandManager.CommandGroups.Any(commandGroup => commandGroup.Name == name))
                CommandManager.CommandGroups.Add(this);
        }

        public override string ToString() => Name;
    }
}
