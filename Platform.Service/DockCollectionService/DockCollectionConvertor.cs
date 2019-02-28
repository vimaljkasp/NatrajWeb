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
        public static DockMilkCollectionDTO ConvertToDockMilkCollectionDto(DockMilkCollection DockMilkCollection)
        {
            DockMilkCollectionDTO DockMilkCollectionDTO = new DockMilkCollectionDTO();
            //DockMilkCollectionDTO.DockMilkCollectionId = DockMilkCollection.DockMilkMilkCollectionId;
            //DockMilkCollectionDTO.VLCId = DockMilkCollection.VLCId.GetValueOrDefault();
            //DockMilkCollectionDTO.TotalAmount = DockMilkCollection.TotalAmount.GetValueOrDefault();
            //DockMilkCollectionDTO.CollectionDateTime = DockMilkCollection.CollectionDateTime.GetValueOrDefault();
            //DockMilkCollectionDTO.CreatedBy = DockMilkCollection.CreatedBy;
            //DockMilkCollectionDTO.CreatedDate = DockMilkCollection.CreatedDate;
            //DockMilkCollectionDTO.CreatedBy = DockMilkCollection.CreatedBy;
            //DockMilkCollectionDTO.CreatedDate = DockMilkCollection.CreatedDate;
            //DockMilkCollectionDTO.IsDeleted = DockMilkCollection.IsDeleted.GetValueOrDefault();
            //DockMilkCollectionDTO.ModifiedBy = DockMilkCollection.ModifiedBy;
            //DockMilkCollectionDTO.ModifiedDate = DockMilkCollection.ModifiedDate.GetValueOrDefault();

            return DockMilkCollectionDTO;

        }

        public static void ConvertToDockMilkCollectionEntity(ref DockMilkCollection DockMilkCollection, DockMilkCollectionDTO DockMilkCollectionDTO, bool isUpdate)
        {
            if (isUpdate)
                DockMilkCollection.DockMilkCollectionId = DockMilkCollectionDTO.DockMilkCollectionId;
            //DockMilkCollection.VLCId = DockMilkCollectionDTO.VLCId;
            //DockMilkCollection.CustomerId = DockMilkCollectionDTO.CustomerId;
            //DockMilkCollection.ShiftId = DockMilkCollectionDTO.ShiftId;


        }

        //public static void ConvertToDockMilkCollectionDtlEntity(ref DockMilkCollectionDtl DockMilkCollectionDtl, DockMilkCollectionDtlDTO DockMilkCollectionDtlDTO, bool isUpdate)
        //{
        //    if (isUpdate)
        //        DockMilkCollectionDtl.Do = DockMilkCollectionDtlDTO.DockMilkCollectionDtlId;
        //    //DockMilkCollectionDtl.CLR = DockMilkCollectionDtlDTO.CLR;
        //    //DockMilkCollectionDtl.FAT = DockMilkCollectionDtlDTO.FAT;
        //    //DockMilkCollectionDtl.Qunatity = DockMilkCollectionDtlDTO.Quantity;
        //    //DockMilkCollectionDtl.Amount = DockMilkCollectionDtlDTO.Amount;
        //    //DockMilkCollectionDtl.ProductId = DockMilkCollectionDtlDTO.ProductId;


        //}


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

        //public static VLCCustomerCollectionDtlDTO ConvertToCustomerCollectionDtlDTO(DockMilkCollectionDtl vLCMilkCollectionDtl)
        //{
        //    VLCCustomerCollectionDtlDTO vLCCustomerCollectionDtlDTO = new VLCCustomerCollectionDtlDTO();
        //    vLCCustomerCollectionDtlDTO.DockMilkCollectionDtlId = vLCMilkCollectionDtl.DockMilkCollectionDtlId;
        //    vLCCustomerCollectionDtlDTO.CLR = vLCMilkCollectionDtl.CLR.GetValueOrDefault();
        //    vLCCustomerCollectionDtlDTO.Fat = vLCMilkCollectionDtl.FAT.GetValueOrDefault();
        //    vLCCustomerCollectionDtlDTO.Quantity = vLCMilkCollectionDtl.Qunatity.GetValueOrDefault();
        //    vLCCustomerCollectionDtlDTO.Amount = vLCMilkCollectionDtl.Amount.GetValueOrDefault();
        //    vLCCustomerCollectionDtlDTO.ProductName = vLCMilkCollectionDtl.ProductId == 1 ? "Cow Milk" : "Buffalo Milk";
        //    return vLCCustomerCollectionDtlDTO;

        //}
    }
}
