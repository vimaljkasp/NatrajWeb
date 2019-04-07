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

        public List<DockMilkCollectionDTO> GetDockCollectionByDateAndShift(DateTime collectionDate, int shift, int? pageNumber)
        {
            List<DockMilkCollectionDTO> dockMilkCollectionDTOList = new List<DockMilkCollectionDTO>();

            var dockMilkCollections = unitOfWork.DockMilkCollectionRepository.GetDockCollectionByDateShift( collectionDate, shift, pageNumber);
            if (dockMilkCollections != null)
            {
                foreach (var dockmilkCollection in dockMilkCollections)
                {
                    dockMilkCollectionDTOList.Add(DockMilkCollectionConvertor.ConvertToDockMilkCollectionDto(dockmilkCollection));
                }

            }

            return dockMilkCollectionDTOList;
        }

        public ResponseDTO GetDockMilkCollectionsByDateAndShift(DateTime collectionDate, int shift, int? pageNumber)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            responseDTO.Status = true;
            responseDTO.Message = "Dock  Collections";
            responseDTO.Data = this.GetDockCollectionByDateAndShift(collectionDate, shift, pageNumber);
            return responseDTO;
        }

        public DockMilkCollectionDTO GetDockMilkCollectionById(int dockCollectionId)
        {
            DockMilkCollectionDTO dockMilkCollectionDTO = new DockMilkCollectionDTO();
            var dockMilkCollection = unitOfWork.DockMilkCollectionRepository.GetById(dockCollectionId);
            if (dockMilkCollection != null)
            {
                dockMilkCollectionDTO= DockMilkCollectionConvertor.ConvertToDockMilkCollectionDto(dockMilkCollection);
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
                decimal milkCommission = GetCommissionForDockMilkCollection(vlc);
                DockMilkCollection dockMilkCollection = new DockMilkCollection();
                dockMilkCollection.DockMilkCollectionId = unitOfWork.DashboardRepository.NextNumberGenerator("DockMilkCollection");
                if(dockMilkCollectionDTO.CollectionDateTime != DateTime.MinValue)
                {
                    dockMilkCollection.CollectionDateTime = dockMilkCollectionDTO.CollectionDateTime;
                }
                else
                {
                    dockMilkCollection.CollectionDateTime = DateTimeHelper.GetISTDateTime();
                }
             
                dockMilkCollection.CreatedDate = DateTimeHelper.GetISTDateTime();
                dockMilkCollection.ModifiedDate = DateTimeHelper.GetISTDateTime();
                dockMilkCollection.CreatedBy = dockMilkCollectionDTO.ModifiedBy = "Admin";
                dockMilkCollection.IsDeleted = false;
                DockMilkCollectionConvertor.ConvertToDockMilkCollectionEntity(ref dockMilkCollection, dockMilkCollectionDTO, false);
                if (dockMilkCollectionDTO.dockMilkCollectionList != null)
                {
                    foreach (var dockMilkCollectionDtlDto in dockMilkCollectionDTO.dockMilkCollectionList)

                    {

                        if (dockMilkCollectionDtlDto.Quantity > 0 && dockMilkCollectionDtlDto.ProductId > 0)
                        {
                            this.CheckForExistingCollectionDetailByDateShiftProduct(dockMilkCollection.CollectionDateTime.Date, dockMilkCollection.ShiftId, (int)dockMilkCollectionDtlDto.ProductId, dockMilkCollection.VLCId);
                            DockMilkCollectionDtl dockMilkCollectionDtl = new DockMilkCollectionDtl();
                            dockMilkCollectionDtl.DockMilkCollectionDtlI = unitOfWork.DashboardRepository.NextNumberGenerator("DockMilkCollectionDtl");
                            dockMilkCollectionDtl.DockMilkCollectionId = dockMilkCollection.DockMilkCollectionId;
                            DockMilkCollectionConvertor.ConvertToDockMilkCollectionDtlEntity(ref dockMilkCollectionDtl, dockMilkCollectionDtlDto, false);
                            dockMilkCollectionDtl.RatePerUnit = unitOfWork.MilkRateRepository.GetMilkRateByApplicableRate(vlc.ApplicableRate, dockMilkCollectionDtlDto.FAT.GetValueOrDefault(), dockMilkCollectionDtlDto.CLR.GetValueOrDefault());
                            dockMilkCollectionDtlDto.Amount = dockMilkCollectionDtl.Amount = dockMilkCollectionDtl.RatePerUnit.GetValueOrDefault() * dockMilkCollectionDtlDto.Quantity;
                            dockMilkCollectionDtlDto.Commission = dockMilkCollectionDtl.Commission = dockMilkCollectionDtlDto.Quantity * milkCommission;
                            dockMilkCollectionDtlDto.TotalAmount = dockMilkCollectionDtl.TotalAmount = dockMilkCollectionDtl.Commission.GetValueOrDefault() + dockMilkCollectionDtl.Amount;

                            unitOfWork.DockMilkCollectionDtlRepository.Add(dockMilkCollectionDtl);
                        }

                    }
                    dockMilkCollection.TotalCan= dockMilkCollectionDTO.dockMilkCollectionList.Sum(s => s.TotalCan);
                    dockMilkCollection.TotalRejectedCan = dockMilkCollectionDTO.dockMilkCollectionList.Sum(s => s.TotalRejectedCan);
                    dockMilkCollection.Amount = dockMilkCollectionDTO.dockMilkCollectionList.Sum(s => s.Amount);
                    dockMilkCollection.TotalQuantity = dockMilkCollectionDTO.dockMilkCollectionList.Sum(s => s.Quantity);
                    dockMilkCollection.Commission = dockMilkCollectionDTO.dockMilkCollectionList.Sum(s => s.Commission).GetValueOrDefault();
                    dockMilkCollection.TotalAmount = dockMilkCollectionDTO.dockMilkCollectionList.Sum(s => s.TotalAmount).GetValueOrDefault();

                    dockMilkCollection.RejectedQuantity = dockMilkCollectionDTO.dockMilkCollectionList.Sum(s => s.RejectedQuantity);
                  

                }
                else
                {
                    throw new PlatformModuleException("Dock Milk Collection Detail Not Found");
                }
                unitOfWork.DockMilkCollectionRepository.Add(dockMilkCollection);
                UpdateVLCPaymentDetailsForDockCollection(vlc, dockMilkCollection);
                string dockMessage = string.Format(NatrajConfigurationHelper.DockCollectionMessage, dockMilkCollection.CollectionDateTime.Date, dockMilkCollection.TotalQuantity, dockMilkCollection.TotalAmount);
                var natrajSMSLog = this.SendSMS(vlc.Contact, dockMessage);
                unitOfWork.SMSRepository.Add(natrajSMSLog);
                unitOfWork.SaveChanges();
                new SMSService().SendEmailInBackgroundThread(natrajSMSLog);

                unitOfWork.SaveChanges();
                responseDTO.Status = true;
                responseDTO.Message = String.Format("Dock Milk Collection Detail Added Successfully ");
                responseDTO.Data = this.GetDockCollectionByDateAndShift(DateTimeHelper.GetISTDateTime().Date, dockMilkCollection.ShiftId, 1);

                return responseDTO;
            }
            else
            {
                throw new PlatformModuleException("VLC Collection Details Not Found");
            }

        }



        private decimal GetCommissionForDockMilkCollection(VLC vLC)
        {
            if (NatrajConfigurationHelper.IsDockCommonCommissionEnabled)
                return NatrajConfigurationHelper.DockCommonCommission;
            else
                return vLC.MilkCommission.GetValueOrDefault();

        }
        public void UpdateVLCWalletForDockCollection(int vlcId, decimal totalAmount, bool isCredit)
        {
            var vlcWallet = unitOfWork.VLCWalletRepository.GetByVLCId(vlcId);
            if (vlcWallet != null)
            {
                if (isCredit)
                    vlcWallet.WalletBalance -= totalAmount;
                else
                    vlcWallet.WalletBalance += totalAmount;
                vlcWallet.AmountDueDate = vlcWallet.AmountDueDate.AddDays(10);
                unitOfWork.VLCWalletRepository.Update(vlcWallet);
            }
            else
            {
                VLCWallet vLCWallet = new VLCWallet();
                vLCWallet.WalletId = unitOfWork.DashboardRepository.NextNumberGenerator("VLCWallet");
                vLCWallet.VLCId = vlcId;
                if (isCredit)
                    vlcWallet.WalletBalance -= totalAmount;
                else
                    vlcWallet.WalletBalance += totalAmount;
                vLCWallet.AmountDueDate = DateTimeHelper.GetISTDateTime().AddDays(10);
                unitOfWork.VLCWalletRepository.Add(vLCWallet);
            }
        }

        public void UpdateVLCPaymentDetailsForDockCollection(VLC vlc, DockMilkCollection dockMilkCollection)
        {
            var OldPaymentDetail = unitOfWork.VLCPaymentDetailRepository.GetVLCPaymentDetailByDockCollectionId(dockMilkCollection.DockMilkCollectionId);
            if (OldPaymentDetail != null)
            {
                decimal oldAmount = OldPaymentDetail.PaymentDrAmount.GetValueOrDefault();
                OldPaymentDetail.PaymentDrAmount = dockMilkCollection.TotalAmount;
                OldPaymentDetail.PaymentDate= DateTimeHelper.GetISTDateTime();
                unitOfWork.VLCPaymentDetailRepository.Update(OldPaymentDetail);
                UpdateVLCWalletForDockCollection(vlc.VLCId, dockMilkCollection.TotalAmount - oldAmount, false);

            }
            else
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
            decimal milkCommission = GetCommissionForDockMilkCollection(vlc);
            DockMilkCollectionConvertor.ConvertToDockMilkCollectionEntity(ref dockMilkCollection, dockMilkCollectionDTO, true);


            if (dockMilkCollectionDTO.dockMilkCollectionList != null)
            {
                foreach (var dockCollectionDtlDTO in dockMilkCollectionDTO.dockMilkCollectionList)
                {
                    var dockMilkCollectionDtl = unitOfWork.DockMilkCollectionDtlRepository.GetById(dockCollectionDtlDTO.DockMilkCollectionDtlId);
                    if (dockMilkCollectionDtl != null && dockCollectionDtlDTO.Quantity > 0 && dockCollectionDtlDTO.ProductId > 0)
                    {
                        DockMilkCollectionConvertor.ConvertToDockMilkCollectionDtlEntity(ref dockMilkCollectionDtl, dockCollectionDtlDTO, false);
                        dockMilkCollectionDtl.RatePerUnit = unitOfWork.MilkRateRepository.GetMilkRateByApplicableRate(vlc.ApplicableRate, dockCollectionDtlDTO.FAT.GetValueOrDefault(), dockCollectionDtlDTO.CLR.GetValueOrDefault());
                        dockCollectionDtlDTO.Amount= dockMilkCollectionDtl.Amount = dockMilkCollectionDtl.Amount = dockMilkCollectionDtl.RatePerUnit.GetValueOrDefault() * dockCollectionDtlDTO.Quantity;
                        dockCollectionDtlDTO.Commission= dockMilkCollectionDtl.Commission = dockMilkCollectionDtl.Commission = dockCollectionDtlDTO.Quantity * milkCommission;
                        dockCollectionDtlDTO.TotalAmount= dockMilkCollectionDtl.TotalAmount = dockMilkCollectionDtl.Commission.GetValueOrDefault() + dockMilkCollectionDtl.Amount;

                        unitOfWork.DockMilkCollectionDtlRepository.Update(dockMilkCollectionDtl);
                    }
                }
  
            dockMilkCollection.TotalCan = dockMilkCollectionDTO.dockMilkCollectionList.Sum(s => s.TotalCan);
            dockMilkCollection.TotalRejectedCan = dockMilkCollectionDTO.dockMilkCollectionList.Sum(s => s.TotalRejectedCan);
            dockMilkCollection.Amount = dockMilkCollectionDTO.dockMilkCollectionList.Sum(s => s.Amount);
            dockMilkCollection.TotalQuantity = dockMilkCollectionDTO.dockMilkCollectionList.Sum(s => s.Quantity);
            dockMilkCollection.Commission = dockMilkCollectionDTO.dockMilkCollectionList.Sum(s => s.Commission).GetValueOrDefault();
            dockMilkCollection.TotalAmount = dockMilkCollectionDTO.dockMilkCollectionList.Sum(s => s.TotalAmount).GetValueOrDefault();

            dockMilkCollection.RejectedQuantity = dockMilkCollectionDTO.dockMilkCollectionList.Sum(s => s.RejectedQuantity);

        }
            else
                throw new PlatformModuleException("Dock Milk Collection Details Not Found");

            unitOfWork.DockMilkCollectionRepository.Update(dockMilkCollection);
            UpdateVLCPaymentDetailsForDockCollection(vlc, dockMilkCollection);
      

            string dockMessage = string.Format(NatrajConfigurationHelper.DockCollectionMessage, dockMilkCollection.CollectionDateTime.Date, dockMilkCollection.TotalQuantity, dockMilkCollection.TotalAmount);
            var natrajSMSLog = this.SendSMS(vlc.Contact, dockMessage);
            unitOfWork.SMSRepository.Add(natrajSMSLog);
            unitOfWork.SaveChanges();
            new SMSService().SendEmailInBackgroundThread(natrajSMSLog);

            unitOfWork.SaveChanges();
            responseDTO.Status = true;
            responseDTO.Message = String.Format("Dock Milk Collection Detail Added Successfully ");
            responseDTO.Data = this.GetDockCollectionByDateAndShift(DateTimeHelper.GetISTDateTime().Date, dockMilkCollection.ShiftId, 1);

            return responseDTO;
        }

        public ResponseDTO DeleteDockMilkCollection(int id)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            UnitOfWork unitOfWork = new UnitOfWork();
            var dockMilkCollection = unitOfWork.DockMilkCollectionRepository.GetById(id);
            if (dockMilkCollection != null)
            {
                var detailList = unitOfWork.DockMilkCollectionDtlRepository.GetByDockMilkCollectionId(id);

                if (detailList != null && detailList.Count() > 0)
                {
                    foreach (var collectionDtl in detailList)
                        unitOfWork.DockMilkCollectionDtlRepository.Delete(collectionDtl.DockMilkCollectionDtlI);

                }
                unitOfWork.DockMilkCollectionRepository.Delete(id);
                unitOfWork.SaveChanges();
                responseDTO.Status = true;
                responseDTO.Message = String.Format("Dock Milk Collection Detail Deleted Successfully");
                responseDTO.Data = this.GetDockCollectionByDateAndShift(DateTimeHelper.GetISTDateTime().Date, dockMilkCollection.ShiftId, 1);
                return responseDTO;
            }
            else
            {
                throw new PlatformModuleException("Milk Collection Detail Not Found ");
            }

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

