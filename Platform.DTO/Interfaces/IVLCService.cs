
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

        VLCDTO GetVLCById(int vlcId);

        ResponseDTO AddVLC(VLCDTO vlcDto);

        void UpdateVLC(VLCDTO vlcDto);

        void DeleteVLC(int id);






    }
}
