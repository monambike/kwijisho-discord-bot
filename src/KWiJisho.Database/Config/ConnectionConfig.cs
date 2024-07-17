// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

namespace KWiJisho.Database.Config
{
    public static class ConnectionConfig
    {
        /// <summary>
        /// The database full file path.
        /// </summary>
        public static string DatabasePath => Path.Combine(DatabaseFolderPath, DatabaseFileName);

        /// <summary>
        /// The database folder path.
        /// </summary>
        private static string DatabaseFolderPath => "";

        /// <summary>
        /// The database file name.
        /// </summary>
        private static string DatabaseFileName => "KWiJisho.db";
    }
}
