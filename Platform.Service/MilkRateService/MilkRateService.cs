using Platform.DTO.Interfaces.MilkRate;
using Platform.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service
{
    public class MilkRateService : IMilkRateService, IDisposable
    {

        private UnitOfWork unitOfWork = new UnitOfWork();
        public decimal GetMilkRateByVLCAndFAT_CLR(int VLCId, decimal fat, decimal clr)
        {
            int applicableRate = unitOfWork.VLCRepository.GetById(VLCId).ApplicableRate;
            decimal rateOfMilk = unitOfWork.MilkRateRepository.GetMilkRateByApplicableRate(applicableRate, fat, clr);
            return rateOfMilk;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
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
    }
}
