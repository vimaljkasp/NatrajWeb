using Platform.DTO;
using Platform.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service
{
    public class UserConvertor
    {
        public static UserDTO ConvertToUserDto(User user)
        {
            UserDTO userDTO = new UserDTO();
            userDTO.UserId = user.UserId;
            userDTO.RoleId = user.RoleId;
            userDTO.Name = user.Name;
            userDTO.Password = user.Password;
            userDTO.Permission = user.Permission;
            userDTO.SecurityCode = user.SecurityCode;
            userDTO.DateOfCreation = user.DateOfCreation;

            userDTO.ModifiedDate = user.ModifiedDate;
            userDTO.CreatedBy = user.CreatedBy;
            userDTO.CreatedDate = user.CreatedDate;
            userDTO.IsDeleted = user.IsDeleted;
            userDTO.ModifiedBy = user.ModifiedBy;

            return userDTO;



    }

        public static void ConvertToUserEntity(ref User user, UserDTO userDTO, bool isUpdate)
        {
            if (isUpdate)
             user.UserId = userDTO.UserId;
            user.RoleId = userDTO.RoleId;
            user.Name = userDTO.Name;
            user.Password = userDTO.Password;
            user.Permission = userDTO.Permission;
            user.SecurityCode = userDTO.SecurityCode;
            user.DateOfCreation = userDTO.DateOfCreation;

            user.ModifiedDate = userDTO.ModifiedDate;
            user.CreatedBy = userDTO.CreatedBy;
            user.CreatedDate = userDTO.CreatedDate;
            user.IsDeleted = userDTO.IsDeleted;
            user.ModifiedBy = userDTO.ModifiedBy;


        }
    }
}
