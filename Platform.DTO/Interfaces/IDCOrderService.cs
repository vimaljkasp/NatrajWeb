using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public interface IDCOrderService
    {
        List<DCOrderDTO> GetAllDCOrders();

        //     List<DCPaymentDTO> GetDistributionCentersByPageCount(int? pageNumber, int? count);


        //    ResponseDTO GetDistributionCenterByVLCId(int vlcId, int? PageNumber);

        ResponseDTO GetDCOrdersById(int dcId);

        //  List<CustomerDto> GetCustomerDetailsByVLCCode(string vlcCode);

        //   ResponseDTO GetCustomerByCustomerId(int customerId);

        ResponseDTO AddDCOrder(DCOrderDTO dCPaymentDTO);

        ResponseDTO UpdateDCOrder(DCOrderDTO dCPaymentDTO);

        ResponseDTO DeleteDCOrder(int id);

    }
}
