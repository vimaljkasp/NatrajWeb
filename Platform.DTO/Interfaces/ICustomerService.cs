
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public interface ICustomerService
    {
         List<CustomerDto> GetAllCustomers();

        List<CustomerDto> GetCustomerByPageCount(int? pageNumber, int? count);


        ResponseDTO GetCustomerListByVLCId(int vlcId,int? PageNumber);

        ResponseDTO GetCustomerDetailsByCustomerId(int customerId);

        List<CustomerDto> GetCustomerDetailsByVLCCode(string vlcCode);

        ResponseDTO GetCustomerByCustomerId(int customerId);

        ResponseDTO AddCustomer(CustomerDto customerDto);

        ResponseDTO UpdateCustomer(CustomerDto customerDto);

        ResponseDTO DeleteCustomer(int id);

    }
}
