using Platform.DTO;
using Platform.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service
{
    public class VLCMilkCollectionConvertor
    {
        public static VLCMilkCollectionDTO ConvertToVLCMilkCollectionDto(VLCMilkCollection vlcMilkCollection)
        {
            VLCMilkCollectionDTO vlcMilkCollectionDTO = new VLCMilkCollectionDTO();
            vlcMilkCollectionDTO.VLCMilkCollectionId = vlcMilkCollection.VLCMilkCollectionId;
            vlcMilkCollectionDTO.VLCId = vlcMilkCollection.VLCId.GetValueOrDefault();
            vlcMilkCollectionDTO.TotalAmount = vlcMilkCollection.TotalAmount.GetValueOrDefault();
            vlcMilkCollectionDTO.CollectionDateTime = vlcMilkCollection.CollectionDateTime.GetValueOrDefault();
            vlcMilkCollectionDTO.CreatedBy = vlcMilkCollection.CreatedBy;
            vlcMilkCollectionDTO.CreatedDate = vlcMilkCollection.CreatedDate;
            vlcMilkCollectionDTO.CreatedBy = vlcMilkCollection.CreatedBy;
            vlcMilkCollectionDTO.CreatedDate = vlcMilkCollection.CreatedDate;
            vlcMilkCollectionDTO.IsDeleted = vlcMilkCollection.IsDeleted.GetValueOrDefault();
            vlcMilkCollectionDTO.ModifiedBy = vlcMilkCollection.ModifiedBy;
            vlcMilkCollectionDTO.ModifiedDate = vlcMilkCollection.ModifiedDate.GetValueOrDefault();
            vlcMilkCollectionDTO.CustomerName = vlcMilkCollection.Customer.CustomerName.ToString();
            vlcMilkCollectionDTO.VLCName = vlcMilkCollection.VLC.VLCName.ToString();
            return vlcMilkCollectionDTO;
        }

        public static void ConvertToVLCMilkCollectionEntity(ref VLCMilkCollection vlcMilkCollection, VLCMilkCollectionDTO vlcMilkCollectionDTO, bool isUpdate)
        {
            if (isUpdate)
                vlcMilkCollection.VLCMilkCollectionId = vlcMilkCollectionDTO.VLCMilkCollectionId;
            vlcMilkCollection.VLCId = vlcMilkCollectionDTO.VLCId;
            vlcMilkCollection.CustomerId = vlcMilkCollectionDTO.CustomerId;
            vlcMilkCollection.ShiftId = vlcMilkCollectionDTO.ShiftId;


        }

        public static void ConvertToVLCMilkCollectionDtlEntity(ref VLCMilkCollectionDtl vlcMilkCollectionDtl, VLCMilkCollectionDtlDTO vlcMilkCollectionDtlDTO, bool isUpdate)
        {
            if (isUpdate)
                vlcMilkCollectionDtl.VLCMilkCollectionDtlId = vlcMilkCollectionDtlDTO.VLCMilkCollectionDtlId;
            vlcMilkCollectionDtl.CLR = vlcMilkCollectionDtlDTO.CLR;
            vlcMilkCollectionDtl.FAT = vlcMilkCollectionDtlDTO.FAT;
            vlcMilkCollectionDtl.Qunatity = vlcMilkCollectionDtlDTO.Quantity;
            vlcMilkCollectionDtl.Amount = vlcMilkCollectionDtlDTO.Amount;
            vlcMilkCollectionDtl.ProductId = vlcMilkCollectionDtlDTO.ProductId;


        }


        public static VLCCustomerCollectionDTO ConvertToVLCCustomerCollectionDTO(VLCMilkCollection vLCMilkCollection)
        {
            VLCCustomerCollectionDTO vLCCustomerCollectionDTO = new VLCCustomerCollectionDTO();
            if (vLCMilkCollection != null && vLCMilkCollection.VLCMilkCollectionDtls.Count() > 0)
            {

                vLCCustomerCollectionDTO.VLCMilkCollectionId = vLCMilkCollection.VLCMilkCollectionId;
                vLCCustomerCollectionDTO.CollectionDateTime = vLCMilkCollection.CollectionDateTime.GetValueOrDefault();
                vLCCustomerCollectionDTO.CustomerId = vLCMilkCollection.CustomerId.GetValueOrDefault();
                vLCCustomerCollectionDTO.CustomerCodeId = vLCMilkCollection.Customer.CustomerCode;
                vLCCustomerCollectionDTO.CustomerName = vLCMilkCollection.Customer.CustomerName;
                vLCCustomerCollectionDTO.Shift = vLCMilkCollection.ShiftId == 1 ? "Morning" : "Evening";
                vLCCustomerCollectionDTO.TotalAmount = vLCMilkCollection.TotalAmount.GetValueOrDefault();
                vLCCustomerCollectionDTO.TotalQuantity = vLCMilkCollection.TotalQuantity.GetValueOrDefault();
                foreach (var dtl in vLCMilkCollection.VLCMilkCollectionDtls)
                {
                    vLCCustomerCollectionDTO.vLCCustomerCollectionDtlDTOList.Add(ConvertToCustomerCollectionDtlDTO(dtl));
                }


            }
            return vLCCustomerCollectionDTO;
        }

        public static VLCCustomerCollectionDtlDTO ConvertToCustomerCollectionDtlDTO(VLCMilkCollectionDtl vLCMilkCollectionDtl)
        {
            VLCCustomerCollectionDtlDTO vLCCustomerCollectionDtlDTO = new VLCCustomerCollectionDtlDTO();
            vLCCustomerCollectionDtlDTO.VLCMilkCollectionDtlId = vLCMilkCollectionDtl.VLCMilkCollectionDtlId;
            vLCCustomerCollectionDtlDTO.CLR = vLCMilkCollectionDtl.CLR.GetValueOrDefault();
            vLCCustomerCollectionDtlDTO.Fat = vLCMilkCollectionDtl.FAT.GetValueOrDefault();
            vLCCustomerCollectionDtlDTO.Quantity = vLCMilkCollectionDtl.Qunatity.GetValueOrDefault();
            vLCCustomerCollectionDtlDTO.Amount = vLCMilkCollectionDtl.Amount.GetValueOrDefault();
            vLCCustomerCollectionDtlDTO.ProductName = vLCMilkCollectionDtl.ProductId == 1 ? "Cow Milk" : "Buffalo Milk";
            return vLCCustomerCollectionDtlDTO;

        }
    }
}
