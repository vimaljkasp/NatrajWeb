using Platform.DTO;
using Platform.DTO.ReadDTO.ReportDTO;
using Platform.Repository;
using Platform.Sql;
using Platform.Utilities;
using System;
using System.Collections.Generic;


namespace Platform.Service
{

    public class VLCReportService : IVLCReportService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public ResponseDTO VLCPaymentSummaryByDate(int vlcId, DateTime StartDate, DateTime EndDate)
        {

            ResponseDTO responseDTO = new ResponseDTO();
            var list = unitOfWork.VLCReportRepository.VLCPaymentSummaryByDate(vlcId, StartDate, EndDate);
            list.VLCName = this.GetVLCName(vlcId);
            responseDTO.Data = list;
            responseDTO.Status = true;
            responseDTO.Message = "VLC Payment Summary By Date";
            return responseDTO;
        }


        public ResponseDTO VLCWalletSummary()
        {
            ResponseDTO responseDTO = new ResponseDTO();
            List<VLCWalletDTO> vLCWalletDTOList = new List<VLCWalletDTO>();
            var vLCWallets = unitOfWork.VLCWalletRepository.GetAll();
            if (vLCWallets != null)
            {
                foreach (var vlcWallet in vLCWallets)
                {
                    vLCWalletDTOList.Add(VLCConvertor.ConvertToVLCWalletDTO(vlcWallet));

                }
                responseDTO.Data = vLCWalletDTOList;
                responseDTO.Status = true;
                responseDTO.Message = "VLC Wallet Summary Report";
                return responseDTO;
            }
            else
            {
                throw new PlatformModuleException("No VLC Wallet Not Found");
            }


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

        public ResponseDTO GetVLCByNameContactnCity(string VLCName, string Contact, string City)
        {

            ResponseDTO response = new ResponseDTO();

            try
            {
                List<VLC> VLCs = unitOfWork.VLCRepository.GetAll();

                if (!string.IsNullOrEmpty(VLCName))
                {
                    VLCs = VLCs.FindAll(x => x.VLCName.ToUpper().Trim().Contains(VLCName.ToUpper().Trim()));
                }

                if (!string.IsNullOrEmpty(City))
                {
                    VLCs = VLCs.FindAll(x => x.City.ToUpper().Trim().Contains(City.ToUpper().Trim())
                    || x.Village.ToUpper().Trim().Contains(City.ToUpper().Trim()));
                }

                if (!string.IsNullOrEmpty(Contact))
                {
                    VLCs = VLCs.FindAll(x => x.Contact.ToUpper().Trim().Contains(Contact.ToUpper().Trim()));
                }

                if (!string.IsNullOrEmpty(VLCName) && !string.IsNullOrEmpty(Contact) && !string.IsNullOrEmpty(City))
                {
                    VLCs = VLCs.FindAll(x => x.VLCName.ToUpper().Trim().Contains(VLCName.ToUpper().Trim())
                     || x.City.ToUpper().Trim().Contains(City.ToUpper().Trim())
                     || x.Contact.ToUpper().Trim().Contains(Contact.ToUpper().Trim())
                    );
                }

                VLCReportByNameAndCityContactDTO report = new VLCReportByNameAndCityContactDTO();
                report.VLCs = new List<VLCDTO>(); 
                foreach (var vlc in VLCs)
                {
                    report.VLCs.Add(VLCConvertor.ConvertToVLCDto(vlc));
                }
                report.VLCName = VLCName;
                report.City = City;
                report.Contact = Contact;

                response.Status = true;
                response.Data = report;
            }
            catch
            {
                response.Status = false;
            }

            return response;
        }
    }
}
