using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Platform.DTO;
using Platform.Repository;
using Platform.Sql;
using Platform.Utilities;


namespace Platform.Service
{
    public class DockMilkCollectionService : IDockMilkCollectionService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public List<DockMilkCollectionDTO> GetAllDockMilkCollection()
        {
            List<DockMilkCollectionDTO> dockMilkCollectionList = new List<DockMilkCollectionDTO>();
            var dockMilkCollections = unitOfWork.DockMilkCollectionRepository.GetAll();
            if (dockMilkCollections != null)
            {
                foreach (var dockMilkCollection in dockMilkCollections)
                {
                    dockMilkCollectionList.Add(DockMilkCollectionConvertor.ConvertToDockMilkCollectionDto(dockMilkCollection));
                }

            }

            return dockMilkCollectionList;

        }

        public List<DockMilkCollectionDTO> GetAllDockMilkCollectionByPageCount(int? pageNumber, int? count)
        {
            List<DockMilkCollectionDTO> DockMilkCollectionList = new List<DockMilkCollectionDTO>();
            var DockMilkCollections = unitOfWork.DockMilkCollectionRepository.GetDockMilkCollectionByCount(pageNumber, count);
            if (DockMilkCollections != null)
            {
                foreach (var dock in DockMilkCollections)
                {
                    DockMilkCollectionList.Add(DockMilkCollectionConvertor.ConvertToDockMilkCollectionDto(dock));
                }

            }

            return DockMilkCollectionList;

        }

        public List<DockVLCCollectionDTO> GetDockCollectionByDateAndShift(int dockMilkCollectionId, DateTime collectionDate, int shift, int? pageNumber)
        {
            List<DockVLCCollectionDTO> dockVLCCollectionList = new List<DockVLCCollectionDTO>();

            var dockMilkCollections = unitOfWork.DockMilkCollectionRepository.GetDockCollectionByDateShift(dockMilkCollectionId, collectionDate, shift, pageNumber);
            if (dockMilkCollections != null)
            {
                foreach (var dockmilkCollection in dockMilkCollections)
                {
                  //  vlcCustomerMilkCollection.Add(DockMilkCollectionConvertor.ConvertToVLCCustomerCollectionDTO(vlcMilk));
                }

            }

            return dockVLCCollectionList;
        }

