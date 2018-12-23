using Platform.DTO;
using Platform.Repository;
using Platform.Sql;
using Platform.Utilities.ExceptionHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service
{
    public class CustomerBankService : ICustomerBankService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();


        public List<CustomerBankDTO> GetAllCustomerBanks()
        {
            List<CustomerBankDTO> customerList = new List<CustomerBankDTO>();
            var customerBanks = unitOfWork.CustomerBankRepository.GetAll();
            if (customerBanks != null)
            {
                foreach (var customerBank in customerBanks)
                {
                    customerList.Add(CustomerBankConvertor.ConvertTocustomerBankDto(customerBank));
                }

            }

            return customerList;

        }

        public List<CustomerBankDTO> GetCustomerBankByPageCount(int? pageNumber, int? count)
        {
            List<CustomerBankDTO> customerList = new List<CustomerBankDTO>();
            var customerBanks = unitOfWork.CustomerBankRepository.GetCustomerByCount(pageNumber, count);
            if (customerBanks != null)
            {
                foreach (var customerBank in customerBanks)
                {
                    customerList.Add(CustomerBankConvertor.ConvertTocustomerBankDto(customerBank));
                }

            }

            return customerList;

        }


        public ResponseDTO GetCustomerBankById(int customerId)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            CustomerBankDTO customerBankDto = null;
            var customerBank = unitOfWork.CustomerBankRepository.GetByCustomerId(customerId);
            if (customerBank != null)
            {
                customerBankDto = CustomerBankConvertor.ConvertTocustomerBankDto(customerBank);
                responseDTO.Status = true;
                responseDTO.Message = "Customer Bank Details For Customer";
                responseDTO.Data = customerBankDto;
            }
            else
            {
                responseDTO.Status = false;
                responseDTO.Message = String.Format("Customer Bank Details with Customer ID {0} not found", customerId);
                responseDTO.Data = new object();
            }
            return responseDTO;
        }



        public ResponseDTO AddCustomerBank(CustomerBankDTO customerBankDto)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            CustomerBank customerBank = new CustomerBank();
            customerBank.CustomerBankId = unitOfWork.DashboardRepository.NextNumberGenerator("CustomerBank");
            CustomerBankConvertor.ConvertToCustomerBankEntity(ref customerBank, customerBankDto, false);
            customerBank.CreatedBy = "Vimal";
            customerBank.CreatedDate = DateTime.Now;
            customerBank.IsDeleted = false;
            customerBank.ModifiedBy = "Vimal";
            customerBank.ModifiedDate = DateTime.Now;
            unitOfWork.CustomerBankRepository.Add(customerBank);
            unitOfWork.SaveChanges();
            customerBankDto = CustomerBankConvertor.ConvertTocustomerBankDto(customerBank);
            responseDTO.Status = true;
            responseDTO.Message = "Customer Bank Added Successfully";
            responseDTO.Data = customerBankDto;
            return responseDTO;
        }


        public void CheckForExisitngCustomerBank(int customerId)
        {
            var customerBank = unitOfWork.CustomerBankRepository.GetByCustomerId(customerId);
            if (customerBank != null)
                throw new PlatformModuleException("Customer Bank Details Already Exist with given Customer");

        }

        public ResponseDTO UpdateCustomerBank(CustomerBankDTO customerBankDto)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            var customerBank = unitOfWork.CustomerBankRepository.GetByCustomerId(customerBankDto.CustomerBankId);
            CustomerBankConvertor.ConvertToCustomerBankEntity(ref customerBank, customerBankDto, true);

            unitOfWork.CustomerBankRepository.Update(customerBank);
            unitOfWork.SaveChanges();
            CustomerBankConvertor.ConvertTocustomerBankDto(customerBank);
            responseDTO.Status = true;
            responseDTO.Message = "Customer Bank Updated Successfully";
            responseDTO.Data = customerBankDto;
            return responseDTO;
        }

        public ResponseDTO DeleteCustomerBank(int id)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            UnitOfWork unitOfWork = new UnitOfWork();
            //get customerBank
            var customerBank = unitOfWork.CustomerBankRepository.GetByCustomerId(id);
            //if((customerBank.ProductOrders !=null && customerBank.ProductOrders.Count()>0) || (customerBank.CustomerWallets !=null && 
            //    customerBank.CustomerWallets.Count()>0 && customerBank.CustomerWallets.FirstOrDefault().WalletBalance>0))
            //    {
            //    throw new PlatformModuleException("CustomerBank Account Cannot be deleted as it is associated with orders");
            //}
            unitOfWork.CustomerBankRepository.Delete(id);
            unitOfWork.SaveChanges();
            CustomerBankDTO customerBankDTO = CustomerBankConvertor.ConvertTocustomerBankDto(customerBank);
            responseDTO.Status = true;
            responseDTO.Message = "Customer Bank Deleted Successfully";
            responseDTO.Data = customerBankDTO;
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
