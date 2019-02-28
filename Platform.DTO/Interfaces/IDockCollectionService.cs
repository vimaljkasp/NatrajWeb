
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public interface IDockMilkCollectionService
    {
        List<DockMilkCollectionDTO> GetAllDockMilkMilkCollection();

        List<DockMilkCollectionDTO> GetAllDockMilkMilkCollectionByPageCount(int? pageNumber, int? count);

        DockMilkCollectionDTO GetDockMilkMilkCollectionById(int vlcId);

        ResponseDTO GetDockMilkCollectionsByDateAndShift(int DockMilkCollectionId, DateTime collectionDate, int shift, int? PageNumber);

        ResponseDTO AddDockMilkMilkCollection(DockMilkCollectionDTO DockMilkCollectionDTO);

        ResponseDTO UpdateDockMilkMilkCollection(DockMilkCollectionDTO DockMilkCollectionDTO);

        ResponseDTO DeleteDockMilkMilkCollection(int id);

    }
}
