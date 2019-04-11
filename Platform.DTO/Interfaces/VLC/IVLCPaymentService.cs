using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public interface IVLCPaymentService
    {
        List<VLCPaymentDTO> GetAllVLCPayments();

        //     List<DCPaymentDTO> GetDistributionCentersByPageCount(int? pageNumber, int? count);


        //    ResponseDTO GetDistributionCenterByVLCId(int vlcId, int? PageNumber);

        ResponseDTO GetAllVLCPayementsByVLCId(int dcId);

        //  List<CustomerDto> GetCustomerDetailsByVLCCode(string vlcCode);

        //   ResponseDTO GetCustomerByCustomerId(int customerId);

        ResponseDTO AddVLCPaymentDetail(VLCPaymentDTO dCPaymentDTO);

        ResponseDTO UpdateVLCPaymentDetail(VLCPaymentDTO dCPaymentDTO);

        ResponseDTO DeleteVLCPaymentDetail(int id);

        ResponseDTO GetVLCPaymentById(int id);

    }
}
