using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KWIJisho.Models.Commands.Themes
{
    internal partial class Theme
    {
        /// <summary>
        /// Class that represents a group or channel inside "Tramontina" server
        /// </summary>
        internal class Tramontina
        {
            public int ChannelId { get; set; }

            public string DefaultName { get; set; }

            public string DefaultEmoji { get; set; }

            public Tramontina()
            {

            }
        }
        public static List<Tramontina> Channels = new List<Tramontina>
        {

        };
    }
}
