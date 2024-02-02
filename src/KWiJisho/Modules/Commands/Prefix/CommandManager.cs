using DSharpPlus;
using KWiJisho.Modules.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KWiJisho.Modules.Commands.Prefix
{
    /// <summary>
    /// Provides a set of commands and classes for managing commands in a Discord server.
    /// </summary>
    internal static partial class PrefixCommandManager
    {
        /// <summary>
        /// Gets or sets the list of command groups.
        /// </summary>
        internal static List<PrefixCommandGroup> CommandGroups { get; set; } = [];

        /// <summary>
        /// Gets the command group for Astronomy commands.
        /// </summary>
        internal static PrefixCommandGroup Astronomy => new("Nasa e Astronomia");

        /// <summary>
        /// Gets the command group for ChatGPT commands.
        /// </summary>
        internal static PrefixCommandGroup ChatGpt => new("ChatGpt (Estilo KWiJisho 🌟)");

        /// <summary>
        /// Gets the command group for Information and Help commands.
        /// </summary>
        internal static PrefixCommandGroup Info => new("Informações Adicionais");

        /// <summary>
        /// Gets the command group for Theme commands.
        /// </summary>
        internal static PrefixCommandGroup Theme => new("Mudar Tema do Servidor");

        /// <summary>
        /// Gets the command group for Birthday commands.
        /// </summary>
        internal static PrefixCommandGroup Birthday => new("Aniversário");

        /// <summary>
        /// Gets the command group for Basic commands.
        /// </summary>
        internal static PrefixCommandGroup Basic => new("Comandos Básicos");
    }

    /// <summary>
    /// Represents a single Discord command.
    /// </summary>
    internal class PrefixCommand
    {
        /// <summary>
        /// Gets or sets the name of the command.
        /// </summary>
        internal string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the commands.
        /// </summary>
        internal string Description { get; set; }

        /// <summary>
        /// Gets or sets the required permissions for the command.
        /// </summary>
        internal Permissions Permissions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrefixCommand"/> class.
        /// </summary>
        /// <param name="name">The name of the command.</param>
        /// <param name="description">The description of the command.</param>
        /// <param name="group">The command group to which this command belongs.</param>
        /// <param name="permission">The required permissions for the command.</param>
        internal PrefixCommand(string name, string description, PrefixCommandGroup group, Permissions permission = Permissions.None)
        {
            // Setting values from parameters into the properties
            (Name, Description, Permissions) = (name, description, permission);

            // If found the command group of this command, then add current prefix to the list
            PrefixCommandManager.CommandGroups.FirstOrDefault(commandGroup => commandGroup.Name == group.Name)?.Commands.Add(this);
            if (permission != Permissions.None)
                Description = $"{description}{Environment.NewLine}" + $@"[Esse comando só pode ser executado por pessoas com a permissão de ""{KWiJishoPermission.GetPermissionNameInPortuguese(permission)}""]".ToDiscordItalic(); ;
        }
    }

    /// <summary>
    /// Represents a group and holds a list with a set of Discord commands.
    /// </summary>
    internal partial class PrefixCommandGroup
    {
        /// <summary>
        /// Gets or sets the name of the prefix command group.
        /// </summary>
        internal string Name { get; set; }

        /// <summary>
        /// Gets or sets the list of commands in the prefix command group.
        /// </summary>
        internal List<PrefixCommand> Commands { get; set; } = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="PrefixCommandGroup"/> class.
        /// </summary>
        /// <param name="name">The name of the command group.</param>
        internal PrefixCommandGroup(string name)
        {
            // Setting the property values
            Name = name;

            // If found a command group with the same name, add it into the list
            if (!PrefixCommandManager.CommandGroups.Any(commandGroup => commandGroup.Name == name))
                PrefixCommandManager.CommandGroups.Add(this);
        }

        /// <inheritdoc/>
        public override string ToString() => Name;
    }
}
