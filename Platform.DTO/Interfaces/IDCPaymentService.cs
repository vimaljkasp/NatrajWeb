using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public interface IDCPaymentService
    {
        List<DCPaymentDTO> GetAllDCPayments();

   //     List<DCPaymentDTO> GetDistributionCentersByPageCount(int? pageNumber, int? count);


        //    ResponseDTO GetDistributionCenterByVLCId(int vlcId, int? PageNumber);

        ResponseDTO GetAllDCPayementsByDCId(int dcId);

        //  List<CustomerDto> GetCustomerDetailsByVLCCode(string vlcCode);

        //   ResponseDTO GetCustomerByCustomerId(int customerId);

        ResponseDTO AddDCPaymentDetail(DCPaymentDTO dCPaymentDTO);

        ResponseDTO UpdateDCPaymentDetail(DCPaymentDTO dCPaymentDTO);

        ResponseDTO DeleteDCPaymentDetail(int id);

    }
}
