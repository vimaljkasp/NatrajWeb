
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
   public interface IVLCMilkCollectionService
    {
        List<VLCMilkCollectionDTO> GetAllVLCMilkCollection();

        List<VLCMilkCollectionDTO> GetAllVLCMilkCollectionByPageCount(int? pageNumber, int? count);

        VLCMilkCollectionDTO GetVLCMilkCollectionById(int customerId);

       ResponseDTO GetVLCCustomerCollectionsByDateAndShift(int vlcId, DateTime collectionDate,int shift,int? PageNumber);

        ResponseDTO AddVLCMilkCollection(VLCMilkCollectionDTO vlcMilkCollectionDto);

        ResponseDTO UpdateVLCMilkCollection(VLCMilkCollectionDTO vlcMilkCollectionDto);

        ResponseDTO DeleteVLCMilkCollection(int id);



        ResponseDTO DeleteVLCMilkCollectionDtl(int id);
    }
}
