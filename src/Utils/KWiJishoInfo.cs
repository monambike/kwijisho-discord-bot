using System;
using System.Reflection;

namespace KWiJisho.Utils
{
    /// <summary>
    /// Class that provides static basic information about KWiJisho bot.
    /// </summary>
    internal class KWiJishoInfo
    {
        /// <summary>
        /// KWiJisho project name gotten directly from <see cref="Assembly"/>.
        /// </summary>
        internal static string Name = Assembly.GetCallingAssembly().GetName().Name;

        /// <summary>
        /// The date Karen Kujo project was first commit on <a href="https://github.com/monambike/kwijisho-discord-bot">GitHub repository</a>.
        /// </summary>
        internal static DateTime Created => new(2020, 07, 28);

        /// <summary>
        /// Gets the calculated birthday for Karen Kujo. Karen Kujo was born on December 1st,
        /// and the age is fixed at 15 years.
        /// The property provides the current date with the year adjusted to maintain the age of 15.
        /// </summary>
        internal static DateTime KarenKujoBirthday => new(DateTime.Now.Year - 15, 12, 01);
    }
}
