
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public interface IDockMilkCollectionService
    {
        List<DockMilkCollectionDTO> GetAllDockMilkCollection();

        List<DockMilkCollectionDTO> GetAllDockMilkCollectionByPageCount(int? pageNumber, int? count);

        DockMilkCollectionDTO GetDockMilkCollectionById(int vlcId);
        

        ResponseDTO GetDockMilkCollectionsByDateAndShift(int DockMilkCollectionId, DateTime collectionDate, int shift, int? PageNumber);

        ResponseDTO AddDockMilkCollection(DockMilkCollectionDTO DockMilkCollectionDTO);

        ResponseDTO UpdateDockMilkCollection(DockMilkCollectionDTO DockMilkCollectionDTO);

        ResponseDTO DeleteDockMilkCollection(int id);

    }
}
