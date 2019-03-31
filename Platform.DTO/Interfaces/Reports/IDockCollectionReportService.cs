using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
   public  interface IDockCollectionReportService
    {
         ResponseDTO DockCollectionSummaryByDate(DateTime collectionStartDate, DateTime collectionEndDate, int startShift, int endShift, int milkType);

        ResponseDTO DockCollectionSummaryByVLC(int vlcId, DateTime collectionStartDate, DateTime collectionEndDate, int startShift, int endShift, int milkType);

   //     ResponseDTO DockCollectionSummaryDetailByVLC(int vlcId, DateTime collectionStartDate, DateTime collectionEndDate);




    }
}
