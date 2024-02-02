namespace KWiJisho.Modules.Commands.Prefix
{
    /// <summary>
    /// Provides a set of commands and classes for managing commands in a Discord server.
    /// </summary>
    internal partial class PrefixCommandManager
    {
        /// <summary>
        /// Represents a set of prefix commands to change theme in a Discord server.
        /// </summary>
        internal partial class PrefixTheme
        {
            /// <summary>
            /// Represents seasonal themes that you can set to the Discord server.
            /// </summary>
            internal enum EmojiTheme { Default, Christmas, Easter, Halloween }
        }
    }
}
