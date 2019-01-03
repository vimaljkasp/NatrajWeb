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
    public class CustomerAgricultureService : ICustomerAgricultureService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();


        public List<CustomerAgricultureDTO> GetAllCustomerAgriCultures()
        {
            List<CustomerAgricultureDTO> customerAgricultureList = new List<CustomerAgricultureDTO>();
            var customerAgricultures = unitOfWork.CustomerAgricultureRepository.GetAll();
            if (customerAgricultures != null)
            {
                foreach (var customerAgriculture in customerAgricultures)
                {
                    customerAgricultureList.Add(CustomerAgricultureConvertor.ConvertToCustomerAgriCultureDto(customerAgriculture));
                }

            }

            return customerAgricultureList;

        }

        public List<CustomerAgricultureDTO> GetCustomerCustomerAgriCulturesByPageCount(int? pageNumber, int? count)
        {
            List<CustomerAgricultureDTO> customerAgricultureList = new List<CustomerAgricultureDTO>();
            var customerAgricultures = unitOfWork.CustomerAgricultureRepository.GetCustomerByCount(pageNumber, count);
            if (customerAgricultures != null)
            {
                foreach (var customerAgriculture in customerAgricultures)
                {
                    customerAgricultureList.Add(CustomerAgricultureConvertor.ConvertToCustomerAgriCultureDto(customerAgriculture));
                }

            }

            return customerAgricultureList;

        }


        public ResponseDTO GetCustomerAgriCultureById(int customerId)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            CustomerAgricultureDTO customerAgricultureDTO = null;
            var customerAgriculture = unitOfWork.CustomerAgricultureRepository.GetByCustomerId(customerId);
            if (customerAgriculture != null)
            {
                customerAgricultureDTO = CustomerAgricultureConvertor.ConvertToCustomerAgriCultureDto(customerAgriculture);
                responseDTO.Status = true;
                responseDTO.Message = "Customer Agriculture Details Found";
                responseDTO.Data = customerAgricultureDTO;
            }
            else
            {
                responseDTO.Status = false;
                responseDTO.Message = String.Format("Customer Agriculture Details Not Found For Customer Id {0}", customerId);
                responseDTO.Data = new object();
            }
            return responseDTO;
        }



        public ResponseDTO AddCustomerAgriculture(CustomerAgricultureDTO customerAgricultureDTO)
        {
            ResponseDTO responseDTO = new ResponseDTO();
             this.CheckForExisitngCustomerAgriculture(customerAgricultureDTO.CustomerId);
            CustomerAgriculture customerAgriculture = new CustomerAgriculture();
            customerAgriculture.CustAgriId = unitOfWork.DashboardRepository.NextNumberGenerator("CustomerAgriculture");
           
            customerAgriculture.CreatedDate = DateTime.Now;
            customerAgriculture.IsDeleted = false;
            customerAgriculture.ModifiedBy = customerAgriculture.CreatedBy = unitOfWork.VLCRepository.GetEmployeeNameByVLCId(customerAgricultureDTO.VLCId);
            customerAgriculture.ModifiedDate = DateTime.Now;

            CustomerAgricultureConvertor.ConvertToCustomerAgriCultureEntity(ref customerAgriculture, customerAgricultureDTO, false);
            unitOfWork.CustomerAgricultureRepository.Add(customerAgriculture);
         
            unitOfWork.SaveChanges();
            customerAgricultureDTO= CustomerAgricultureConvertor.ConvertToCustomerAgriCultureDto(customerAgriculture);
            responseDTO.Status = true;
            responseDTO.Message = "Customer Agriculture Detail Added Successfully";
            responseDTO.Data = customerAgricultureDTO;
            return responseDTO;


        }

        public void CheckForExisitngCustomerAgriculture(int customerId)
        {
            var customerAgri = unitOfWork.CustomerAgricultureRepository.GetByCustomerId(customerId);
            if(customerAgri !=null)
                throw new PlatformModuleException("Customer Agriculture Details Already Exist with given Customer");

        }


        public ResponseDTO UpdateCustomerAgriculture(CustomerAgricultureDTO customerAgricultureDTO)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            var customerAgriculture = unitOfWork.CustomerAgricultureRepository.GetByCustomerId(customerAgricultureDTO.CustomerId);
            if (customerAgriculture == null)
                throw new PlatformModuleException(string.Format("Customer Agriculture Details Not Found with Customer Id {0}", customerAgriculture.CustomerId));

            CustomerAgricultureConvertor.ConvertToCustomerAgriCultureEntity(ref customerAgriculture, customerAgricultureDTO, true);
         //   customerAgriculture.ModifiedBy  = unitOfWork.VLCRepository.GetEmployeeNameByVLCId(customerAgriculture.Customer.VLCId.GetValueOrDefault());
            customerAgriculture.ModifiedDate = DateTime.Now;
            unitOfWork.CustomerAgricultureRepository.Update(customerAgriculture);
            unitOfWork.SaveChanges();
            customerAgricultureDTO = CustomerAgricultureConvertor.ConvertToCustomerAgriCultureDto(customerAgriculture);
            responseDTO.Status = true;
            responseDTO.Message = "Customer Agriculture Detail Updated Successfully";
            responseDTO.Data = customerAgricultureDTO;
            return responseDTO;
        }

        public ResponseDTO DeleteCustomerAgriculture(int id)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            UnitOfWork unitOfWork = new UnitOfWork();
            //get customerAgriculture
            var customerAgriculture = unitOfWork.CustomerAgricultureRepository.GetByCustomerId(id);
            customerAgriculture.IsDeleted = true;
            unitOfWork.CustomerAgricultureRepository.Update(customerAgriculture);
            unitOfWork.SaveChanges();
            CustomerAgricultureDTO customerAgricultureDTO = CustomerAgricultureConvertor.ConvertToCustomerAgriCultureDto(customerAgriculture);
            responseDTO.Status = true;
            responseDTO.Message = "Customer Agriculture Detail Deleted Successfully";
            responseDTO.Data = customerAgricultureDTO;
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
