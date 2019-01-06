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


        //public List<CustomerDto> GetAllCustomers()
        //{
        //    List<CustomerDto> customerList = new List<CustomerDto>();
        //    var customers = unitOfWork.CustomerRepository.GetAll();
        //    if (customers != null)
        //    {
        //        foreach (var customer in customers)
        //        {
        //            customerList.Add(CustomerConvertor.ConvertToCustomerDto(customer));
        //        }

        //    }

        //    return customerList;

        //}

        //public List<CustomerDto> GetCustomerByPageCount(int? pageNumber, int? count)
        //{
        //    List<CustomerDto> customerList = new List<CustomerDto>();
        //    var customers = unitOfWork.CustomerRepository.GetCustomerByCount(pageNumber, count);
        //    if (customers != null)
        //    {
        //        foreach (var customer in customers)
        //        {
        //            customerList.Add(CustomerConvertor.ConvertToCustomerDto(customer));
        //        }

        //    }

        //    return customerList;

        //}

        //public ResponseDTO GetCustomerListByVLCId(int vlcId, int? pageNumber)
        //{
        //    ResponseDTO responseDTO = new ResponseDTO();

        //    List<CustomerDto> customerList = new List<CustomerDto>();
        //    var customers = unitOfWork.CustomerRepository.GetCustomerListByVLCId(vlcId, pageNumber);
        //    if (customers != null)
        //    {
        //        foreach (var customer in customers)
        //        {
        //            customerList.Add(CustomerConvertor.ConvertToCustomerDto(customer));
        //        }

        //    }
        //    responseDTO.Status = true;
        //    responseDTO.Message = String.Format("List of Customers By VLC Id");
        //    responseDTO.Data = customerList;
        //    return responseDTO;

        //}

        //public ResponseDTO GetCustomerDetailsByCustomerId(int customerId)
        //{
        //    ResponseDTO responseDTO = new ResponseDTO();

        //    CustomerDetailsDTO customerDetailsDto = new CustomerDetailsDTO();
        //    var customer = unitOfWork.CustomerRepository.GetById(customerId);
        //    if (customer != null)
        //    {
        //        customerDetailsDto.customerProfileDetails = CustomerConvertor.ConvertToCustomerDto(customer);
        //        if (customer.CustomerBanks != null)
        //        {
        //            customerDetailsDto.customerBankDetails = CustomerBankConvertor.ConvertTocustomerBankDto(customer.CustomerBanks.FirstOrDefault());
        //        }
        //        else
        //        {
        //            customerDetailsDto.customerBankDetails = new CustomerBankDTO();
        //        }
        //        if (customer.CustomerAgricultures != null)
        //        {
        //            customerDetailsDto.customerAgricultureDetails = CustomerAgricultureConvertor.ConvertToCustomerAgriCultureDto(customer.CustomerAgricultures.FirstOrDefault());
        //        }
        //        else
        //        {
        //            customerDetailsDto.customerAgricultureDetails = new CustomerAgricultureDTO();
        //        }
        //        responseDTO.Status = true;
        //        responseDTO.Message = "Customer Profile Details";
        //        responseDTO.Data = customerDetailsDto;

        //    }
        //    else
        //    {
        //        throw new PlatformModuleException("Customer  Details Not Found with given Customer Id");

        //    }
        //    return responseDTO;

        //}

        //public List<CustomerDto> GetCustomerDetailsByVLCCode(string vlcCode)
        //{
        //    List<CustomerDto> customerList = new List<CustomerDto>();
        //    var customers = unitOfWork.CustomerRepository.GetAll().Where(id => id.VLC.VLCCode == vlcCode).ToList();
        //    if (customers != null)
        //    {
        //        foreach (var customer in customers)
        //        {
        //            customerList.Add(CustomerConvertor.ConvertToCustomerDto(customer));
        //        }

        //    }

        //    return customerList;
        //}

        //public ResponseDTO GetCustomerByCustomerId(int customerId)
        //{
        //    ResponseDTO responseDTO = new ResponseDTO();
        //    CustomerDto customerDto = null;
        //    var customer = unitOfWork.CustomerRepository.GetById(customerId);
        //    if (customer != null)
        //    {
        //        customerDto = CustomerConvertor.ConvertToCustomerDto(customer);
        //        responseDTO.Status = true;
        //        responseDTO.Message = "Customer Detail By Customer Id";
        //        responseDTO.Data = customerDto;
        //    }
        //    else
        //    {
        //        responseDTO.Status = false;
        //        responseDTO.Message = "No Customer Exist with given customer Id";
        //        responseDTO.Data = new object();
        //    }
        //    return responseDTO;
        //}



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
            distributionCenter.IsDeleted = false;
            distributionCenterDTO.DCId = distributionCenter.DCId;
            unitOfWork.DistributionCenterRepository.Add(distributionCenter);
            //creating Distribution Center wallet with Distribution Center 
            AddDistributionCenterWallet(distributionCenter);
            AddDistributionCenterAddress(distributionCenterDTO);
            
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

        public void AddDistributionCenterAddress(DistributionCenterDTO distributionCenterDTO)
        {
            if(distributionCenterDTO.DCAddressDTO !=null)
            {
                distributionCenterDTO.DCAddressDTO.DCId = distributionCenterDTO.DCId;
                distributionCenterDTO.DCAddressDTO.Contact = distributionCenterDTO.Contact;
                DCAddress dCAddress = new DCAddress();
                dCAddress.DCAddressId= unitOfWork.DashboardRepository.NextNumberGenerator("DCAddress");

                dCAddress.AddressTypeId = 1;
                dCAddress.IsDefaultAddress = true;
                DCAddressConvertor.ConvertToDCAddressEntity(ref dCAddress, distributionCenterDTO.DCAddressDTO, false);
                unitOfWork.DCAddressRepository.Add(dCAddress);

            }
        }

        public void AddDistributionCenterWallet(DistributionCenter distributionCenter)
        {
            DCWallet dCWallet = new DCWallet();
            dCWallet.WalletId = unitOfWork.DashboardRepository.NextNumberGenerator("DCWallet");
            dCWallet.DCId = distributionCenter.DCId;
            dCWallet.WalletBalance = 0;
            dCWallet.AmountDueDate = DateTime.Now.AddDays(10);
            unitOfWork.DCWalletRepository.Add(dCWallet);
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

        //public ResponseDTO UpdateCustomer(CustomerDto customerDto)
        //{

        //    var customer = unitOfWork.CustomerRepository.GetById(customerDto.CustomerId);
        //    CustomerConvertor.ConvertToCustomerEntity(ref customer, customerDto, true);

        //    customer.ModifiedDate = DateTime.Now;
        //    customer.ModifiedBy = unitOfWork.VLCRepository.GetEmployeeNameByVLCId(customerDto.VLCId);

        //    unitOfWork.CustomerRepository.Update(customer);
        //    unitOfWork.SaveChanges();
        //    ResponseDTO responseDTO = new ResponseDTO();
        //    responseDTO.Status = true;
        //    responseDTO.Message = String.Format("Customer Updated Successfully");
        //    responseDTO.Data = CustomerConvertor.ConvertToCustomerDto(customer);
        //    return responseDTO;
        //}

        //public ResponseDTO DeleteCustomer(int id)
        //{
        //    UnitOfWork unitOfWork = new UnitOfWork();
        //    //get customer
        //    var customer = unitOfWork.CustomerRepository.GetById(id);
        //    //if((customer.ProductOrders !=null && customer.ProductOrders.Count()>0) || (customer.CustomerWallets !=null && 
        //    //    customer.CustomerWallets.Count()>0 && customer.CustomerWallets.FirstOrDefault().WalletBalance>0))
        //    //    {
        //    //    throw new PlatformModuleException("Customer Account Cannot be deleted as it is associated with orders");
        //    //}
        //    unitOfWork.CustomerRepository.Delete(id);
        //    unitOfWork.SaveChanges();
        //    ResponseDTO responseDTO = new ResponseDTO();
        //    responseDTO.Status = true;
        //    responseDTO.Message = String.Format("Customer Deleted Successfully");
        //    responseDTO.Data = CustomerConvertor.ConvertToCustomerDto(customer);
        //    return responseDTO;

        //}
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
            throw new NotImplementedException();
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
