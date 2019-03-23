using Platform.DTO;
using Platform.Repository;
using Platform.Sql;
using Platform.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Platform.Service
{
    public class CustomerService : ICustomerService,IDisposable
    {
        private  UnitOfWork unitOfWork=new UnitOfWork();
     public CustomerService(LoggerService loggerService)
        {
            
        }

        public List<CustomerDto> GetAllCustomers()
        { 
            List<CustomerDto> customerList = new List<CustomerDto>();
            var customers = unitOfWork.CustomerRepository.GetAll();
            if (customers != null)
            {
                foreach (var customer in customers)
                {
                    customerList.Add(CustomerConvertor.ConvertToCustomerDto(customer));
                }

            }

            return customerList;

        }

        public List<CustomerDto> GetCustomerByPageCount(int? pageNumber, int? count)
        {
            List<CustomerDto> customerList = new List<CustomerDto>();
            var customers = unitOfWork.CustomerRepository.GetCustomerByCount(pageNumber,count);
            if (customers != null)
            {
                foreach (var customer in customers)
                {
                    customerList.Add(CustomerConvertor.ConvertToCustomerDto(customer));
                }

            }

            return customerList;

        }

        public ResponseDTO GetCustomerListByVLCId(int vlcId,int? pageNumber)
        {
            ResponseDTO responseDTO = new ResponseDTO();

            List<CustomerDto> customerList = new List<CustomerDto>();
            var customers = unitOfWork.CustomerRepository.GetCustomerListByVLCId(vlcId,pageNumber);
            if (customers != null)
            {
                foreach (var customer in customers)
                {
                    customerList.Add(CustomerConvertor.ConvertToCustomerDto(customer));
                }

            }
            responseDTO.Status = true;
            responseDTO.Message = String.Format("List of Customers By VLC Id");
            responseDTO.Data = customerList;
            return responseDTO;
           
        }

        public ResponseDTO GetCustomerListForSearchByVLCId(int vlcId)
        {
            ResponseDTO responseDTO = new ResponseDTO();

            List<CustomerSearchDTO> customerList = new List<CustomerSearchDTO>();
            var customers = unitOfWork.CustomerRepository.GetCustomerListForSearchByVLCId(vlcId);
            if (customers != null)
            {
                foreach (var customer in customers)
                {
                    customerList.Add(CustomerConvertor.ConvertToCustomerSearchDto(customer));
                }

            }
            responseDTO.Status = true;
            responseDTO.Message = String.Format("List of Customers By VLC Id");
            responseDTO.Data = customerList;
            return responseDTO;
        }

        public ResponseDTO GetCustomerDetailsByCustomerId(int customerId)
        {
            ResponseDTO responseDTO = new ResponseDTO();
        
            CustomerDetailsDTO customerDetailsDto = new CustomerDetailsDTO() ;
            var customer = unitOfWork.CustomerRepository.GetById(customerId);
            if (customer != null)
            {
                customerDetailsDto.customerProfileDetails = CustomerConvertor.ConvertToCustomerDto(customer);
                if(customer.CustomerBanks!=null)
                {
                    customerDetailsDto.customerBankDetails = CustomerBankConvertor.ConvertTocustomerBankDto(customer.CustomerBanks.FirstOrDefault());
                }
                else
                {
                    customerDetailsDto.customerBankDetails = new CustomerBankDTO();
                }
                if (customer.CustomerAgricultures != null)
                {
                    customerDetailsDto.customerAgricultureDetails = CustomerAgricultureConvertor.ConvertToCustomerAgriCultureDto(customer.CustomerAgricultures.FirstOrDefault());
                }
                else
                {
                    customerDetailsDto.customerAgricultureDetails = new CustomerAgricultureDTO();
                }
                responseDTO.Status = true;
                responseDTO.Message = "Customer Profile Details";
                responseDTO.Data = customerDetailsDto;

            }
            else
            {
                throw new PlatformModuleException("Customer  Details Not Found with given Customer Id");

            }
            return responseDTO;
           
        }

        public List<CustomerDto> GetCustomerDetailsByVLCCode(string vlcCode)
        {
            List<CustomerDto> customerList = new List<CustomerDto>();
            var customers = unitOfWork.CustomerRepository.GetAll().Where(id => id.VLC.VLCCode==vlcCode).ToList();
            if (customers != null)
            {
                foreach (var customer in customers)
                {
                    customerList.Add(CustomerConvertor.ConvertToCustomerDto(customer));
                }

            }

            return customerList;
        }

        public ResponseDTO GetCustomerByCustomerId(int customerId)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            CustomerDto customerDto = null;
            var customer = unitOfWork.CustomerRepository.GetById(customerId);
            if (customer != null)
            {
                customerDto = CustomerConvertor.ConvertToCustomerDto(customer);
                responseDTO.Status = true;
                responseDTO.Message = "Customer Detail By Customer Id";
                responseDTO.Data = customerDto;
            }
            else
            {
                responseDTO.Status = false;
                responseDTO.Message = "No Customer Exist with given customer Id";
                responseDTO.Data = new object();
            }
            return responseDTO;
        }

        

        public ResponseDTO AddCustomer(CustomerDto customerDto)
        {
             this.CheckForExisitngCustomer(customerDto);
            ResponseDTO responseDTO = new ResponseDTO();
            Customer customer = new Customer();
           customer.CustomerId = unitOfWork.DashboardRepository.NextNumberGenerator("Customer");
            var vlc = unitOfWork.VLCRepository.GetById(customerDto.VLCId);
            if (vlc != null)
            {
                CustomerConvertor.ConvertToCustomerEntity(ref customer, customerDto, false);
                customer.CustomerCode = unitOfWork.CustomerRepository.GetCustomerCodeIdByVLC(customerDto.VLCId);
                customer.CreatedDate = DateTimeHelper.GetISTDateTime();
                customer.ModifiedDate = DateTimeHelper.GetISTDateTime();
                customer.CreatedBy = customer.ModifiedBy = vlc.AgentName ?? "System";
                customer.DateOfJoinVLC = DateTimeHelper.GetISTDateTime().Date;
                customer.IsDeleted = false;
                customer.VLCId = customerDto.VLCId;
               unitOfWork.CustomerRepository.Add(customer);
                customerDto = CustomerConvertor.ConvertToCustomerDto(customer);
                unitOfWork.SaveChanges();
                responseDTO.Status = true;
                responseDTO.Message = String.Format("Customer Successfully Created");
                responseDTO.Data = customerDto;
            }
            else
            {
                throw new PlatformModuleException("VLC Details Not Found");
            }
            return responseDTO;


        }

        private void CheckForExisitngCustomer(CustomerDto customerDto)
        {
            var existingCustomer = unitOfWork.CustomerRepository.GetCustomerByMobileNumber(customerDto.Contact);
            if (existingCustomer != null)
                throw new PlatformModuleException("Customer Account Already Exist with given Mobile Number");
             existingCustomer = unitOfWork.CustomerRepository.GetCustomerByEmail( customerDto.Email);
            if (existingCustomer != null)
                throw new PlatformModuleException("Customer Account Already Exist with given Email");


        }

        public ResponseDTO UpdateCustomer(CustomerDto customerDto)
        {

            var customer = unitOfWork.CustomerRepository.GetById(customerDto.CustomerId);
            if(customer ==null)
                throw new PlatformModuleException(string.Format("Customer Account Not Found with Customer Id {0}",customerDto.CustomerId));
            CustomerConvertor.ConvertToCustomerEntity(ref customer, customerDto, true);
           
            customer.ModifiedDate = DateTimeHelper.GetISTDateTime();
            customer.ModifiedBy = unitOfWork.VLCRepository.GetEmployeeNameByVLCId(customer.VLCId.GetValueOrDefault());
          
            unitOfWork.CustomerRepository.Update(customer);
            unitOfWork.SaveChanges();
            ResponseDTO responseDTO = new ResponseDTO();
            responseDTO.Status = true;
            responseDTO.Message = String.Format("Customer Updated Successfully");
            responseDTO.Data = CustomerConvertor.ConvertToCustomerDto(customer);
            return responseDTO;
        }

        public ResponseDTO DeleteCustomer(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            //get customer
            var customer = unitOfWork.CustomerRepository.GetById(id);
            //if((customer.ProductOrders !=null && customer.ProductOrders.Count()>0) || (customer.CustomerWallets !=null && 
            //    customer.CustomerWallets.Count()>0 && customer.CustomerWallets.FirstOrDefault().WalletBalance>0))
            //    {
            //    throw new PlatformModuleException("Customer Account Cannot be deleted as it is associated with orders");
            //}
            unitOfWork.CustomerRepository.Delete(id);
            unitOfWork.SaveChanges();
            ResponseDTO responseDTO = new ResponseDTO();
            responseDTO.Status = true;
            responseDTO.Message = String.Format("Customer Deleted Successfully");
            responseDTO.Data = CustomerConvertor.ConvertToCustomerDto(customer);
            return responseDTO;

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
