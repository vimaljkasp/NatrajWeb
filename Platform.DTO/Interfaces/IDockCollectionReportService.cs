using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
   public  interface IDockCollectionReportService
    {
         ResponseDTO DockCollectionSummaryByDate(DateTime collectionStartDate, DateTime collectionEndDate);

        ResponseDTO DockCollectionSummaryByVLC(int vlcId, DateTime collectionStartDate, DateTime collectionEndDate);

        ResponseDTO DockCollectionSummaryDetailByVLC(int vlcId, DateTime collectionStartDate, DateTime collectionEndDate);




    }
}
