using Platform.DTO;
using Platform.Repository;
using Platform.Sql;
using Platform.Utilities;
using System;
using System.Collections.Generic;


namespace Platform.Service
{
    public class VLCService : IVLCService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();


        public List<VLCDTO> GetAllVLCAgents()
        {
            List<VLCDTO> vlcList = new List<VLCDTO>();
            var vLCs = unitOfWork.VLCRepository.GetAll();
            if (vLCs != null)
            {
                foreach (var vlc in vLCs)
                {
                    vlcList.Add(VLCConvertor.ConvertToVLCDto(vlc));
                }

            }

            return vlcList;

        }




        public List<VLCDTO> GetAllVLCAgentsByPageCount(int? pageNumber, int? count)
        {
            List<VLCDTO> vlcList = new List<VLCDTO>();
            var vlcs = unitOfWork.VLCRepository.GetVLCByCount(pageNumber, count);
            if (vlcs != null)
            {
                foreach (var vlc in vlcs)
                {
                    vlcList.Add(VLCConvertor.ConvertToVLCDto(vlc));
                }

            }

            return vlcList;

        }


        public ResponseDTO GetVLCById(int vlcId)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            VLCDTO vlcDto = null;
            var vlc = unitOfWork.VLCRepository.GetById(vlcId);
            if (vlc != null)
            {
                vlcDto = VLCConvertor.ConvertToVLCDto(vlc);
                responseDTO.Status = true;
                responseDTO.Message = "VLC Details By VLC";
                responseDTO.Data = vlcDto;
            }
            else
            {
                throw new PlatformModuleException("VLC Details Not Found");
            }
            return responseDTO;

        }



        public ResponseDTO AddVLC(VLCDTO vlcDto)
        {
            //  this.CheckForExisitngCustomer(vlcDto.MobileNumber);
            ResponseDTO responseDTO = new ResponseDTO();
            VLC vLC = new VLC();
            vLC.VLCId = unitOfWork.DashboardRepository.NextNumberGenerator("VLC");
            vLC.VLCCode = unitOfWork.DashboardRepository.GenerateVLCCode(vlcDto.VLCName);
            vLC.CreatedDate = DateTimeHelper.GetISTDateTime();
            vLC.ModifiedDate = DateTimeHelper.GetISTDateTime();
            vLC.CreatedBy = vlcDto.ModifiedBy = "Admin";
            vLC.VLCEnrollmentDate = DateTimeHelper.GetISTDateTime().Date;
            vLC.IsDeleted = false;
            vlcDto.Password = vlcDto.Password == null ? "Admin@123" : vlcDto.Password;
            vLC.Password = EncryptionHelper.Encryptword(vlcDto.Password);
            VLCConvertor.ConvertToVLCEntity(ref vLC, vlcDto, false);
            unitOfWork.VLCRepository.Add(vLC);
            AddVLCWallet(vLC);
            responseDTO.Status = true;
            responseDTO.Message = "VLC Succesfully Created";
            responseDTO.Data = VLCConvertor.ConvertToVLCDto(vLC);
            unitOfWork.SaveChanges();
            return responseDTO;
        }

        public void AddVLCWallet(VLC vLC)
        {
            VLCWallet vLCWallet = new VLCWallet();
            vLCWallet.WalletId = unitOfWork.DashboardRepository.NextNumberGenerator("VLCWallet");
            vLCWallet.VLCId = vLC.VLCId;
            vLCWallet.WalletBalance = 0;
            vLCWallet.AmountDueDate = DateTimeHelper.GetISTDateTime().AddDays(10);
            unitOfWork.VLCWalletRepository.Add(vLCWallet);

        }

        public ResponseDTO UpdateVLC(VLCDTO vlcDto)
        {

            var vlc = unitOfWork.VLCRepository.GetById(vlcDto.VLCId);
            VLCConvertor.ConvertToVLCEntity(ref vlc, vlcDto, true);
            vlc.ModifiedBy = vlc.AgentName;
            vlc.ModifiedDate = DateTimeHelper.GetISTDateTime();
            unitOfWork.VLCRepository.Update(vlc);
            unitOfWork.SaveChanges();
            ResponseDTO responseDTO = new ResponseDTO();
            responseDTO.Status = true;
            responseDTO.Message = "VLC Succesfully Updated";
            responseDTO.Data = VLCConvertor.ConvertToVLCDto(vlc);
            return responseDTO;
        }

        public ResponseDTO DeleteVLC(int id)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                UnitOfWork unitOfWork = new UnitOfWork();


                var vlc = unitOfWork.VLCRepository.GetById(id);
                unitOfWork.VLCRepository.Delete(id);
                if (unitOfWork.SaveChanges() > 0)
                {
                    //Deleted 
                    response.Status = true;
                    response.Message = "VLC Deleted Successfully.";
                }
                else
                {
                    //Not deleted
                    response.Status = false;
                    response.Message = "You can not Delete this VLC. PLease check with Admin for more details.";
                }


            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }
            return response;
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

        public ResponseDTO GetVLCCollectionSummary(int vlcId)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            responseDTO.Data = unitOfWork.VLCReportRepository.GetCollectionSummaryReportByVLC(vlcId);
            responseDTO.Status = true;
            responseDTO.Message = "VLC Collection Summary Report";
            return responseDTO;
        }

        public ResponseDTO GetCustomerCollectionSummary(int customerId)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            responseDTO.Data = unitOfWork.VLCReportRepository.GetCollectionSummaryReportByCustomer(customerId);
            responseDTO.Status = true;
            responseDTO.Message = "Customer Collection Summary Report";
            return responseDTO;
        }


        public ResponseDTO GetVLCWalletDetailsByVLCId(int vlcId)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            var vLCWallet = unitOfWork.VLCWalletRepository.GetByVLCId(vlcId);
            if (vLCWallet != null)
            {
                VLCConvertor.ConvertToVLCWalletDTO(vLCWallet);
                responseDTO.Status = true;
                responseDTO.Message = "Customer Collection Summary Report";
                return responseDTO;
            }
            else
            {
                throw new PlatformModuleException("VLC Wallet Not found for given VLC Id");
            }
        }


    }
}
