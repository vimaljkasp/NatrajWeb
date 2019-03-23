
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public interface ICustomerBankService
    {
        List<CustomerBankDTO> GetAllCustomerBanks();

        List<CustomerBankDTO> GetCustomerBankByPageCount(int? pageNumber, int? count);

        ResponseDTO GetCustomerBankById(int customerId);

        ResponseDTO AddCustomerBank(CustomerBankDTO customerDto);

        ResponseDTO UpdateCustomerBank(CustomerBankDTO customerDto);

        ResponseDTO DeleteCustomerBank(int id);
    }
}
