// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using KWiJisho.Database.DTO;
using KWiJisho.Database.Entities;
using KWiJisho.Database.Helpers;
using SQLite;

namespace KWiJisho.Database.Repositories
{
    /// <summary>
    /// Repository class for managing operations related to User.
    /// </summary>
    public class UserRepository
    {
        private readonly SQLiteAsyncConnection _connection = DatabaseService.Connection;

        /// <summary>
        /// Asynchronously retrieves a User information associated with the specified user GUID.
        /// </summary>
        /// <param name="userGuid">The GUID of the user for which to retrieve the User information.</param>
        /// <returns>
        /// A Task that represents the asynchronous operation. The task result contains 
        /// the first matching User, or null if no match is found.
        /// </returns>
        public async Task<UserDTO?> GetUserByServerGuid(int userGuid)
        {
            try
            {
                var result = await _connection.Table<UserEntity>()
                    .Where(user => user.UserId == userGuid)
                    .ToListAsync();

                var userData = result.FirstOrDefault();

                return MapEntityToDTO(userData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message} while trying to get user data.");
                throw;
            }
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
