using Platform.DTO;
using Platform.Repository;
using Platform.Sql;
using Platform.Utilities;
using System;
using System.Collections.Generic;


namespace Platform.Service
{
    public class DistributionCenterService : IDistributionCenterService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public ResponseDTO AddDistributionCenter(DistributionCenterDTO distributionCenterDTO)
        {
            this.CheckForExisitngDistributionCenter(distributionCenterDTO);
            ResponseDTO responseDTO = new ResponseDTO();
            DistributionCenter distributionCenter = new DistributionCenter();
            distributionCenter.DCId = unitOfWork.DashboardRepository.NextNumberGenerator("DistributionCenter");
            distributionCenterDTO.Password = EncryptionHelper.Encryptword(distributionCenterDTO.Password);
            DistributionCenterConvertor.ConvertToDistributionCenterEntity(ref distributionCenter, distributionCenterDTO, false);
            //   customer.CustomerCode = unitOfWork.CustomerRepository.GetCustomerCodeIdByVLC(customerDto.VLCId);
            distributionCenter.DCCode = "DC" + distributionCenter.DCId.ToString();
            distributionCenter.CreatedDate = DateTimeHelper.GetISTDateTime();
            distributionCenter.ModifiedDate = DateTimeHelper.GetISTDateTime();
            distributionCenter.CreatedBy = distributionCenter.ModifiedBy = "Admin";
               // unitOfWork.VLCRepository.GetEmployeeNameByVLCId(customerDto.VLCId);
            distributionCenter.DateOfRegistration = DateTimeHelper.GetISTDateTime().Date;
            distributionCenter.IsDeleted = true;
            distributionCenter.Pin = OTPGenerator.GetSixDigitOTP();
            distributionCenterDTO.DCId = distributionCenter.DCId;
            
            //creating Distribution Center wallet with Distribution Center 
            AddDistributionCenterWallet(distributionCenter);
           DCAddress dCAddress= AddDistributionCenterAddress(distributionCenterDTO);
            if (dCAddress != null)
                distributionCenter.DCAddresses.Add(dCAddress);
            unitOfWork.DistributionCenterRepository.Add(distributionCenter);
            unitOfWork.SaveChanges();
            responseDTO.Status = true;
            responseDTO.Message = String.Format("Distribution Center Successfully Created");
            responseDTO.Data = DistributionCenterConvertor.ConvertToDistributionCenterDto(distributionCenter);
            return responseDTO;

        }

        public void SendSMSForMobileNumberVerfication(DistributionCenter distributionCenter)
        {
            SMSService sMSService = new SMSService();
            string message =string.Format(unitOfWork.MessageRepository.GetMessageByMessageCode("DCSignUp", NatrajMessages.DCSignUpMessage),distributionCenter.Pin);

            sMSService.SendMessage(NatrajComponent.DC, SMSType.SignUp, distributionCenter.Contact, message);
        }

        public ResponseDTO UpdateDistributionCenter(DistributionCenterDTO distributionCenterDTO)
        {
            var distributionCenter = unitOfWork.DistributionCenterRepository.GetById(distributionCenterDTO.DCId);
            if (distributionCenter != null)
            {
                DistributionCenterConvertor.ConvertToDistributionCenterEntity(ref distributionCenter, distributionCenterDTO, true);
                distributionCenter.ModifiedBy = distributionCenter.AgentName;
                distributionCenter.ModifiedDate = DateTimeHelper.GetISTDateTime();
                unitOfWork.DistributionCenterRepository.Update(distributionCenter);
                unitOfWork.SaveChanges();
                ResponseDTO responseDTO = new ResponseDTO();
                responseDTO.Status = true;
                responseDTO.Message = "Distribution Center Succesfully Updated";
                responseDTO.Data = DistributionCenterConvertor.ConvertToDistributionCenterDto(distributionCenter);
                return responseDTO;
            }
            else
            {
                throw new PlatformModuleException("Distribution Center Not Found with given DC Id");
            }
            
        }

        public DCAddress AddDistributionCenterAddress(DistributionCenterDTO distributionCenterDTO)
        {
            if (distributionCenterDTO.DCAddressDTO != null)
            {
                distributionCenterDTO.DCAddressDTO.DCId = distributionCenterDTO.DCId;
                distributionCenterDTO.DCAddressDTO.Contact = distributionCenterDTO.Contact;
                DCAddress dCAddress = new DCAddress();
                dCAddress.DCAddressId = unitOfWork.DashboardRepository.NextNumberGenerator("DCAddress");

                dCAddress.AddressTypeId = 1;
                dCAddress.IsDefaultAddress = true;
                DCAddressConvertor.ConvertToDCAddressEntity(ref dCAddress, distributionCenterDTO.DCAddressDTO, false);
                //   unitOfWork.DCAddressRepository.Add(dCAddress);
                return dCAddress;

            }
            else
                throw new PlatformModuleException("Distribution Center Address Details Not Provided");
        }

        public void AddDistributionCenterWallet(DistributionCenter distributionCenter)
        {
            DCWallet dCWallet = new DCWallet();
            dCWallet.WalletId = unitOfWork.DashboardRepository.NextNumberGenerator("DCWallet");
            dCWallet.DCId = distributionCenter.DCId;
            dCWallet.WalletBalance = 0;
            dCWallet.AmountDueDate = DateTimeHelper.GetISTDateTime().AddDays(10);
            distributionCenter.DCWallets.Add(dCWallet);
          //  unitOfWork.DCWalletRepository.Add(dCWallet);
        }

