using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public interface IDCAddressService
    {
        List<DCAddressDTO> GetAllDCAddresses();

        //     List<DCPaymentDTO> GetDistributionCentersByPageCount(int? pageNumber, int? count);


        //    ResponseDTO GetDistributionCenterByVLCId(int vlcId, int? PageNumber);

        ResponseDTO GetAllDCAddressByDCId(int dcId);
        ResponseDTO GetDCAddressById(int Id);

        ResponseDTO GetDefaultDCAddressByDCId(int dcId);

        //  List<CustomerDto> GetCustomerDetailsByVLCCode(string vlcCode);

        //   ResponseDTO GetCustomerByCustomerId(int customerId);

        ResponseDTO AddDCAddress(DCAddressDTO dcAddressDTO);

        ResponseDTO UpdateDCAddress(DCAddressDTO dcAddressDTO);

        ResponseDTO DeleteDCAddress(int id);

    }
}
