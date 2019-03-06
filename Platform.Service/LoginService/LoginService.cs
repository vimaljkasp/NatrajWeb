using System;
using System.Collections.Generic;
using System.Linq;
using Platform.DTO;
using Platform.Repository;
using Platform.Sql;
using Platform.Utilities;

namespace Platform.Service
{
    public class LoginService : ILoginService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public LoggedInUserDTO ValidateLoginAndCreateLoginSession(string userName, string password)
        {
            LoggedInUserDTO loggedInUserDTO = new LoggedInUserDTO();
            password = EncryptionHelper.Encryptword(password);
            // DC Check
            List<DistributionCenter> distributionCenters = unitOfWork.DistributionCenterRepository.GetAll();
            if (distributionCenters.Where(v => v.Contact.Equals(userName, StringComparison.CurrentCultureIgnoreCase)
              && v.Password.Equals(password, StringComparison.CurrentCultureIgnoreCase)).Any())
            {
           
                var dcEmployee = distributionCenters.Where(e => e.Contact.Equals(userName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                if(dcEmployee.IsDeleted.GetValueOrDefault())
                    throw new PlatformModuleException("Your account is not active. Please Contact Administrator");

                loggedInUserDTO = LoggedInUserConvertor.ConvertToLoggedInDistributionCenterDTO(dcEmployee);
                loggedInUserDTO.LoginType = LoginType.DistributionCenter.ToString();
                return loggedInUserDTO;
            }
            //VLC Check
                List<VLC> vLCs = unitOfWork.VLCRepository.GetAll(); 
         
             if (vLCs.Where(v => v.VLCName.Equals(userName, StringComparison.CurrentCultureIgnoreCase)
                 && v.Password.Equals(password, StringComparison.CurrentCultureIgnoreCase)).Any())
            {
                var vlcEmployee = vLCs.Where(e => e.VLCName.Equals(userName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                loggedInUserDTO = LoggedInUserConvertor.ConvertToLoggedInUserDTO(vlcEmployee);
                loggedInUserDTO.LoginType = LoginType.VLC.ToString();
                return loggedInUserDTO;
            }
          
            else 
            {
                throw new PlatformModuleException("UserName and Password that you've entered doesn't match any account");

            }
           
        }
            //  }
            //else if (loginDto.LoginType ==LoginType.UserLogin)
            //{
            //    List<User> users = unitOfWork.UserRepository.GetAll();
            //    loginDto.Password = EncryptionHelper.Encryptword(loginDto.Password);
            //    if (users.Where(v => v.Name.Equals(loginDto.UserName, StringComparison.CurrentCultureIgnoreCase)
            //     && v.Password.Equals(loginDto.Password, StringComparison.CurrentCultureIgnoreCase)).Any())
            //    {
            //        var userEmployee = users.Where(e => e.Name.Equals(loginDto.UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            //        //    this.CreateEmployeeSession(employee, logindto);

            //        return true;
            //    }
            //    else if (!users.Where(e => e.Name.Equals(loginDto.UserName, StringComparison.CurrentCultureIgnoreCase)).Any())

            //    {
            //        throw new PlatformModuleException("The UserName that you've entered doesn't match any account");
            //    }
            //    else if (!users.Where(e => e.Password.Equals(loginDto.Password, StringComparison.CurrentCultureIgnoreCase)).Any())
            //    {
            //        throw new PlatformModuleException("Password that you've entered doesn't match any account");

            //    }
            //    return false;
            //}

            //return false;
        


        public bool ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            if (changePasswordDTO.LoginType == LoginType.VLC)
            {
                List<VLC> vLCs = unitOfWork.VLCRepository.GetAll();
                changePasswordDTO.CurrentPassword = EncryptionHelper.Encryptword(changePasswordDTO.CurrentPassword);
                if (vLCs.Where(v => v.VLCId==changePasswordDTO.VLCId && v.Password.Equals(changePasswordDTO.CurrentPassword, StringComparison.CurrentCultureIgnoreCase)).Any())
                {
                    var vlc =unitOfWork.VLCRepository.GetById(changePasswordDTO.VLCId);
                    vlc.Password = EncryptionHelper.Encryptword(changePasswordDTO.NewPassword);
                    //    this.CreateEmployeeSession(employee, logindto);

                    return true;
                }
                else

                {
                    throw new PlatformModuleException("The Current Password doesn't match any account");
                }
            
                
            }
            //else if (changePasswordDTO.LoginType == LoginType.UserLogin)
            //{
            //    List<User> users = unitOfWork.UserRepository.GetAll();
            //    loginDto.Password = EncryptionHelper.Encryptword(loginDto.Password);
            //    if (users.Where(v => v.Name.Equals(loginDto.UserName, StringComparison.CurrentCultureIgnoreCase)
            //     && v.Password.Equals(loginDto.Password, StringComparison.CurrentCultureIgnoreCase)).Any())
            //    {
            //        var userEmployee = users.Where(e => e.Name.Equals(loginDto.UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            //        //    this.CreateEmployeeSession(employee, logindto);

            //        return true;
            //    }
            //    else if (!users.Where(e => e.Name.Equals(loginDto.UserName, StringComparison.CurrentCultureIgnoreCase)).Any())

            //    {
            //        throw new PlatformModuleException("The UserName that you've entered doesn't match any account");
            //    }
            //    else if (!users.Where(e => e.Password.Equals(loginDto.Password, StringComparison.CurrentCultureIgnoreCase)).Any())
            //    {
            //        throw new PlatformModuleException("Password that you've entered doesn't match any account");

            //    }
            //    return false;
            //}

            return false;
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
