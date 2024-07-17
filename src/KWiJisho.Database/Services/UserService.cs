// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using KWiJisho.Database.Config;
using KWiJisho.Database.Entities;
using KWiJisho.Database.DTO;
using SQLite;

namespace KWiJisho.Database.Services
{
    /// <summary>
    /// Service class for managing operations related to User.
    /// </summary>
    public class UserService
    {
        /// <summary>
        /// Asynchronously retrieves a User information associated with the specified user GUID.
        /// </summary>
        /// <param name="userGuid">The GUID of the user for which to retrieve the User information.</param>
        /// <returns>
        /// A Task that represents the asynchronous operation. The task result contains 
        /// the first matching User, or null if no match is found.
        /// </returns>
        public static async Task<UserDTO?> GetUserByServerGuid(int userGuid)
        {
            // Establish a connection to the SQLite database asynchronously.
            var connection = new SQLiteAsyncConnection(ConnectionConfig.DatabasePath);

            // Query the User table for entries matching the specified userGuid,
            // and retrieve the results as a list asynchronously.
            var result = await connection.Table<UserEntity>()
                .Where(user => user.UserId == userGuid)
                .ToListAsync();

            var userData = result.FirstOrDefault();

            return MapEntityToDTO(userData);
        }

        private static UserDTO? MapEntityToDTO(UserEntity? userData)
        {
            if (userData == null) return null;

            return new UserDTO
            {
                UserId = userData.UserId,
                UserGuid = userData.UserGuid,
                Username = userData.Username,
                Birthday = userData.Birthday != null ? DateTime.Parse(userData.Birthday) : null
            };
        }
    }
}
