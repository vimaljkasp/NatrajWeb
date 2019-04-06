
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public interface IDistributionCenterService
    {
        ResponseDTO GetAllDistributionCenters();

      //  List<DistributionCenterDTO> GetDistributionCentersByPageCount(int? pageNumber, int? count);


    //    ResponseDTO GetDistributionCenterByVLCId(int vlcId, int? PageNumber);

        ResponseDTO GetDistributionCenterByCenterId(int dcId);

        ResponseDTO GetDistributionCentersByCity(string city,int? pageNumber);

      //  List<CustomerDto> GetCustomerDetailsByVLCCode(string vlcCode);

     //   ResponseDTO GetCustomerByCustomerId(int customerId);

        ResponseDTO AddDistributionCenter(DistributionCenterDTO distributionCenterDTO);

        ResponseDTO UpdateDistributionCenter(DistributionCenterDTO distributionCenterDTO);

        ResponseDTO UpdateDistributionCenterStatus(int dcId,bool status);

        ResponseDTO DeleteDistriubtionCenter(int id);

        ResponseDTO GetDCWalletBalance(int dcId);

    }
}
