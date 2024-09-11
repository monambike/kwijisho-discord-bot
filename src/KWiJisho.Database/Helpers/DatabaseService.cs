// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using KWiJisho.Database.Config;
using SQLite;

namespace KWiJisho.Database.Helpers
{
    /// <summary>
    /// Singleton database service helper class.
    /// </summary>
    public class DatabaseService
    {
        private static readonly Lazy<SQLiteAsyncConnection> _connection = new(() => new SQLiteAsyncConnection(ConnectionConfig.DatabasePath));

        public static SQLiteAsyncConnection Connection => _connection.Value;

        private DatabaseService() { }
    }
}
