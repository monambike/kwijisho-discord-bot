// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using KWiJisho.Database.Entities;
using KWiJisho.Database.DTO;

namespace KWiJisho.Database.Helpers
{
    internal class Mapper
    {
        public static TDTO? MapToModel<TEntity, TDTO>(TEntity? entity, Func<TEntity, TDTO> mappingFunc)
        {
            if (entity == null) return null;
            return mappingFunc(entity);
        }

        public static UserDTO? MapUserDataToUserModel(UserEntity? userData)
        {
            if (userData == null) return null;

            return new UserDTO
            {
                UserId = userData.UserId,
                UserGuid = userData.UserGuid,
                Username = userData.Username,
                Birthday = userData.Birthday != null ? DateTime.Parse(userData.Birthday) : (DateTime?)null
            };
        }
    }
}
