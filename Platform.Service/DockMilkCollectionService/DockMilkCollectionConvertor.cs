using Platform.DTO;
using Platform.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service
{
    public class DockMilkCollectionConvertor
    {
        public static DockMilkCollectionDTO ConvertToDockMilkCollectionDto(DockMilkCollection dockMilkCollection)
        {
            DockMilkCollectionDTO dockMilkCollectionDTO = new DockMilkCollectionDTO();
            dockMilkCollectionDTO.DockMilkCollectionId = dockMilkCollection.DockMilkCollectionId;
            dockMilkCollectionDTO.VLCId = dockMilkCollection.VLCId;
            dockMilkCollectionDTO.VLCName = dockMilkCollection.VLC != null ? dockMilkCollection.VLC.VLCName : string.Empty;
            dockMilkCollectionDTO.CollectionDateTime = dockMilkCollection.CollectionDateTime;
            dockMilkCollectionDTO.TotalQuantity = dockMilkCollection.TotalQuantity;
            dockMilkCollectionDTO.ShiftId = (ShiftEnum)dockMilkCollection.ShiftId;
            dockMilkCollectionDTO.CollectionShift = dockMilkCollectionDTO.ShiftId.ToString();
            dockMilkCollectionDTO.ReceiverName = dockMilkCollection.ReceiverName;
            dockMilkCollectionDTO.RejectedQuantity = dockMilkCollection.RejectedQuantity;
            dockMilkCollectionDTO.TotalCan = dockMilkCollection.TotalCan.HasValue ? dockMilkCollection.TotalCan.Value : 0;
            dockMilkCollectionDTO.TotalRejectedCan = dockMilkCollection.TotalRejectedCan;
            dockMilkCollectionDTO.Amount = dockMilkCollection.Amount;
            dockMilkCollectionDTO.Commission = dockMilkCollection.Commission;
            dockMilkCollectionDTO.TotalAmount = dockMilkCollection.TotalAmount;
            dockMilkCollectionDTO.CreatedBy = dockMilkCollection.CreatedBy;
            dockMilkCollectionDTO.CreatedDate = dockMilkCollection.CreatedDate;
            dockMilkCollectionDTO.Comments = dockMilkCollection.Comments;
            dockMilkCollectionDTO.IsDeleted = dockMilkCollection.IsDeleted.GetValueOrDefault();
            dockMilkCollectionDTO.ModifiedBy = dockMilkCollection.ModifiedBy;
            dockMilkCollectionDTO.ModifiedDate = dockMilkCollection.ModifiedDate.GetValueOrDefault();
            dockMilkCollectionDTO.BillNumber = dockMilkCollection.BillNumber;
            
            if (dockMilkCollection.DockMilkCollectionDtls != null && dockMilkCollection.DockMilkCollectionDtls.Count() > 0)
            {
                dockMilkCollectionDTO.dockMilkCollectionList = new List<DockMilkCollectionDtlDTO>();
                foreach (DockMilkCollectionDtl dockMilkCollectionDtl in dockMilkCollection.DockMilkCollectionDtls)
                {
                    dockMilkCollectionDTO.dockMilkCollectionList.Add(ConvertToDockMilkCollectionDtlDTO(dockMilkCollectionDtl));
                }
            }
            return dockMilkCollectionDTO;

        }

        public static DockMilkCollectionDtlDTO ConvertToDockMilkCollectionDtlDTO(DockMilkCollectionDtl dockMilkCollectionDtl)
        {
            DockMilkCollectionDtlDTO dockMilkCollectionDtlDTO = new DockMilkCollectionDtlDTO();
            dockMilkCollectionDtlDTO.DockMilkCollectionDtlId = dockMilkCollectionDtl.DockMilkCollectionDtlI;
            dockMilkCollectionDtlDTO.CLR = dockMilkCollectionDtl.CLR.GetValueOrDefault();
            dockMilkCollectionDtlDTO.FAT = dockMilkCollectionDtl.FAT.GetValueOrDefault();
            dockMilkCollectionDtlDTO.Quantity = dockMilkCollectionDtl.Quantity.GetValueOrDefault();
            dockMilkCollectionDtlDTO.TotalAmount = dockMilkCollectionDtl.TotalAmount.GetValueOrDefault();
            dockMilkCollectionDtlDTO.ProductId = (MilkTypeEnum)dockMilkCollectionDtl.ProductId;
            dockMilkCollectionDtlDTO.Comments = dockMilkCollectionDtl.Comments;
            dockMilkCollectionDtlDTO.RatePerUnit = dockMilkCollectionDtl.RatePerUnit.GetValueOrDefault();
            dockMilkCollectionDtlDTO.RejectedQuantity = dockMilkCollectionDtl.RejectedQuantity.GetValueOrDefault();
            dockMilkCollectionDtlDTO.RejectedReason = dockMilkCollectionDtl.RejectedReason;
            dockMilkCollectionDtlDTO.TotalCan = dockMilkCollectionDtl.TotalCan.HasValue ? dockMilkCollectionDtl.TotalCan.Value : 0;
            dockMilkCollectionDtlDTO.TotalRejectedCan = dockMilkCollectionDtl.TotalRejectedCan.GetValueOrDefault();
            dockMilkCollectionDtlDTO.Amount = dockMilkCollectionDtl.Amount;
            dockMilkCollectionDtlDTO.Commission = dockMilkCollectionDtl.Commission;


            return dockMilkCollectionDtlDTO;

        }

        public static void ConvertToDockMilkCollectionEntity(ref DockMilkCollection DockMilkCollection, DockMilkCollectionDTO DockMilkCollectionDTO, bool isUpdate)
        {
            if (isUpdate)
                DockMilkCollection.DockMilkCollectionId = DockMilkCollectionDTO.DockMilkCollectionId;
            DockMilkCollection.VLCId = DockMilkCollectionDTO.VLCId;
            if (DockMilkCollectionDTO.ShiftId > 0)
                DockMilkCollection.ShiftId = (int)DockMilkCollectionDTO.ShiftId;
            if (string.IsNullOrWhiteSpace(DockMilkCollectionDTO.Comments) == false)
                DockMilkCollection.Comments = DockMilkCollectionDTO.Comments;
            if (string.IsNullOrWhiteSpace(DockMilkCollectionDTO.ReceiverName) == false)
                DockMilkCollection.ReceiverName = DockMilkCollectionDTO.ReceiverName;



        }

        public static void ConvertToDockMilkCollectionDtlEntity(ref DockMilkCollectionDtl DockMilkCollectionDtl, DockMilkCollectionDtlDTO DockMilkCollectionDtlDTO, bool isUpdate)
        {
            if (isUpdate)
                DockMilkCollectionDtl.DockMilkCollectionDtlI = DockMilkCollectionDtlDTO.DockMilkCollectionDtlId;
            if(DockMilkCollectionDtlDTO.CLR>0)
            DockMilkCollectionDtl.CLR = DockMilkCollectionDtlDTO.CLR;
            if (DockMilkCollectionDtlDTO.FAT > 0)
                DockMilkCollectionDtl.FAT = DockMilkCollectionDtlDTO.FAT;
            if (DockMilkCollectionDtlDTO.Quantity > 0)
                DockMilkCollectionDtl.Quantity = DockMilkCollectionDtlDTO.Quantity;
            if (DockMilkCollectionDtlDTO.RejectedQuantity > 0)
                DockMilkCollectionDtl.RejectedQuantity = DockMilkCollectionDtlDTO.RejectedQuantity;
            if (DockMilkCollectionDtlDTO.TotalCan > 0)
                DockMilkCollectionDtl.TotalCan = DockMilkCollectionDtlDTO.TotalCan;
            if (DockMilkCollectionDtlDTO.TotalRejectedCan > 0)
                DockMilkCollectionDtl.TotalRejectedCan = DockMilkCollectionDtlDTO.TotalRejectedCan;
            DockMilkCollectionDtl.ProductId = (int)DockMilkCollectionDtlDTO.ProductId;
            DockMilkCollectionDtl.TotalAmount = DockMilkCollectionDtlDTO.TotalAmount;
           
            if (string.IsNullOrWhiteSpace(DockMilkCollectionDtlDTO.Comments) == false)
                DockMilkCollectionDtl.Comments = DockMilkCollectionDtlDTO.Comments;

            if (string.IsNullOrWhiteSpace(DockMilkCollectionDtlDTO.RejectedReason) == false)
                DockMilkCollectionDtl.RejectedReason = DockMilkCollectionDtlDTO.RejectedReason;
        }


        //public static VLCCustomerCollectionDTO ConvertToVLCCustomerCollectionDTO(DockMilkCollection vLCMilkCollection)
        //{
        //    VLCCustomerCollectionDTO vLCCustomerCollectionDTO = new VLCCustomerCollectionDTO();
        //    if (vLCMilkCollection != null && vLCMilkCollection.DockMilkCollectionDtls.Count() > 0)
        //    {

        //        vLCCustomerCollectionDTO.DockMilkCollectionId = vLCMilkCollection.DockMilkCollectionId;
        //        vLCCustomerCollectionDTO.CollectionDateTime = vLCMilkCollection.CollectionDateTime.GetValueOrDefault();
        //        vLCCustomerCollectionDTO.CustomerId = vLCMilkCollection.CustomerId.GetValueOrDefault();
        //        vLCCustomerCollectionDTO.CustomerCodeId = vLCMilkCollection.Customer.CustomerCode;
        //        vLCCustomerCollectionDTO.CustomerName = vLCMilkCollection.Customer.CustomerName;
        //        vLCCustomerCollectionDTO.Shift = vLCMilkCollection.ShiftId == 1 ? "Morning" : "Evening";
        //        vLCCustomerCollectionDTO.TotalAmount = vLCMilkCollection.TotalAmount.GetValueOrDefault();
        //        vLCCustomerCollectionDTO.TotalQuantity = vLCMilkCollection.TotalQuantity.GetValueOrDefault();
        //        foreach (var dtl in vLCMilkCollection.DockMilkCollectionDtls)
        //        {
        //            vLCCustomerCollectionDTO.vLCCustomerCollectionDtlDTOList.Add(ConvertToCustomerCollectionDtlDTO(dtl));
        //        }


        //    }
        //    return vLCCustomerCollectionDTO;
        //}


    }
}
