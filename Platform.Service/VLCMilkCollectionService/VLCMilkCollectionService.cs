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
    public partial class VLCMilkCollectionService : IVLCMilkCollectionService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public List<VLCMilkCollectionDTO> GetAllVLCMilkCollection()
        {
            List<VLCMilkCollectionDTO> vlcMilkCollectionList = new List<VLCMilkCollectionDTO>();
            var vLCMilkCollections = unitOfWork.VLCMilkCollectionRepository.GetAll();
            if (vLCMilkCollections != null)
            {
                foreach (var vlcMilkCollection in vLCMilkCollections)
                {
                    vlcMilkCollectionList.Add(VLCMilkCollectionConvertor.ConvertToVLCMilkCollectionDto(vlcMilkCollection));
                }

            }

            return vlcMilkCollectionList;

        }

        public List<VLCMilkCollectionDTO> GetAllVLCMilkCollectionByPageCount(int? pageNumber, int? count)
        {
            List<VLCMilkCollectionDTO> vlcMilkCollectionList = new List<VLCMilkCollectionDTO>();
            var vLCMilkCollections = unitOfWork.VLCMilkCollectionRepository.GetVLCMilkCollectionByCount(pageNumber, count);
            if (vLCMilkCollections != null)
            {
                foreach (var vlc in vLCMilkCollections)
                {
                    vlcMilkCollectionList.Add(VLCMilkCollectionConvertor.ConvertToVLCMilkCollectionDto(vlc));
                }

            }

            return vlcMilkCollectionList;

        }

        public List<VLCCustomerCollectionDTO> GetVLCCustomerCollectionByDateAndShift(int vlcId, DateTime collectionDate, ShiftEnum shift, int? pageNumber)
        {
            List<VLCCustomerCollectionDTO> vlcCustomerMilkCollection = new List<VLCCustomerCollectionDTO>();

            var vLCMilkCollection = unitOfWork.VLCMilkCollectionRepository.GetByVLCIdAndCollectionDateShift(vlcId, collectionDate, shift, pageNumber);
            if (vLCMilkCollection != null)
            {
                foreach (var vlcMilk in vLCMilkCollection)
                {
                    vlcCustomerMilkCollection.Add(VLCMilkCollectionConvertor.ConvertToVLCCustomerCollectionDTO(vlcMilk));
                }

            }

            return vlcCustomerMilkCollection;
        }

        public ResponseDTO GetVLCCustomerCollectionsByDateAndShift(int vlcId, DateTime collectionDate, ShiftEnum shift, int? pageNumber)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            responseDTO.Status = true;
            responseDTO.Message = "Customer Collections";
            responseDTO.Data = this.GetVLCCustomerCollectionByDateAndShift(vlcId, collectionDate, shift, pageNumber);
            return responseDTO;
        }

        public VLCMilkCollectionDTO GetVLCMilkCollectionById(int vlcMilkCollectionId)
        {
            VLCMilkCollectionDTO vlcMilkCollectionDto = null;
            var vlcMilkCollection = unitOfWork.VLCMilkCollectionRepository.GetById(vlcMilkCollectionId);
            if (vlcMilkCollection != null)
            {
                vlcMilkCollectionDto = VLCMilkCollectionConvertor.ConvertToVLCMilkCollectionDto(vlcMilkCollection);
            }
            return vlcMilkCollectionDto;
        }

        //public ResponseDTO AddVLCMilkCollectionNew(VLCMilkCollectionDTO vLCMilkCollectionDTO)
        //{
        //    var customer = unitOfWork.CustomerRepository.GetById(vLCMilkCollectionDTO.CustomerId);
        //    if (customer != null)
        //    {
        //        ResponseDTO responseDTO = new ResponseDTO();
        //        VLCMilkCollection vLCMilkCollection = new VLCMilkCollection();
        //        vLCMilkCollection.VLCMilkCollectionId = unitOfWork.DashboardRepository.NextNumberGenerator("VLCMilkCollection");
        //        vLCMilkCollection.CollectionDateTime = DateTimeHelper.GetISTDateTime();
        //        vLCMilkCollection.CreatedDate = DateTimeHelper.GetISTDateTime();
        //        vLCMilkCollection.ModifiedDate = DateTimeHelper.GetISTDateTime();
        //        vLCMilkCollection.CreatedBy = vLCMilkCollectionDTO.ModifiedBy = "Admin";
        //        vLCMilkCollection.IsDeleted = false;
        //        VLCMilkCollectionConvertor.ConvertToVLCMilkCollectionEntity(ref vLCMilkCollection, vLCMilkCollectionDTO, false);
        //        if (vLCMilkCollectionDTO.vLCMilkCollectionDtlDTOList != null)
        //        {
        //            foreach (var vlcCollectionDtlDTO in vLCMilkCollectionDTO.vLCMilkCollectionDtlDTOList)
        //            {
        //                this.CheckForExistingCollectionDetailByDateShiftProduct(vLCMilkCollection.CollectionDateTime.Value.Date, vLCMilkCollectionDTO.ShiftId, vlcCollectionDtlDTO.ProductId, vLCMilkCollectionDTO.CustomerId);
        //                VLCMilkCollectionDtl vLCMilkCollectionDtl = new VLCMilkCollectionDtl();
        //                vLCMilkCollectionDtl.VLCMilkCollectionDtlId = unitOfWork.DashboardRepository.NextNumberGenerator("VLCMilkCollectionDtl");
        //                vLCMilkCollectionDtl.VLCMilkCollectionId = vLCMilkCollection.VLCMilkCollectionId;
        //                VLCMilkCollectionConvertor.ConvertToVLCMilkCollectionDtlEntity(ref vLCMilkCollectionDtl, vlcCollectionDtlDTO, false);
        //                vLCMilkCollectionDtl.RatePerUnit = unitOfWork.MilkRateRepository.GetMilkRateByApplicableRate(customer.ApplicableRate, vlcCollectionDtlDTO.FAT.GetValueOrDefault(), vlcCollectionDtlDTO.CLR.GetValueOrDefault());
        //                vlcCollectionDtlDTO.Amount=vLCMilkCollectionDtl.Amount = vLCMilkCollectionDtl.RatePerUnit * vLCMilkCollectionDtl.Qunatity;
        //                unitOfWork.VLCMilkCollectionDtlRepository.Add(vLCMilkCollectionDtl);
        //            }

        //            vLCMilkCollection.TotalAmount = vLCMilkCollectionDTO.vLCMilkCollectionDtlDTOList.Sum(s => s.Amount);
        //            vLCMilkCollection.TotalQuantity = vLCMilkCollectionDTO.vLCMilkCollectionDtlDTOList.Sum(s => s.Quantity);
        //        }
        //        else
        //        {
        //            throw new PlatformModuleException("Milk Collection Detail Not Found");
        //        }
        //        unitOfWork.VLCMilkCollectionRepository.Add(vLCMilkCollection);
        //        string vlcMessage=string.Format(NatrajConfigurationHelper.VLCCollectionMessage, vLCMilkCollection.CollectionDateTime.Value.Date, vLCMilkCollection.TotalQuantity,vLCMilkCollection.TotalAmount);
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
            SMSConvertor.ConvertToSMSMessage(ref natrajSMSLog, NatrajComponent.VLC, SMSType.VLCMilkCollection, mobileNumber, message);

            return natrajSMSLog;
        }

        public ResponseDTO AddVLCMilkCollection(VLCMilkCollectionDTO vLCMilkCollectionDTO)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            //  this.CheckForExisitngCustomer(vlcDto.MobileNumber);
            VLCMilkCollection vLCMilkCollection = new VLCMilkCollection();
            vLCMilkCollection.VLCMilkCollectionId = unitOfWork.DashboardRepository.NextNumberGenerator("VLCMilkCollection");
            vLCMilkCollection.CollectionDateTime = DateTimeHelper.GetISTDateTime();
            vLCMilkCollection.CreatedDate = DateTimeHelper.GetISTDateTime();
            vLCMilkCollection.ModifiedDate = DateTimeHelper.GetISTDateTime();
            vLCMilkCollection.CreatedBy = vLCMilkCollectionDTO.ModifiedBy = "Admin";
            vLCMilkCollection.IsDeleted = false;
            VLCMilkCollectionConvertor.ConvertToVLCMilkCollectionEntity(ref vLCMilkCollection, vLCMilkCollectionDTO, false);
            if (vLCMilkCollectionDTO.vLCMilkCollectionDtlDTOList != null)
            {
                foreach (var vlcCollectionDtlDTO in vLCMilkCollectionDTO.vLCMilkCollectionDtlDTOList)
                {
                    this.CheckForExistingCollectionDetailByDateShiftProduct(vLCMilkCollection.CollectionDateTime.Value.Date, vLCMilkCollectionDTO.ShiftId, vlcCollectionDtlDTO.ProductId, vLCMilkCollectionDTO.CustomerId);
                    VLCMilkCollectionDtl vLCMilkCollectionDtl = new VLCMilkCollectionDtl();
                    vLCMilkCollectionDtl.VLCMilkCollectionDtlId = unitOfWork.DashboardRepository.NextNumberGenerator("VLCMilkCollectionDtl");
                    vLCMilkCollectionDtl.VLCMilkCollectionId = vLCMilkCollection.VLCMilkCollectionId;
                    VLCMilkCollectionConvertor.ConvertToVLCMilkCollectionDtlEntity(ref vLCMilkCollectionDtl, vlcCollectionDtlDTO, false);
                    unitOfWork.VLCMilkCollectionDtlRepository.Add(vLCMilkCollectionDtl);
                }

                vLCMilkCollection.TotalAmount = vLCMilkCollectionDTO.vLCMilkCollectionDtlDTOList.Sum(s => s.Amount);
                vLCMilkCollection.TotalQuantity = vLCMilkCollectionDTO.vLCMilkCollectionDtlDTOList.Sum(s => s.Quantity);
            }
            else
            {
                throw new PlatformModuleException("Milk Collection Detail Not Found");
            }
            unitOfWork.VLCMilkCollectionRepository.Add(vLCMilkCollection);

            unitOfWork.SaveChanges();
            responseDTO.Status = true;
            responseDTO.Message = String.Format("Milk Collection Detail Added Successfully ");
            responseDTO.Data = this.GetVLCCustomerCollectionByDateAndShift(vLCMilkCollectionDTO.VLCId, DateTimeHelper.GetISTDateTime().Date, vLCMilkCollectionDTO.ShiftId, 1);

            return responseDTO;


        }

        public void CheckForExistingCollectionDetailByDateShiftProduct(DateTime collectionDate, ShiftEnum shift, MilkTypeEnum product, int customerId)
        {
            var existingCollection = unitOfWork.VLCMilkCollectionRepository.GetCollectionByShiftDateProduct(collectionDate, shift, product, customerId);
            if (existingCollection != null)
                throw new PlatformModuleException("Customer Collection Already Exist with given Details");

        }



        public ResponseDTO UpdateVLCMilkCollection(VLCMilkCollectionDTO vLCMilkCollectionDTO)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            //Will update the method when required
            var vlcMilkCollection = unitOfWork.VLCMilkCollectionRepository.GetById(vLCMilkCollectionDTO.VLCMilkCollectionId);
            //TODO: Make sure what detail need to allow for update - Anil
            vlcMilkCollection.ShiftId = (int)vLCMilkCollectionDTO.ShiftId;
            vlcMilkCollection.CollectionDateTime = vLCMilkCollectionDTO.CollectionDateTime;
            vlcMilkCollection.CustomerId = vLCMilkCollectionDTO.CustomerId;
            vlcMilkCollection.VLCId = vLCMilkCollectionDTO.VLCId;

            var customer = unitOfWork.CustomerRepository.GetById(vlcMilkCollection.CustomerId.GetValueOrDefault());
            if (vlcMilkCollection == null)
                throw new PlatformModuleException(string.Format("VLC Milk Collection Detail Not Found with Collection Id {0}", vLCMilkCollectionDTO.VLCMilkCollectionId));
            vlcMilkCollection.ModifiedDate = DateTimeHelper.GetISTDateTime();
            vlcMilkCollection.ModifiedBy = "Admin";
            //    VLCMilkCollectionConvertor.ConvertToVLCMilkCollectionEntity(ref vlcMilkCollection, vLCMilkCollectionDTO, true);

            var detailList = unitOfWork.VLCMilkCollectionDtlRepository.GetById(vLCMilkCollectionDTO.VLCMilkCollectionId);

            if (detailList != null && detailList.Count() > 0)
            {
                foreach (var collectionDtl in detailList)
                    unitOfWork.VLCMilkCollectionDtlRepository.Delete(collectionDtl.VLCMilkCollectionDtlId);

            }

            if (vLCMilkCollectionDTO.vLCMilkCollectionDtlDTOList != null)
            {
                foreach (var vlcCollectionDtlDTO in vLCMilkCollectionDTO.vLCMilkCollectionDtlDTOList)
                {
                    VLCMilkCollectionDtl vLCMilkCollectionDtl = new VLCMilkCollectionDtl();
                    vLCMilkCollectionDtl.VLCMilkCollectionDtlId = unitOfWork.DashboardRepository.NextNumberGenerator("VLCMilkCollectionDtl");
                    vLCMilkCollectionDtl.VLCMilkCollectionId = vlcMilkCollection.VLCMilkCollectionId;
                    VLCMilkCollectionConvertor.ConvertToVLCMilkCollectionDtlEntity(ref vLCMilkCollectionDtl, vlcCollectionDtlDTO, false);
                    //vLCMilkCollectionDtl.RatePerUnit = unitOfWork.MilkRateRepository.GetMilkRateByApplicableRate(customer.ApplicableRate, vlcCollectionDtlDTO.FAT.GetValueOrDefault(), vlcCollectionDtlDTO.CLR.GetValueOrDefault());
                    //vlcCollectionDtlDTO.Amount = vLCMilkCollectionDtl.Amount = vLCMilkCollectionDtl.RatePerUnit * vLCMilkCollectionDtl.Qunatity;

                    unitOfWork.VLCMilkCollectionDtlRepository.Add(vLCMilkCollectionDtl);
                }

                vlcMilkCollection.TotalAmount = vLCMilkCollectionDTO.vLCMilkCollectionDtlDTOList.Sum(s => s.Amount);
                vlcMilkCollection.TotalQuantity = vLCMilkCollectionDTO.vLCMilkCollectionDtlDTOList.Sum(s => s.Quantity);
            }
            else
                throw new PlatformModuleException("VLC Milk Collection Details Not Found");

            unitOfWork.VLCMilkCollectionRepository.Update(vlcMilkCollection);
            unitOfWork.SaveChanges();
            responseDTO.Status = true;
            responseDTO.Message = String.Format("Milk Collection Detail Updated Successfully");
            ShiftEnum shift;
            Enum.TryParse<ShiftEnum>(vlcMilkCollection.ShiftId.GetValueOrDefault().ToString(), out shift);
            responseDTO.Data = this.GetVLCCustomerCollectionByDateAndShift(vlcMilkCollection.VLCId.GetValueOrDefault(), DateTimeHelper.GetISTDateTime().Date, shift, 1);
            return responseDTO;
        }

        public ResponseDTO DeleteVLCMilkCollection(int id)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            UnitOfWork unitOfWork = new UnitOfWork();
            var vLCMilkCollection = unitOfWork.VLCMilkCollectionRepository.GetById(id);
            if (vLCMilkCollection != null)
            {
                var detailList = unitOfWork.VLCMilkCollectionDtlRepository.GetById(id);

                if (detailList != null && detailList.Count() > 0)
                {
                    foreach (var collectionDtl in detailList)
                        unitOfWork.VLCMilkCollectionDtlRepository.Delete(collectionDtl.VLCMilkCollectionDtlId);

                }
                unitOfWork.VLCMilkCollectionRepository.Delete(id);
                unitOfWork.SaveChanges();
                responseDTO.Status = true;
                responseDTO.Message = String.Format("Milk Collection Detail Deleted Successfully");
                ShiftEnum shift;
                Enum.TryParse<ShiftEnum>(vLCMilkCollection.ShiftId.GetValueOrDefault().ToString(), out shift);
                responseDTO.Data = this.GetVLCCustomerCollectionByDateAndShift(vLCMilkCollection.VLCId.GetValueOrDefault(), DateTimeHelper.GetISTDateTime().Date, shift, 1);
                return responseDTO;
            }
            else
            {
                throw new PlatformModuleException("Milk Collection Detail Not Found ");
            }


        }







        public ResponseDTO DeleteVLCMilkCollectionDtl(int id)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            UnitOfWork unitOfWork = new UnitOfWork();
            var vLCMilkCollectionDtl = unitOfWork.VLCMilkCollectionDtlRepository.GetById(id);

            unitOfWork.VLCMilkCollectionDtlRepository.Delete(id);
            unitOfWork.SaveChanges();
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

        public void AddVLCMilkCollectionDtl(VLCMilkCollectionDtlDTO vlcMilkCollectionDtlDto)
        {
            throw new NotImplementedException();
        }

        public void UpdateVLCMilkCollectionDtl(VLCMilkCollectionDtlDTO vlcMilkCollectionDtlDto)
        {
            throw new NotImplementedException();
        }
    }
}

