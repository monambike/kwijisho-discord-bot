// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using SQLite;

namespace KWiJisho.Database.Data
{
    [Table("Log")]
    public class Log
    {
        public int LogId { get; set; }

        public string Content { get; set; }
    }
}
