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
                if (dcEmployee.IsDeleted.GetValueOrDefault())
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
                VLC vLC = unitOfWork.VLCRepository.GetById(changePasswordDTO.Id);
                changePasswordDTO.CurrentPassword = EncryptionHelper.Encryptword(changePasswordDTO.CurrentPassword);
                if (vLC.Password.Equals(changePasswordDTO.CurrentPassword, StringComparison.CurrentCultureIgnoreCase))
                {

                    vLC.Password = EncryptionHelper.Encryptword(changePasswordDTO.NewPassword);
                    unitOfWork.VLCRepository.Update(vLC);
                    unitOfWork.SaveChanges();
                    //    this.CreateEmployeeSession(employee, logindto);

                    return true;
                }
                else

                {
                    throw new PlatformModuleException("The Current Password doesn't match any account");
                }


            }
            else if (changePasswordDTO.LoginType == LoginType.DistributionCenter)
            {
                DistributionCenter distributionCenter = unitOfWork.DistributionCenterRepository.GetById(changePasswordDTO.Id);
                changePasswordDTO.CurrentPassword = EncryptionHelper.Encryptword(changePasswordDTO.CurrentPassword);
                if (distributionCenter.Password.Equals(changePasswordDTO.CurrentPassword, StringComparison.CurrentCultureIgnoreCase))
                {
                    distributionCenter.Password = EncryptionHelper.Encryptword(changePasswordDTO.NewPassword);
                    unitOfWork.DistributionCenterRepository.Update(distributionCenter);
                    unitOfWork.SaveChanges();

                    return true;
                }
                else

                {
                    throw new PlatformModuleException("The Current Password doesn't match any account");
                }
            }

            //else if (changePasswordDTO.LoginType == LoginType.UserLogin)
            //{
            //    List<> users = unitOfWork.UserRepository.GetAll();
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


        public bool ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            if (resetPasswordDTO.LoginType == LoginType.VLC)
            {
                VLC vLC = unitOfWork.VLCRepository.GetById(resetPasswordDTO.Id);

                if (vLC.Pin.Equals(resetPasswordDTO.OTP, StringComparison.CurrentCultureIgnoreCase))
                {

                    vLC.Password = EncryptionHelper.Encryptword(resetPasswordDTO.Password);
                    unitOfWork.VLCRepository.Update(vLC);
                    unitOfWork.SaveChanges();
                    //    this.CreateEmployeeSession(employee, logindto);

                    return true;
                }
                else

                {
                    throw new PlatformModuleException("The Current Password doesn't match any account");
                }


            }
            else if (resetPasswordDTO.LoginType == LoginType.DistributionCenter)
            {
                DistributionCenter distributionCenter = unitOfWork.DistributionCenterRepository.GetById(resetPasswordDTO.Id);
                if (distributionCenter.Pin.Equals(resetPasswordDTO.OTP, StringComparison.CurrentCultureIgnoreCase))
                {
                    distributionCenter.Password = EncryptionHelper.Encryptword(resetPasswordDTO.Password);
                    unitOfWork.DistributionCenterRepository.Update(distributionCenter);
                    unitOfWork.SaveChanges();

                    return true;
                }
                else

                {
                    throw new PlatformModuleException("The Current Password doesn't match any account");
                }
            }
            return false;

        }


        public ResponseDTO ForgotPassword(ForgotPasswordDTO forgotPasswordDTO)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            if (forgotPasswordDTO.LoginType == LoginType.VLC)
            {
                VLC vLC = unitOfWork.VLCRepository.GetByUserName(forgotPasswordDTO.UserName);
                if (vLC != null)
                {
                    string otp = OTPGenerator.GetSixDigitOTP();
                    vLC.Pin = otp;
                    this.SendOTP(otp, vLC.Contact, NatrajComponent.VLC);
                    unitOfWork.VLCRepository.Update(vLC);
                    unitOfWork.SaveChanges();
                    responseDTO.Status = true;
                    responseDTO.Message = string.Format("A OTP has been sent to registered mobile number - {0}", vLC.Contact);
                    responseDTO.Data = new object();
                }
                else

                {
                    throw new PlatformModuleException("The User Name Does Not Exist");
                }


            }
            else if (forgotPasswordDTO.LoginType == LoginType.DistributionCenter)
            {
                DistributionCenter distributionCenter = unitOfWork.DistributionCenterRepository.GetDistributionCenterByMobileNumber(forgotPasswordDTO.UserName);
                if (distributionCenter != null)
                {
                    string otp = OTPGenerator.GetSixDigitOTP();
                    distributionCenter.Pin = otp;
                    this.SendOTP(otp, distributionCenter.Contact, NatrajComponent.DC);
                    unitOfWork.DistributionCenterRepository.Update(distributionCenter);
                    unitOfWork.SaveChanges();
                    responseDTO.Status = true;
                    responseDTO.Message = string.Format("A OTP has been sent to registered mobile number - {0}", distributionCenter.Contact);
                    responseDTO.Data = new object();
                }
                else

                {
                    throw new PlatformModuleException("The User Name Does Not Exist");
                }
            }
            return responseDTO;
        }


        public void SendOTP(string OTP,string mobileNumber,NatrajComponent natrajComponent)
        {
            string otpMessage = string.Format(unitOfWork.NatrajConfigurationSettings.ForgotPasswordOTPMessage,OTP);
            NatrajSMSLog natrajSMSLog = new NatrajSMSLog();
            natrajSMSLog.SMSId = unitOfWork.DashboardRepository.NextNumberGenerator("NatrajSMSLog");
            SMSConvertor.ConvertToSMSMessage(ref natrajSMSLog, natrajComponent, SMSType.ForgotPasswordOTP, mobileNumber, otpMessage);
            unitOfWork.SMSRepository.Add(natrajSMSLog);
            new SMSService().SendEmailInBackgroundThread(natrajSMSLog);
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
