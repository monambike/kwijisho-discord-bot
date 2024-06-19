// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus;
using KWiJisho.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KWiJisho.Commands.Prefix
{
    /// <summary>
    /// Provides a set of commands and classes for managing commands in a Discord server.
    /// </summary>
    public static partial class PrefixCommandManager
    {
        /// <summary>
        /// Gets or sets the list of command groups.
        /// </summary>
        public static List<PrefixCommandGroup> CommandGroups { get; set; } = [];

        /// <summary>
        /// Gets the command group for Astronomy commands.
        /// </summary>
        public static PrefixCommandGroup Astronomy => new("Nasa e Astronomia");

        /// <summary>
        /// Gets the command group for ChatGPT commands.
        /// </summary>
        public static PrefixCommandGroup ChatGpt => new("ChatGpt (Estilo KWiJisho 🌟)");

        /// <summary>
        /// Gets the command group for Information and Help commands.
        /// </summary>
        public static PrefixCommandGroup Info => new("Informações Adicionais");

        /// <summary>
        /// Gets the command group for Theme commands.
        /// </summary>
        public static PrefixCommandGroup Manage => new("Gerência do Servidor");

        /// <summary>
        /// Gets the command group for Birthday commands.
        /// </summary>
        public static PrefixCommandGroup Birthday => new("Aniversário");

        /// <summary>
        /// Gets the command group for Basic commands.
        /// </summary>
        public static PrefixCommandGroup Basic => new("Comandos Básicos");
    }

    /// <summary>
    /// Represents a single Discord command.
    /// </summary>
    public class PrefixCommand
    {
        /// <summary>
        /// Gets or initializes the name of the command.
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Gets or initializes the description of the commands.
        /// </summary>
        public string Description { get; init; }

        /// <summary>
        /// Gets or initializes the required permissions for the command.
        /// </summary>
        public Permissions Permissions { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrefixCommand"/> class.
        /// </summary>
        /// <param name="name">The name of the command.</param>
        /// <param name="description">The description of the command.</param>
        /// <param name="group">The command group to which this command belongs.</param>
        /// <param name="permission">The required permissions for the command.</param>
        public PrefixCommand(string name, string description, PrefixCommandGroup group, Permissions permission = Permissions.None)
        {
            // Setting values from parameters into the properties.
            (Name, Description, Permissions) = (name, description, permission);

            // If found the command group of this command, then add current prefix to the list.
            PrefixCommandManager.CommandGroups.FirstOrDefault(commandGroup => commandGroup.Name == group.Name)?.Commands.Add(this);
            if (permission != Permissions.None)
                Description = $"{description}{Environment.NewLine}" + $@"[Esse comando só pode ser executado por pessoas com a permissão de ""{KWiJishoPermission.GetPermissionNameInPortuguese(permission)}""]".ToDiscordItalic(); ;
        }
    }

    /// <summary>
    /// Represents a group and holds a list with a set of Discord commands.
    /// </summary>
    public partial class PrefixCommandGroup
    {
        /// <summary>
        /// Gets or sets the name of the prefix command group.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the list of commands in the prefix command group.
        /// </summary>
        public List<PrefixCommand> Commands { get; set; } = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="PrefixCommandGroup"/> class.
        /// </summary>
        /// <param name="name">The name of the command group.</param>
        public PrefixCommandGroup(string name)
        {
            // Setting the property values.
            Name = name;

            // If found a command group with the same name, add it into the list.
            if (!PrefixCommandManager.CommandGroups.Any(commandGroup => commandGroup.Name == name))
                PrefixCommandManager.CommandGroups.Add(this);
        }

        /// <inheritdoc/>
        public override string ToString() => Name;
    }
}