        private void CheckForExisitngDistributionCenter(DistributionCenterDTO distributionCenterDTO)
        {
            DistributionCenter existingDistributionCenter = null;
            if (string.IsNullOrWhiteSpace(distributionCenterDTO.DCCode) == false)
            {
                 existingDistributionCenter = unitOfWork.DistributionCenterRepository.GetDistributionCenterByCode(distributionCenterDTO.DCCode);
                if (existingDistributionCenter != null)
                    throw new PlatformModuleException("Distribution Center Already Exist with given DC Code");
            }
                if (string.IsNullOrWhiteSpace(distributionCenterDTO.Contact) == false)
                {
                    existingDistributionCenter = unitOfWork.DistributionCenterRepository.GetDistributionCenterByMobileNumber(distributionCenterDTO.Contact);

                    if (existingDistributionCenter != null)
                        throw new PlatformModuleException("Distribution Center Already Exist with given Mobile Number");
                }

                if (string.IsNullOrWhiteSpace(distributionCenterDTO.Email) == false)
                {
                    existingDistributionCenter = unitOfWork.DistributionCenterRepository.GetDistributionCenterByEmail(distributionCenterDTO.Email);
                    if (existingDistributionCenter != null)
                        throw new PlatformModuleException("Distribution Center Already Exist with given Email");
                }


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

        public ResponseDTO GetAllDistributionCenters()
        {
            ResponseDTO responseDTO = new ResponseDTO();
            responseDTO.Message = "Distribution Center Details";
            responseDTO.Status = true;
            List<DistributionCenterDTO> dcList = new List<DistributionCenterDTO>();
            var distributionCenters = unitOfWork.DistributionCenterRepository.GetAll();
            if (distributionCenters != null)
            {
                foreach (var dc in distributionCenters)
                {
                    dcList.Add(DistributionCenterConvertor.ConvertToDistributionCenterDto(dc));
                }

            }

            responseDTO.Data = dcList;
            return responseDTO;
        }

        public ResponseDTO GetDistributionCentersByPageCount(int? pageNumber, int? count)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            responseDTO.Message = "Distribution Center Details";
            responseDTO.Status = true;
            List<DistributionCenterDTO> dcList = new List<DistributionCenterDTO>();
            var distributionCenters = unitOfWork.DistributionCenterRepository.GetDistributionCenterByCount(pageNumber, count);
            if (distributionCenters != null)
            {
                foreach (var dc in distributionCenters)
                {
                    dcList.Add(DistributionCenterConvertor.ConvertToDistributionCenterDto(dc));
                }

            }

            responseDTO.Data = dcList;
            return responseDTO;
        }

        public ResponseDTO GetDistributionCenterByCenterId(int dcId)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            DistributionCenterDTO distributionCenterDTO = null;
            var distributionCenter = unitOfWork.DistributionCenterRepository.GetById(dcId);
            if (distributionCenter != null)
            {
                distributionCenterDTO = DistributionCenterConvertor.ConvertToDistributionCenterDto(distributionCenter);
                responseDTO.Status = true;
                responseDTO.Message = "Distribution Center Details By DC Id";
                responseDTO.Data = distributionCenterDTO;
            }
            else
            {
                throw new PlatformModuleException("Distribution Center Details Not Found");
            }
            return responseDTO;
        }

       

      

        public ResponseDTO DeleteDistriubtionCenter(int id)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            responseDTO.Message = "Distribution Center Details by city";
            responseDTO.Status = true;
            var dc = unitOfWork.DistributionCenterRepository.GetById(id);
            if (dc != null)
            {
                dc.IsDeleted = true;
                unitOfWork.SaveChanges();
                responseDTO.Data = DistributionCenterConvertor.ConvertToDistributionCenterDto(dc);
                return responseDTO;
            }
            else
            {
                throw new PlatformModuleException("Distribution Center Details Not Found");
            }
        }

        public ResponseDTO GetDistributionCentersByCity(string city, int? pageNumber)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            responseDTO.Message = "Distribution Center Details by city";
            responseDTO.Status = true;
            List<DistributionCenterDTO> dcList = new List<DistributionCenterDTO>();
            var distributionCenters = unitOfWork.DistributionCenterRepository.GetDistributionCenterListByCity(city, pageNumber);
            if (distributionCenters != null)
            {
                foreach (var dc in distributionCenters)
                {
                    dcList.Add(DistributionCenterConvertor.ConvertToDistributionCenterDto(dc));
                }

            }

            responseDTO.Data= dcList;
            return responseDTO;
        }

        public ResponseDTO UpdateDistributionCenterStatus(int dcId, bool status)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            responseDTO.Message = "Distribution Center Details by city";
            responseDTO.Status = true;
            var dc = unitOfWork.DistributionCenterRepository.GetById(dcId);
            if(dc!=null)
            {
                dc.IsDeleted = !status;
                unitOfWork.SaveChanges();
                responseDTO.Data = DistributionCenterConvertor.ConvertToDistributionCenterDto(dc);
                return responseDTO;
            }
            else
            {
                throw new PlatformModuleException("Distribution Center Details Not Found");
            }
        }


        public ResponseDTO GetDCWalletBalance(int dcId)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            responseDTO.Message = "Distribution Center Wallet Balance by DC ID";
            responseDTO.Status = true;
            var dc = unitOfWork.DCWalletRepository.GetByDCId(dcId);
            if(dc!=null)
            responseDTO.Data = dc.WalletBalance;
            else
            {
                throw new PlatformModuleException("DC Wallet Does Not Exist");
            }
            return responseDTO;
        }
    }
}