        public ResponseDTO GetDockMilkCollectionsByDateAndShift(int dockMilkCollectionId, DateTime collectionDate, int shift, int? pageNumber)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            responseDTO.Status = true;
            responseDTO.Message = "Dock  Collections";
            responseDTO.Data = this.GetDockCollectionByDateAndShift(dockMilkCollectionId, collectionDate, shift, pageNumber);
            return responseDTO;
        }

        public DockMilkCollectionDTO GetDockMilkCollectionById(int dockCollectionId)
        {
            DockMilkCollectionDTO dockMilkCollectionDTO = new DockMilkCollectionDTO();
            var dockMilkCollection = unitOfWork.DockMilkCollectionRepository.GetById(dockCollectionId);
            if (dockMilkCollection != null)
            {
             //   vlcMilkCollectionDto = DockMilkCollectionConvertor.ConvertToDockMilkCollectionDto(vlcMilkCollection);
            }
            return dockMilkCollectionDTO;
        }

        

  

        public NatrajSMSLog SendSMS(string mobileNumber, string message)
        {
            NatrajSMSLog natrajSMSLog = new NatrajSMSLog();
            natrajSMSLog.SMSId = unitOfWork.DashboardRepository.NextNumberGenerator("NatrajSMSLog");
            SMSConvertor.ConvertToSMSMessage(ref natrajSMSLog, NatrajComponent.CRM, SMSType.DockMilkCollection, mobileNumber, message);

            return natrajSMSLog;
        }

        public ResponseDTO AddDockMilkCollection(DockMilkCollectionDTO dockMilkCollectionDTO)
        {
            var vlc = unitOfWork.VLCRepository.GetById(dockMilkCollectionDTO.VLCId);
            if (vlc != null)
            {
                ResponseDTO responseDTO = new ResponseDTO();
                DockMilkCollection dockMilkCollection = new DockMilkCollection();
                dockMilkCollection.DockMilkCollectionId = unitOfWork.DashboardRepository.NextNumberGenerator("DockMilkCollection");
                dockMilkCollection.CollectionDateTime = DateTimeHelper.GetISTDateTime();
                dockMilkCollection.CreatedDate = DateTimeHelper.GetISTDateTime();
                dockMilkCollection.ModifiedDate = DateTimeHelper.GetISTDateTime();
                dockMilkCollection.CreatedBy = dockMilkCollectionDTO.ModifiedBy = "Admin";
                dockMilkCollection.IsDeleted = false;
                DockMilkCollectionConvertor.ConvertToDockMilkCollectionEntity(ref dockMilkCollection, dockMilkCollectionDTO, false);
                if (dockMilkCollectionDTO.dockMilkCollectionList != null)
                {
                    foreach (var dockMilkCollectionDtlDto in dockMilkCollectionDTO.dockMilkCollectionList)

                    {
                        this.CheckForExistingCollectionDetailByDateShiftProduct(dockMilkCollection.CollectionDateTime.Date, dockMilkCollection.ShiftId, dockMilkCollectionDtlDto.ProductId, dockMilkCollection.VLCId);
                        DockMilkCollectionDtl dockMilkCollectionDtl = new DockMilkCollectionDtl();
                        dockMilkCollectionDtl.DockMilkCollectionDtlI = unitOfWork.DashboardRepository.NextNumberGenerator("DockMilkCollectionDtl");
                        dockMilkCollectionDtl.DockMilkCollectionId = dockMilkCollection.DockMilkCollectionId;
                        DockMilkCollectionConvertor.ConvertToDockMilkCollectionDtlEntity(ref dockMilkCollectionDtl, dockMilkCollectionDtlDto, false);
                        dockMilkCollectionDtl.RatePerUnit = unitOfWork.MilkRateRepository.GetMilkRateByApplicableRate(vlc.ApplicableRate, dockMilkCollectionDtlDto.FAT.GetValueOrDefault(), dockMilkCollectionDtlDto.CLR.GetValueOrDefault());
                        dockMilkCollectionDtlDto.TotalAmount = dockMilkCollectionDtl.TotalAmount = dockMilkCollectionDtl.RatePerUnit * dockMilkCollectionDtlDto.Quantity;
                                  
                        unitOfWork.DockMilkCollectionDtlRepository.Add(dockMilkCollectionDtl);

                    }
                    dockMilkCollection.TotalCan= dockMilkCollectionDTO.dockMilkCollectionList.Sum(s => s.TotalCan).GetValueOrDefault();
                    dockMilkCollection.TotalRejectedCan = dockMilkCollectionDTO.dockMilkCollectionList.Sum(s => s.TotalRejectedCan).GetValueOrDefault();
                    dockMilkCollection.Amount = dockMilkCollectionDTO.dockMilkCollectionList.Sum(s => s.TotalAmount).GetValueOrDefault();
                    dockMilkCollection.TotalQuantity = dockMilkCollectionDTO.dockMilkCollectionList.Sum(s => s.Quantity).GetValueOrDefault();
                    dockMilkCollection.Commission = dockMilkCollection.TotalQuantity * vlc.MilkCommission;
                    dockMilkCollection.TotalAmount = dockMilkCollection.Amount + dockMilkCollection.Commission.GetValueOrDefault();
                  

                }
                else
                {
                    throw new PlatformModuleException("Dock Milk Collection Detail Not Found");
                }
                unitOfWork.DockMilkCollectionRepository.Add(dockMilkCollection);
                UpdateVLCPaymentDetailsForDockCollection(vlc, dockMilkCollection);
                string dockMessage = string.Format(unitOfWork.NatrajConfigurationSettings.DockCollectionMessage, dockMilkCollection.CollectionDateTime.Date, dockMilkCollection.TotalQuantity, dockMilkCollection.TotalAmount);
                var natrajSMSLog = this.SendSMS(vlc.Contact, dockMessage);
                unitOfWork.SMSRepository.Add(natrajSMSLog);
                unitOfWork.SaveChanges();
                new SMSService().SendEmailInBackgroundThread(natrajSMSLog);

                unitOfWork.SaveChanges();
                responseDTO.Status = true;
                responseDTO.Message = String.Format("Dock Milk Collection Detail Added Successfully ");
                responseDTO.Data = this.GetDockCollectionByDateAndShift(dockMilkCollectionDTO.DockMilkCollectionId, DateTimeHelper.GetISTDateTime().Date, dockMilkCollection.ShiftId, 1);

                return responseDTO;
            }
            else
            {
                throw new PlatformModuleException("VLC Collection Details Not Found");
            }

        }


        public void UpdateVLCWalletForDockCollection(int vlcId, decimal totalAmount, bool isCredit)
        {
            var vlcWallet = unitOfWork.VLCWalletRepository.GetByVLCId(vlcId);
            if (isCredit)
                vlcWallet.WalletBalance -= totalAmount;
            else
                vlcWallet.WalletBalance += totalAmount;
            vlcWallet.AmountDueDate = vlcWallet.AmountDueDate.AddDays(10);
            unitOfWork.VLCWalletRepository.Update(vlcWallet);
        }

        public void UpdateVLCPaymentDetailsForDockCollection(VLC vlc, DockMilkCollection dockMilkCollection)
        {
            VLCPaymentDetail vLCPaymentDetail = new VLCPaymentDetail();
            vLCPaymentDetail.VLCPaymentId = unitOfWork.DashboardRepository.NextNumberGenerator("VLCPaymentDetail");
            vLCPaymentDetail.CreatedDate = vLCPaymentDetail.ModifiedDate = DateTimeHelper.GetISTDateTime();
            vLCPaymentDetail.CreatedBy = vLCPaymentDetail.ModifiedBy = "Admin";
            vLCPaymentDetail.VLCId = dockMilkCollection.VLCId;
            vLCPaymentDetail.DockMilkCollectionId = dockMilkCollection.DockMilkCollectionId;
            vLCPaymentDetail.IsDeleted = false;
            vLCPaymentDetail.PaymentComments = "Initial Dock Amount";
            vLCPaymentDetail.PaymentDate = DateTimeHelper.GetISTDateTime();
            vLCPaymentDetail.PaymentDrAmount = dockMilkCollection.TotalAmount;
            unitOfWork.VLCPaymentDetailRepository.Add(vLCPaymentDetail);
            UpdateVLCWalletForDockCollection(vlc.VLCId, dockMilkCollection.TotalAmount, false);
        }

        public void CheckForExistingCollectionDetailByDateShiftProduct(DateTime collectionDate, int shift, int product, int vlcId)
        {
            var existingCollection = unitOfWork.DockMilkCollectionRepository.GetCollectionByShiftDateProduct(collectionDate, shift, product, vlcId);
            if (existingCollection != null)
                throw new PlatformModuleException("VLC Collection Already Exist with given Details");

        }



        public ResponseDTO UpdateDockMilkCollection(DockMilkCollectionDTO dockMilkCollectionDTO)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            //Will update the method when required
            var dockMilkCollection = unitOfWork.DockMilkCollectionRepository.GetById(dockMilkCollectionDTO.DockMilkCollectionId);
            var vlc = unitOfWork.VLCRepository.GetById(dockMilkCollection.VLCId);
            if (dockMilkCollection == null)
                throw new PlatformModuleException(string.Format("Dock Milk Collection Detail Not Found with Collection Id {0}", dockMilkCollectionDTO.DockMilkCollectionId));
            dockMilkCollection.ModifiedDate = DateTimeHelper.GetISTDateTime();
            dockMilkCollection.ModifiedBy = "Admin";
            //    DockMilkCollectionConvertor.ConvertToDockMilkCollectionEntity(ref vlcMilkCollection, vLCMilkCollectionDTO, true);

            //var detailList = unitOfWork.DockMilkCollectionDtlRepository.GetById(vLCMilkCollectionDTO.DockMilkCollectionId);

            //if (detailList != null && detailList.Count() > 0)
            //{
            //    foreach (var collectionDtl in detailList)
            //        unitOfWork.DockMilkCollectionDtlRepository.Delete(collectionDtl.DockMilkCollectionDtlId);

            //}

            //if (vLCMilkCollectionDTO.vLCMilkCollectionDtlDTOList != null)
            //{
            //    foreach (var vlcCollectionDtlDTO in vLCMilkCollectionDTO.vLCMilkCollectionDtlDTOList)
            //    {
            //        DockMilkCollectionDtl vLCMilkCollectionDtl = new DockMilkCollectionDtl();
            //        vLCMilkCollectionDtl.DockMilkCollectionDtlId = unitOfWork.DashboardRepository.NextNumberGenerator("DockMilkCollectionDtl");
            //        vLCMilkCollectionDtl.DockMilkCollectionId = vlcMilkCollection.DockMilkCollectionId;
            //        DockMilkCollectionConvertor.ConvertToDockMilkCollectionDtlEntity(ref vLCMilkCollectionDtl, vlcCollectionDtlDTO, false);
            //        //vLCMilkCollectionDtl.RatePerUnit = unitOfWork.MilkRateRepository.GetMilkRateByApplicableRate(customer.ApplicableRate, vlcCollectionDtlDTO.FAT.GetValueOrDefault(), vlcCollectionDtlDTO.CLR.GetValueOrDefault());
            //        //vlcCollectionDtlDTO.Amount = vLCMilkCollectionDtl.Amount = vLCMilkCollectionDtl.RatePerUnit * vLCMilkCollectionDtl.Qunatity;

            //        unitOfWork.DockMilkCollectionDtlRepository.Add(vLCMilkCollectionDtl);
            //    }

            //    vlcMilkCollection.TotalAmount = vLCMilkCollectionDTO.vLCMilkCollectionDtlDTOList.Sum(s => s.Amount);
            //    vlcMilkCollection.TotalQuantity = vLCMilkCollectionDTO.vLCMilkCollectionDtlDTOList.Sum(s => s.Quantity);
            //}
            //else
            //    throw new PlatformModuleException("VLC Milk Collection Details Not Found");

            unitOfWork.DockMilkCollectionRepository.Update(dockMilkCollection);
            unitOfWork.SaveChanges();
            responseDTO.Status = true;
            responseDTO.Message = String.Format("Milk Collection Detail Updated Successfully");
            responseDTO.Data = this.GetDockCollectionByDateAndShift(dockMilkCollection.DockMilkCollectionId, DateTimeHelper.GetISTDateTime().Date, dockMilkCollection.ShiftId, 1);
            return responseDTO;
        }

        public ResponseDTO DeleteDockMilkCollection(int id)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            UnitOfWork unitOfWork = new UnitOfWork();
            var dockMilkCollection = unitOfWork.DockMilkCollectionRepository.GetById(id);
            //if (vLCMilkCollection != null)
            //{
            //    var detailList = unitOfWork.DockMilkCollectionDtlRepository.GetById(id);

            //    if (detailList != null && detailList.Count() > 0)
            //    {
            //        foreach (var collectionDtl in detailList)
            //            unitOfWork.DockMilkCollectionDtlRepository.Delete(collectionDtl.DockMilkCollectionDtlId);

            //    }
            //    unitOfWork.DockMilkCollectionRepository.Delete(id);
            //    unitOfWork.SaveChanges();
            //    responseDTO.Status = true;
            //    responseDTO.Message = String.Format("Milk Collection Detail Deleted Successfully");
            //    responseDTO.Data = this.GetVLCCustomerCollectionByDateAndShift(vLCMilkCollection.VLCId.GetValueOrDefault(), DateTimeHelper.GetISTDateTime().Date, vLCMilkCollection.ShiftId.GetValueOrDefault(), 1);
            //    return responseDTO;
            //}
            //else
            //{
            //    throw new PlatformModuleException("Milk Collection Detail Not Found ");
            //}

            return responseDTO;
        }







        public ResponseDTO DeleteDockMilkCollectionDtl(int id)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            UnitOfWork unitOfWork = new UnitOfWork();
            //var vLCMilkCollectionDtl = unitOfWork.DockMilkCollectionDtlRepository.GetById(id);

            //unitOfWork.DockMilkCollectionDtlRepository.Delete(id);
            //unitOfWork.SaveChanges();
            responseDTO.Status = true;
            responseDTO.Message = String.Format("Milk Collection Detail Deleted Successfully");
            responseDTO.Data = new object();
            return responseDTO;

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

