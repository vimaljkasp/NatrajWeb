using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO.Interfaces
{
    public interface IVLCReportService
    {
        ResponseDTO VLCPaymentSummaryByDate(int vlcId, DateTime StartDate, DateTime EndDate);
    }
}
