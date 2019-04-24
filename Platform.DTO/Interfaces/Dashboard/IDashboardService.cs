using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO.Interfaces
{
    public interface IDashboardService
    {
        Dictionary<string, decimal> GetDashboardDetails();

        List<TopDCOrderDisplay> GetTopTenOrders();

        List<TopTenVLCWalletBalance> GetTopTenVLCWalletBalance();

        List<TopTenDockCollectionDateWise> GetTopTenDockCollectionDateWise();
    }
}
