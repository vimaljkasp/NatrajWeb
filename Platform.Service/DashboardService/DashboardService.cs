using Platform.DTO;
using Platform.DTO.Interfaces;
using Platform.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service
{
    public class DashboardService : IDashboardService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public DashboardService(LoggerService loggerService)
        {

        }

        public Dictionary<string, decimal> GetDashboardDetails()
        {
            Dictionary<string, decimal> dictionary = unitOfWork.DashboardRepository.GetDashboardDetails();
            return dictionary;
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (unitOfWork != null)
                {
                    unitOfWork.Dispose();
                    unitOfWork = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public List<TopDCOrderDisplay> GetTopTenOrders()
        {
            List<TopDCOrderDisplay> TopTenOrders = unitOfWork.DashboardRepository.GetTopTenOrders();
            return TopTenOrders;
        }

        public List<TopTenVLCWalletBalance> GetTopTenVLCWalletBalance()
        {
            List<TopTenVLCWalletBalance> TopTenVLCWalletBalances = unitOfWork.DashboardRepository.GetTopTenVLCWalletBalance();
            return TopTenVLCWalletBalances;
        }

        public List<TopTenDockCollectionDateWise> GetTopTenDockCollectionDateWise()
        {
            List<TopTenDockCollectionDateWise> TopTenDockCollections = unitOfWork.DashboardRepository.GetTopTenDockCollectionDateWise();
            return TopTenDockCollections;
        }
    }
}
