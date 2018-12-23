
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
     public interface ICustomerAgricultureService
    {
        List<CustomerAgricultureDTO> GetAllCustomerAgriCultures();

        List<CustomerAgricultureDTO> GetCustomerCustomerAgriCulturesByPageCount(int? pageNumber, int? count);

        ResponseDTO GetCustomerAgriCultureById(int customerAgricultureId);

        ResponseDTO AddCustomerAgriculture(CustomerAgricultureDTO customerAgricultureDto);

        ResponseDTO UpdateCustomerAgriculture(CustomerAgricultureDTO customerAgricultureDto);

        ResponseDTO DeleteCustomerAgriculture(int id);
    }
}
