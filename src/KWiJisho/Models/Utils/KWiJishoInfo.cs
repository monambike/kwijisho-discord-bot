using System;
using System.Reflection;

namespace KWiJisho.Models.Utils
{
    internal class KWiJishoInfo
    {
        internal static string Name = Assembly.GetCallingAssembly().GetName().Name;

        internal static DateTime Created => new(28, 07, 2020);

        // Karen Kujo has born December 1th and has 15 years. So, she will always be
        // (thisYear - 15) years old
        internal static DateTime KarenKujoBirthday => new(28, 07, DateTime.Now.Year - 15);
    }
}
