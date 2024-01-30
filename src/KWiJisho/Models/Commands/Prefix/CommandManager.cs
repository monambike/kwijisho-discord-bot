using DSharpPlus;
using KWiJisho.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KWiJisho.Models.Commands.Prefix
{
    /// <summary>
    /// Represent the class that represents commands groups and holds all Discord command classes.
    /// </summary>
    internal static partial class PrefixCommandManager
    {
        internal static List<PrefixCommandGroup> CommandGroups { get; set; } = [];

        internal static PrefixCommandGroup Astronomy => new("Nasa e Astronomia");
        internal static PrefixCommandGroup ChatGpt => new("ChatGpt (Estilo KWiJisho 🌟)");
        internal static PrefixCommandGroup Info => new("Informações Adicionais");
        internal static PrefixCommandGroup Theme => new("Mudar Tema do Servidor");
        internal static PrefixCommandGroup Birthday => new("Aniversário");
        internal static PrefixCommandGroup Basic => new("Comandos Básicos");
    }

    /// <summary>
    /// Represents a single Discord command.
    /// </summary>
    internal class PrefixCommand
    {
        internal string Name { get; set; }

        internal string Description { get; set; }

        internal Permissions Permissions { get; set; }

        internal PrefixCommand(string name, string description, PrefixCommandGroup group, Permissions permission = Permissions.None)
        {
            Name = name;
            Description = description;
            Permissions = permission;

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
        internal string Name { get; set; }

        internal List<PrefixCommand> Commands { get; set; } = [];

        internal PrefixCommandGroup(string name)
        {
            Name = name;
            if (!PrefixCommandManager.CommandGroups.Any(commandGroup => commandGroup.Name == name))
                PrefixCommandManager.CommandGroups.Add(this);
        }

        public override string ToString() => Name;
    }
}
