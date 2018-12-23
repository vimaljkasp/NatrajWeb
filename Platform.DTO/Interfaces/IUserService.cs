
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public interface IUserService
    {
        List<UserDTO> GetAllUsers();



        UserDTO GetUserById(int userId);


        void AddUser(UserDTO userDTO);


         void UpdateUser(UserDTO userDTO);


         void DeleteUser(int userId);

     //   bool ValidateLoginAndCreateUserSession(LoginDto loginDto);


       

    }
}
