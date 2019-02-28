using Platform.DTO;
using Platform.Repository;
using Platform.Sql;
using Platform.Utilities;
using System;
using System.Collections.Generic;


namespace Platform.Service
{
    public class UserService : IUserService,IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public void AddUser(UserDTO userDTO)
        {
            
            User user = new User();
            UserConvertor.ConvertToUserEntity(ref user, userDTO, false);
            user.Password = EncryptionHelper.Encryptword(userDTO.Password);
           unitOfWork.UserRepository.Add(user);

        }

        public void DeleteUser(int employeeId)
        {
            unitOfWork.UserRepository.Delete(employeeId);
        }

        public List<UserDTO> GetAllUsers()
        {
            List<UserDTO> userList = new List<UserDTO>();
            var users = unitOfWork.UserRepository.GetAll();
            if (users != null)
            {
                foreach (var user in users)
                {
                    userList.Add(UserConvertor.ConvertToUserDto(user));
                }

            }

            return userList;
        }

        public UserDTO GetUserById(int userId)
        {
            UserDTO userDTO = null;
            var user = unitOfWork.UserRepository.GetById(userId);
            if (user != null)
            {
                userDTO = UserConvertor.ConvertToUserDto(user);
            }
            return userDTO;
        }

        public void UpdateUser(UserDTO userDTO)
        {
           
            User user = new User();
            UserConvertor.ConvertToUserEntity(ref user, userDTO, true);
            unitOfWork.UserRepository.Update(user);
        }


     

      

        //public bool ValidateLoginAndCreateEmployeeSession(LoginDto logindto)
        //{
        //    //List<Employee> employees = unitOfWork.EmployeeRepository.GetAllEmployees();
        //    //logindto.Password = EncryptionHelper.Encryptword(logindto.Password);
        //    //if (employees.Where(e => e.UserName.Equals(logindto.UserName, StringComparison.CurrentCultureIgnoreCase)
        //    // && e.Password.Equals(logindto.Password, StringComparison.CurrentCultureIgnoreCase)).Any())
        //    //{
        //    //    var employee = employees.Where(e => e.UserName.Equals(logindto.UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
        //    //    this.CreateEmployeeSession(employee, logindto);
               
        //    //    return true;
        //    //}
        //    //else if (!employees.Where(e => e.UserName.Equals(logindto.UserName, StringComparison.CurrentCultureIgnoreCase)).Any())

        //    //{
        //    //    throw new PlatformModuleException("The UserName that you've entered doesn't match any account");
        //    //}
        //    //else if (!employees.Where(e => e.UserName.Equals(logindto.Password, StringComparison.CurrentCultureIgnoreCase)).Any())
        //    //{
        //    //    throw new PlatformModuleException("Password that you've entered doesn't match any account");

        //    //}
        //    //return false;
            
        //}

        private void CreateUserSession(User user,LoginDto logindto)
        {
            
            //EmployeeSession employeeSession = new EmployeeSession();
            //employeeSession.SiteId = logindto.SiteId;
            //employeeSession.EmployeeId = employee.EmployeeId;
            //employeeSession.SessionStartDtm = DateTimeHelper.GetISTDateTime();
            //employeeSession.SessionEndDtm = DateTime.MaxValue;
            //employeeSession.IsLogout = false;
            //unitOfWork.EmployeeSessionRepository.Add(employeeSession);
            //unitOfWork.SaveChanges();
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (unitOfWork != null)
                {
                    unitOfWork.Dispose();
                    unitOfWork = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
