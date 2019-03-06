using Platform.DTO;
using Platform.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service
{
    public class DCOrderConvertor
    {
        public static DCOrderDTO ConvertToDCOrderDto(DCOrder dCOrder,string path)
        {
            DCOrderDTO dCOrderDTO = new DCOrderDTO();
            dCOrderDTO.DCId = dCOrder.DCId;
            dCOrderDTO.DCOrderId = dCOrder.DCOrderId;
            dCOrderDTO.DCOrderNumber = dCOrder.DCOrderNumber;
            dCOrderDTO.DeliveryExpectedDate = dCOrder.DeliveryExpectedDate.GetValueOrDefault();
            dCOrderDTO.OrderComments = dCOrder.OrderComments;
            dCOrderDTO.OrderDate = dCOrder.OrderDate;
            dCOrderDTO.OrderTotalPrice = dCOrder.OrderTotalPrice;
            dCOrderDTO.TotalOrderQuantity = dCOrder.TotalOrderQuantity;
            dCOrderDTO.TotalActualQuantity = dCOrder.TotalActualQuantity.GetValueOrDefault();
            dCOrderDTO.OrderStatus = ((OrderStatus)dCOrder.OrderStatusId).ToString();
            dCOrderDTO.DCName = dCOrder.DistributionCenter != null ? dCOrder.DistributionCenter.DCName : string.Empty;
            if (dCOrder.DCAddress != null)
             dCOrderDTO.dCAddressDTO =DCAddressConvertor.ConvertToDCAddressDTO(dCOrder.DCAddress);
            if (dCOrder.DCOrderDtls != null)
            {
                dCOrderDTO.dcOrderDtlList = new List<DCOrderDtlDTO>();
                foreach (var dcorderDtl in dCOrder.DCOrderDtls)
                    dCOrderDTO.dcOrderDtlList.Add(ConvertToDCOrderDtlDto(dcorderDtl,path));
            }
            return dCOrderDTO;

        }

        public static DCOrderDtlDTO ConvertToDCOrderDtlDto(DCOrderDtl dCOrderDtl,string path)
        {
            DCOrderDtlDTO dCOrderDtlDTO = new DCOrderDtlDTO();
            dCOrderDtlDTO.DCOrderDtlId = dCOrderDtl.DCOrderDtlId;
            dCOrderDtlDTO.DCOrderId = dCOrderDtl.DCOrderId;
            dCOrderDtlDTO.ProductId = dCOrderDtl.ProductId;
            dCOrderDtlDTO.ProductName = dCOrderDtl.Product.Name;
            dCOrderDtlDTO.ProductDescription= dCOrderDtl.Product.Description;
            dCOrderDtlDTO.ProductImageUrl = Path.Combine(path, "PROD" + dCOrderDtl.ProductId.ToString() + ".jpg");
            dCOrderDtlDTO.QuantityOrdered = dCOrderDtl.QuantityOrdered;
            dCOrderDtlDTO.ActualQuantity = dCOrderDtl.ActualQuantity;
            dCOrderDtlDTO.TotalPrice = dCOrderDtl.OrderTotalPrice;
            dCOrderDtlDTO.UnitPrice = dCOrderDtl.UnitPrice.GetValueOrDefault();
            return dCOrderDtlDTO;
        }
            //public static void ConvertToDCOrderEntity(ref DCOrder dcOrder, CreateDCOrderDTO dcOrderDTO, bool isUpdate)
            //{

            //    dcOrder.dco = dcOrderDTO.VLCId;
            //    dcOrder.CustomerId = dcOrderDTO.CustomerId;
            //    dcOrder.OrderComments = dcOrderDTO.ShiftId;


            //}

            public static void ConvertToDCOrderDtlEntity(ref DCOrderDtl dcOrderDtl, CreateDCOrderDtlDTO dCOrderDtlDTO, bool isUpdate)
        {
            if(dCOrderDtlDTO.ActualQuantity>0)
              dcOrderDtl.ActualQuantity = dCOrderDtlDTO.ActualQuantity;
            else
                dcOrderDtl.ActualQuantity= dCOrderDtlDTO.QuantityOrdered;

            if (dCOrderDtlDTO.QuantityOrdered > 0)
                dcOrderDtl.QuantityOrdered = dCOrderDtlDTO.QuantityOrdered;
          
            
            if (dCOrderDtlDTO.ProductId > 0)
                dcOrderDtl.ProductId = dCOrderDtlDTO.ProductId;

            if (dCOrderDtlDTO.UnitPrice > 0)
                dcOrderDtl.UnitPrice = dCOrderDtlDTO.UnitPrice;
            if (dCOrderDtlDTO.TotalPrice > 0)
                dcOrderDtl.OrderTotalPrice = dCOrderDtlDTO.TotalPrice;



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
