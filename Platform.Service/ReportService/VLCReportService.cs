using Platform.DTO;
using Platform.Repository;
using Platform.Sql;
using Platform.Utilities;
using System;
using System.Collections.Generic;


namespace Platform.Service
{

    public class VLCReportService :  IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public ResponseDTO VLCPaymentSummaryByDate(int vlcId,DateTime StartDate, DateTime EndDate)
        {

            ResponseDTO responseDTO = new ResponseDTO();
            responseDTO.Data = unitOfWork.VLCReportRepository.VLCPaymentSummaryByDate(vlcId,StartDate, EndDate);
            responseDTO.Status = true;
            responseDTO.Message = "Dock Collection Summary Report By Date";
            return responseDTO;
        }






        private string GetVLCName(int vlcId)
        {
            var vlc = unitOfWork.VLCRepository.GetById(vlcId);
            if (vlc != null)
                return vlc.VLCName;
            else
                throw new PlatformModuleException("Given VLC does not exist");
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




    }
}
