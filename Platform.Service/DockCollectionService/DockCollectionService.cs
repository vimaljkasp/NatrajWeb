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

        public List<DockMilkCollectionDTO> GetAllDockMilkMilkCollection()
        {
            List<DockMilkCollectionDTO> DockMilkCollectionList = new List<DockMilkCollectionDTO>();
            var DockMilkCollections = unitOfWork.DockMilkCollectionRepository.GetAll();
            if (DockMilkCollections != null)
            {
                foreach (var vlcMilkCollection in DockMilkCollections)
                {
                    DockMilkCollectionList.Add(DockMilkCollectionConvertor.ConvertToDockMilkCollectionDto(vlcMilkCollection));
                }

            }

            return DockMilkCollectionList;

        }

        public List<DockMilkCollectionDTO> GetAllDockMilkMilkCollectionByPageCount(int? pageNumber, int? count)
        {
            List<DockMilkCollectionDTO> DockMilkCollectionList = new List<DockMilkCollectionDTO>();
            var DockMilkCollections = unitOfWork.DockMilkCollectionRepository.GetDockMilkCollectionByCount(pageNumber, count);
            if (DockMilkCollections != null)
            {
                foreach (var vlc in DockMilkCollections)
                {
                    DockMilkCollectionList.Add(DockMilkCollectionConvertor.ConvertToDockMilkCollectionDto(vlc));
                }

            }

            return DockMilkCollectionList;

        }

        public List<VLCCustomerCollectionDTO> GetVLCCustomerCollectionByDateAndShift(int vlcId, DateTime collectionDate, int shift, int? pageNumber)
        {
            List<VLCCustomerCollectionDTO> vlcCustomerMilkCollection = new List<VLCCustomerCollectionDTO>();

            var vLCMilkCollection = unitOfWork.DockMilkCollectionRepository.GetByVLCIdAndCollectionDateShift(vlcId, collectionDate, shift, pageNumber);
            if (vLCMilkCollection != null)
            {
                foreach (var vlcMilk in vLCMilkCollection)
                {
                  //  vlcCustomerMilkCollection.Add(DockMilkCollectionConvertor.ConvertToVLCCustomerCollectionDTO(vlcMilk));
                }

            }

            return vlcCustomerMilkCollection;
        }

        public ResponseDTO GetVLCCustomerCollectionsByDateAndShift(int vlcId, DateTime collectionDate, int shift, int? pageNumber)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            responseDTO.Status = true;
            responseDTO.Message = "Customer Collections";
            responseDTO.Data = this.GetVLCCustomerCollectionByDateAndShift(vlcId, collectionDate, shift, pageNumber);
            return responseDTO;
        }

        public DockMilkCollectionDTO GetDockMilkCollectionById(int vlcMilkCollectionId)
        {
            DockMilkCollectionDTO vlcMilkCollectionDto = null;
            var vlcMilkCollection = unitOfWork.DockMilkCollectionRepository.GetById(vlcMilkCollectionId);
            if (vlcMilkCollection != null)
            {
             //   vlcMilkCollectionDto = DockMilkCollectionConvertor.ConvertToDockMilkCollectionDto(vlcMilkCollection);
            }
            return vlcMilkCollectionDto;
        }

        //public ResponseDTO AddDockMilkCollectionNew(DockMilkCollectionDTO vLCMilkCollectionDTO)
        //{
        //    var customer = unitOfWork.CustomerRepository.GetById(vLCMilkCollectionDTO.CustomerId);
        //    if (customer != null)
        //    {
        //        ResponseDTO responseDTO = new ResponseDTO();
        //        DockMilkCollection vLCMilkCollection = new DockMilkCollection();
        //        vLCMilkCollection.DockMilkCollectionId = unitOfWork.DashboardRepository.NextNumberGenerator("DockMilkCollection");
        //        vLCMilkCollection.CollectionDateTime = DateTimeHelper.GetISTDateTime();
        //        vLCMilkCollection.CreatedDate = DateTimeHelper.GetISTDateTime();
        //        vLCMilkCollection.ModifiedDate = DateTimeHelper.GetISTDateTime();
        //        vLCMilkCollection.CreatedBy = vLCMilkCollectionDTO.ModifiedBy = "Vimal";
        //        vLCMilkCollection.IsDeleted = false;
        //        DockMilkCollectionConvertor.ConvertToDockMilkCollectionEntity(ref vLCMilkCollection, vLCMilkCollectionDTO, false);
        //        if (vLCMilkCollectionDTO.vLCMilkCollectionDtlDTOList != null)
        //        {
        //            foreach (var vlcCollectionDtlDTO in vLCMilkCollectionDTO.vLCMilkCollectionDtlDTOList)
        //            {
        //                this.CheckForExistingCollectionDetailByDateShiftProduct(vLCMilkCollection.CollectionDateTime.Value.Date, vLCMilkCollectionDTO.ShiftId, vlcCollectionDtlDTO.ProductId, vLCMilkCollectionDTO.CustomerId);
        //                DockMilkCollectionDtl vLCMilkCollectionDtl = new DockMilkCollectionDtl();
        //                vLCMilkCollectionDtl.DockMilkCollectionDtlId = unitOfWork.DashboardRepository.NextNumberGenerator("DockMilkCollectionDtl");
        //                vLCMilkCollectionDtl.DockMilkCollectionId = vLCMilkCollection.DockMilkCollectionId;
        //                DockMilkCollectionConvertor.ConvertToDockMilkCollectionDtlEntity(ref vLCMilkCollectionDtl, vlcCollectionDtlDTO, false);
        //                vLCMilkCollectionDtl.RatePerUnit = unitOfWork.MilkRateRepository.GetMilkRateByApplicableRate(customer.ApplicableRate, vlcCollectionDtlDTO.FAT.GetValueOrDefault(), vlcCollectionDtlDTO.CLR.GetValueOrDefault());
        //                vlcCollectionDtlDTO.Amount=vLCMilkCollectionDtl.Amount = vLCMilkCollectionDtl.RatePerUnit * vLCMilkCollectionDtl.Qunatity;
        //                unitOfWork.DockMilkCollectionDtlRepository.Add(vLCMilkCollectionDtl);
        //            }

        //            vLCMilkCollection.TotalAmount = vLCMilkCollectionDTO.vLCMilkCollectionDtlDTOList.Sum(s => s.Amount);
        //            vLCMilkCollection.TotalQuantity = vLCMilkCollectionDTO.vLCMilkCollectionDtlDTOList.Sum(s => s.Quantity);
        //        }
        //        else
        //        {
        //            throw new PlatformModuleException("Milk Collection Detail Not Found");
        //        }
        //        unitOfWork.DockMilkCollectionRepository.Add(vLCMilkCollection);
        //        string vlcMessage=string.Format(unitOfWork.NatrajConfigurationSettings.VLCCollectionMessage, vLCMilkCollection.CollectionDateTime.Value.Date, vLCMilkCollection.TotalQuantity,vLCMilkCollection.TotalAmount);
        //        var natrajSMSLog = this.SendSMS(customer.Contact, vlcMessage);
        //        unitOfWork.SMSRepository.Add(natrajSMSLog);
        //        unitOfWork.SaveChanges();
        //        new SMSService().SendEmailInBackgroundThread(natrajSMSLog);
        //        responseDTO.Status = true;
        //        responseDTO.Message = String.Format("Milk Collection Detail Added Successfully ");
        //        responseDTO.Data = this.GetVLCCustomerCollectionByDateAndShift(vLCMilkCollectionDTO.VLCId, DateTimeHelper.GetISTDateTime().Date, vLCMilkCollectionDTO.ShiftId, 1);
        //        return responseDTO;

        //    }
        //    else
        //    {
        //        throw new PlatformModuleException(string.Format("Customer Details Not Found with Customer Id {0}", vLCMilkCollectionDTO.CustomerId));
        //    }


        //}

        public NatrajSMSLog SendSMS(string mobileNumber, string message)
        {
            NatrajSMSLog natrajSMSLog = new NatrajSMSLog();
            natrajSMSLog.SMSId = unitOfWork.DashboardRepository.NextNumberGenerator("");
            SMSConvertor.ConvertToSMSMessage(ref natrajSMSLog, NatrajComponent.VLC, SMSType.DockMilkCollection, mobileNumber, message);

            return natrajSMSLog;
        }

        public ResponseDTO AddDockMilkCollection(DockMilkCollectionDTO vLCMilkCollectionDTO)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            //  this.CheckForExisitngCustomer(vlcDto.MobileNumber);
            DockMilkCollection vLCMilkCollection = new DockMilkCollection();
            vLCMilkCollection.DockMilkCollectionId = unitOfWork.DashboardRepository.NextNumberGenerator("DockMilkCollection");
         //   vLCMilkCollection.CollectionDateTime = DateTimeHelper.GetISTDateTime();
            vLCMilkCollection.CreatedDate = DateTimeHelper.GetISTDateTime();
            vLCMilkCollection.ModifiedDate = DateTimeHelper.GetISTDateTime();
            vLCMilkCollection.CreatedBy = vLCMilkCollectionDTO.ModifiedBy = "Vimal";
            vLCMilkCollection.IsDeleted = false;
            DockMilkCollectionConvertor.ConvertToDockMilkCollectionEntity(ref vLCMilkCollection, vLCMilkCollectionDTO, false);
            if (vLCMilkCollectionDTO.vLCMilkCollectionDtlDTOList != null)
            {
                foreach (var vlcCollectionDtlDTO in vLCMilkCollectionDTO.vLCMilkCollectionDtlDTOList)
                {
                    //    this.CheckForExistingCollectionDetailByDateShiftProduct(vLCMilkCollection.CollectionDateTime.Value.Date, vLCMilkCollectionDTO.ShiftId, vlcCollectionDtlDTO.ProductId, vLCMilkCollectionDTO.CustomerId);
                    //    DockMilkCollectionDtl vLCMilkCollectionDtl = new DockMilkCollectionDtl();
                    //    vLCMilkCollectionDtl.DockMilkCollectionDtlId = unitOfWork.DashboardRepository.NextNumberGenerator("DockMilkCollectionDtl");
                    //    vLCMilkCollectionDtl.DockMilkCollectionId = vLCMilkCollection.DockMilkCollectionId;
                    //    DockMilkCollectionConvertor.ConvertToDockMilkCollectionDtlEntity(ref vLCMilkCollectionDtl, vlcCollectionDtlDTO, false);
                    //    unitOfWork.DockMilkCollectionDtlRepository.Add(vLCMilkCollectionDtl);
                    //}
                }

             //   vLCMilkCollection.TotalAmount = vLCMilkCollectionDTO.vLCMilkCollectionDtlDTOList.Sum(s => s.Amount);
                vLCMilkCollection.TotalQuantity = vLCMilkCollectionDTO.vLCMilkCollectionDtlDTOList.Sum(s => s.Quantity).GetValueOrDefault();
            }
            else
            {
                throw new PlatformModuleException("Milk Collection Detail Not Found");
            }
         //   unitOfWork.DockMilkCollectionRepository.Add(vLCMilkCollection);

            unitOfWork.SaveChanges();
            responseDTO.Status = true;
            responseDTO.Message = String.Format("Milk Collection Detail Added Successfully ");
            responseDTO.Data = this.GetVLCCustomerCollectionByDateAndShift(vLCMilkCollectionDTO.VLCId, DateTimeHelper.GetISTDateTime().Date, vLCMilkCollectionDTO.ShiftId, 1);

            return responseDTO;


        }

        public void CheckForExistingCollectionDetailByDateShiftProduct(DateTime collectionDate, int shift, int product, int customerId)
        {
            var existingCollection = unitOfWork.DockMilkCollectionRepository.GetCollectionByShiftDateProduct(collectionDate, shift, product, customerId);
            if (existingCollection != null)
                throw new PlatformModuleException("Customer Collection Already Exist with given Details");

        }



        public ResponseDTO UpdateDockMilkCollection(DockMilkCollectionDTO vLCMilkCollectionDTO)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            //Will update the method when required
            var vlcMilkCollection = unitOfWork.DockMilkCollectionRepository.GetById(vLCMilkCollectionDTO.DockMilkCollectionId);
            var customer = unitOfWork.CustomerRepository.GetById(vlcMilkCollection.CustomerId.GetValueOrDefault());
            if (vlcMilkCollection == null)
                throw new PlatformModuleException(string.Format("VLC Milk Collection Detail Not Found with Collection Id {0}", vLCMilkCollectionDTO.DockMilkCollectionId));
            vlcMilkCollection.ModifiedDate = DateTimeHelper.GetISTDateTime();
            vlcMilkCollection.ModifiedBy = "vimal";
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

            unitOfWork.DockMilkCollectionRepository.Update(vlcMilkCollection);
            unitOfWork.SaveChanges();
            responseDTO.Status = true;
            responseDTO.Message = String.Format("Milk Collection Detail Updated Successfully");
            responseDTO.Data = this.GetVLCCustomerCollectionByDateAndShift(vlcMilkCollection.VLCId.GetValueOrDefault(), DateTimeHelper.GetISTDateTime().Date, vlcMilkCollection.ShiftId.GetValueOrDefault(), 1);
            return responseDTO;
        }

        public ResponseDTO DeleteDockMilkCollection(int id)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            UnitOfWork unitOfWork = new UnitOfWork();
            var vLCMilkCollection = unitOfWork.DockMilkCollectionRepository.GetById(id);
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

        public void AddDockMilkCollectionDtl(DockMilkCollectionDtlDTO vlcMilkCollectionDtlDto)
        {
            throw new NotImplementedException();
        }

        public void UpdateDockMilkCollectionDtl(DockMilkCollectionDtlDTO vlcMilkCollectionDtlDto)
        {
            throw new NotImplementedException();
        }

        public DockMilkCollectionDTO GetDockMilkMilkCollectionById(int vlcId)
        {
            throw new NotImplementedException();
        }

        public ResponseDTO GetDockMilkCollectionsByDateAndShift(int DockMilkCollectionId, DateTime collectionDate, int shift, int? PageNumber)
        {
            throw new NotImplementedException();
        }

        public ResponseDTO AddDockMilkMilkCollection(DockMilkCollectionDTO DockMilkCollectionDTO)
        {
            throw new NotImplementedException();
        }

        public ResponseDTO UpdateDockMilkMilkCollection(DockMilkCollectionDTO DockMilkCollectionDTO)
        {
            throw new NotImplementedException();
        }

        public ResponseDTO DeleteDockMilkMilkCollection(int id)
        {
            throw new NotImplementedException();
        }
    }
}

