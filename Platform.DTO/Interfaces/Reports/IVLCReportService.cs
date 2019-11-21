using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public interface IVLCReportService
    {
        ResponseDTO VLCPaymentSummaryByDate(int vlcId, DateTime StartDate, DateTime EndDate);

        ResponseDTO VLCWalletSummary();

        ResponseDTO GetVLCByNameContactnCity(string VLCName, string Contact, string City);
    }
}
