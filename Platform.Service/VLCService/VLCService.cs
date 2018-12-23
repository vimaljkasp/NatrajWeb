using Platform.DTO;
using Platform.Repository;
using Platform.Sql;
using Platform.Utilities.Encryption;
using Platform.Utilities.ExceptionHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service
{
    public class VLCService : IVLCService,IDisposable
    {
        private  UnitOfWork unitOfWork=new UnitOfWork();
   

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
            var vlcs = unitOfWork.VLCRepository.GetVLCByCount(pageNumber,count);
            if (vlcs != null)
            {
                foreach (var vlc in vlcs)
                {
                    vlcList.Add(VLCConvertor.ConvertToVLCDto(vlc));
                }

            }

            return vlcList;

        }


        public VLCDTO GetVLCById(int vlcId)
        {
            VLCDTO vlcDto = null;
            var vlc = unitOfWork.VLCRepository.GetById(vlcId);
            if (vlc != null)
            {
                vlcDto = VLCConvertor.ConvertToVLCDto(vlc);
            }
            return vlcDto;
        }

        

        public ResponseDTO AddVLC(VLCDTO vlcDto)
        {
            //  this.CheckForExisitngCustomer(vlcDto.MobileNumber);
            ResponseDTO responseDTO = new ResponseDTO();
            VLC vLC = new VLC();
            vLC.VLCId = unitOfWork.DashboardRepository.NextNumberGenerator("VLC");
            vlcDto.CreatedDate = DateTime.Now.Date;
            vlcDto.ModifiedDate = DateTime.Now.Date;
            vlcDto.CreatedBy = vlcDto.ModifiedBy = "Vimal";
            vlcDto.VLCEnrollmentDate = DateTime.Now.Date;
            vlcDto.IsDeleted = false;
            vlcDto.Password = EncryptionHelper.Encryptword(vlcDto.Password);
            VLCConvertor.ConvertToVLCEntity(ref vLC, vlcDto, false);
            unitOfWork.VLCRepository.Add(vLC);
            //creating customer wallet with customer 
            //CustomerWallet customerWallet = new CustomerWallet();
            //customerWallet.WalletId = unitOfWork.DashboardRepository.NextNumberGenerator("CustomerWallet");
            //customerWallet.CustomerId = customer.CustomerId;
            //customerWallet.WalletBalance = 0;
            //customerWallet.AmountDueDate = DateTime.Now.AddDays(10);
            //unitOfWork.CustomerWalletRepository.Add(customerWallet);
            responseDTO.Status = true;
            responseDTO.Message = "VLC Succesfully Created";
            responseDTO.Data = null;
            unitOfWork.SaveChanges();
            return responseDTO;
            
        }

    

        public void UpdateVLC(VLCDTO vlcDto)
        {

            var vlc = unitOfWork.VLCRepository.GetById(vlcDto.VLCId);
            VLCConvertor.ConvertToVLCEntity(ref vlc, vlcDto, true);
           
            unitOfWork.VLCRepository.Update(vlc);
            unitOfWork.SaveChanges();
        }

        public void DeleteVLC(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            //get customer
            var vlc = unitOfWork.VLCRepository.GetById(id);
            //if((customer.ProductOrders !=null && customer.ProductOrders.Count()>0) || (customer.CustomerWallets !=null && 
            //    customer.CustomerWallets.Count()>0 && customer.CustomerWallets.FirstOrDefault().WalletBalance>0))
            //    {
            //    throw new PlatformModuleException("Customer Account Cannot be deleted as it is associated with orders");
            //}
            unitOfWork.VLCRepository.Delete(id);
            unitOfWork.SaveChanges();
  
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
