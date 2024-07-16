// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using SQLite;

namespace KWiJisho.Database
{
    public class Connection
    {
        /// <summary>
        /// The database full file path.
        /// </summary>
        private static string DatabasePath => Path.Combine(DatabaseFolderPath, DatabaseFileName);

        /// <summary>
        /// The database folder path.
        /// </summary>
        private static string DatabaseFolderPath => "../../data/";

        /// <summary>
        /// The database file name.
        /// </summary>
        private static string DatabaseFileName => "KWIJISHO.db";

        /// <summary>
        /// The database connection.
        /// </summary>
        public SQLiteAsyncConnection SQLiteAsyncConnection { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Connection"/> class.
        /// </summary>
        public Connection()
        {
            SQLiteAsyncConnection = new(DatabasePath);
        }
    }
}
