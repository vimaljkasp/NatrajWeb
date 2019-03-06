
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public interface IVLCService
    {
         List<VLCDTO> GetAllVLCAgents();

        List<VLCDTO> GetAllVLCAgentsByPageCount(int? pageNumber, int? count);

        ResponseDTO GetVLCById(int vlcId);

        ResponseDTO GetVLCCollectionSummary(int vlcId);

        ResponseDTO GetCustomerCollectionSummary(int customerId);
            
        ResponseDTO AddVLC(VLCDTO vlcDto);

        ResponseDTO UpdateVLC(VLCDTO vlcDto);

        void DeleteVLC(int id);






    }
}
