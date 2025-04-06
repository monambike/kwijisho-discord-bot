// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using System;
using System.Diagnostics;
using System.Reflection;

namespace KWiJisho.Data
{
    /// <summary>
    /// Class that provides static basic information about KWiJisho bot.
    /// </summary>
    public class KWiJisho
    {
        /// <summary>
        /// KWiJisho user id retrieved from Discord.
        /// </summary>
        public static ulong Id = 737535848102363259;

        /// <summary>
        /// KWiJisho project name retrieved directly from <see cref="Assembly"/>.
        /// </summary>
        public static string? Name = Assembly.GetCallingAssembly().GetName().Name;

        /// <summary>
        /// The date Karen Kujo project was first commit on <a href="https://github.com/monambike/kwijisho-discord-bot">GitHub repository</a>.
        /// </summary>
        public static DateTime Created => new(2020, 07, 28);

        /// <summary>
        /// Gets the calculated birthday for Karen Kujo. Karen Kujo was born on December 1st,
        /// and the age is fixed at 15 years.
        /// The property provides the current date with the year adjusted to maintain the age of 15.
        /// </summary>
        public static DateTime KarenKujoBirthday => new(DateTime.Now.Year - 15, 12, 01);
    }
}
