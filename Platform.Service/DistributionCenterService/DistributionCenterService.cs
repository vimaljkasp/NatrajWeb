using Platform.DTO;
using Platform.Repository;
using Platform.Sql;
using Platform.Utilities.Encryption;
using Platform.Utilities.ExceptionHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            distributionCenter.CreatedDate = DateTime.Now;
            distributionCenter.ModifiedDate = DateTime.Now;
            distributionCenter.CreatedBy = distributionCenter.ModifiedBy = "Admin";
               // unitOfWork.VLCRepository.GetEmployeeNameByVLCId(customerDto.VLCId);
            distributionCenter.DateOfRegistration = DateTime.Now.Date;
            distributionCenter.IsDeleted = true;
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

        public ResponseDTO UpdateDistributionCenter(DistributionCenterDTO distributionCenterDTO)
        {
            var distributionCenter = unitOfWork.DistributionCenterRepository.GetById(distributionCenterDTO.DCId);
            if (distributionCenter != null)
            {
                DistributionCenterConvertor.ConvertToDistributionCenterEntity(ref distributionCenter, distributionCenterDTO, true);
                distributionCenter.ModifiedBy = distributionCenter.AgentName;
                distributionCenter.ModifiedDate = DateTime.Now;
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
                return null;
        }

        public void AddDistributionCenterWallet(DistributionCenter distributionCenter)
        {
            DCWallet dCWallet = new DCWallet();
            dCWallet.WalletId = unitOfWork.DashboardRepository.NextNumberGenerator("DCWallet");
            dCWallet.DCId = distributionCenter.DCId;
            dCWallet.WalletBalance = 0;
            dCWallet.AmountDueDate = DateTime.Now.AddDays(10);
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
                    if (distributionCenterDTO != null)
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

        public List<DistributionCenterDTO> GetAllDistributionCenters()
        {
            throw new NotImplementedException();
        }

        public List<DistributionCenterDTO> GetDistributionCentersByPageCount(int? pageNumber, int? count)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public ResponseDTO GetDistributionCentersByCity(string city, int? pageNumber)
        {
            throw new NotImplementedException();
        }
    }
}
