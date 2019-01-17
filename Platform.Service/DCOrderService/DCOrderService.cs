using Platform.DTO;
using Platform.Sql;
using Platform.Utilities.ExceptionHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service
{
    public class DCOrderService : IDCOrderService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();


        public ResponseDTO UpdateDCOrderStatus(DCOrderStatusDTO dCOrderStatusDTO)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            var dcOrder = unitOfWork.DCOrderRepository.GetDCOrderByOrderId(dCOrderStatusDTO.DCOrderId);
            if(dcOrder ==null)
                throw new PlatformModuleException("DC Order Detail Not Found");
            dcOrder.OrderStatusId = (int)dCOrderStatusDTO.OrderStatus;
            if (dCOrderStatusDTO.DeliveryExpectedDate != DateTime.MinValue)
                dcOrder.DeliveryExpectedDate = dCOrderStatusDTO.DeliveryExpectedDate;
            unitOfWork.DCOrderRepository.Update(dcOrder);
            unitOfWork.SaveChanges();
            responseDTO.Status = true;
            responseDTO.Message = "DC Order Status is Updated";
            responseDTO.Data= this.GetOrderDetailsByOrderId(dcOrder.DCOrderId);
            return responseDTO;
        }
        public ResponseDTO AddDCOrder(CreateDCOrderDTO dCOrderDTO)
        {

            ResponseDTO responseDTO = new ResponseDTO();
            DistributionCenter distributionCenter = unitOfWork.DistributionCenterRepository.GetById(dCOrderDTO.DCId);
            DCOrder dcOrder = new DCOrder();
            dcOrder.DCOrderId = unitOfWork.DashboardRepository.NextNumberGenerator("DCOrder");
            dcOrder.DCOrderNumber = distributionCenter.DCCode + "OD" + dcOrder.DCOrderId.ToString();
            dcOrder.DCId = dCOrderDTO.DCId;
            dcOrder.OrderAddressId = distributionCenter.DCAddresses.FirstOrDefault().DCAddressId;
            dcOrder.OrderDate = DateTime.Now;
            dcOrder.CreatedDate = DateTime.Now;
            dcOrder.ModifiedDate = DateTime.Now;
            dcOrder.CreatedBy = dcOrder.ModifiedBy = distributionCenter.AgentName;
            dcOrder.IsDeleted = false;
            dcOrder.OrderStatusId = (int)OrderStatus.Placed;
            dcOrder.DeliveryExpectedDate = DateTime.Now.AddDays(1);
            dcOrder.OrderComments = dCOrderDTO.OrderComments;
            if (dCOrderDTO.CreateDCOrderDtlList != null)
            {
                foreach (var dcOrderDtlDTO in dCOrderDTO.CreateDCOrderDtlList)
                {
                    //  this.CheckForExistingCollectionDetailByDateShiftProduct(vLCMilkCollection.CollectionDateTime.Value.Date, vLCMilkCollectionDTO.ShiftId, vlcCollectionDtlDTO.ProductId, vLCMilkCollectionDTO.CustomerId);
                    DCOrderDtl dCOrderDtl = new DCOrderDtl();
                    dCOrderDtl.DCOrderDtlId = unitOfWork.DashboardRepository.NextNumberGenerator("DCOrderDtl");
                    dCOrderDtl.DCOrderId = dcOrder.DCOrderId;
                    DCOrderConvertor.ConvertToDCOrderDtlEntity(ref dCOrderDtl, dcOrderDtlDTO, false);
                    unitOfWork.DCOrderDtlRepository.Add(dCOrderDtl);
                }

                dcOrder.OrderTotalPrice = dCOrderDTO.CreateDCOrderDtlList.Sum(s => s.TotalPrice);
                dcOrder.TotalOrderQuantity = dCOrderDTO.CreateDCOrderDtlList.Sum(s => s.QuantityOrdered);
            }
            else
            {
                throw new PlatformModuleException("DC Order Detail Not Found");
            }
            unitOfWork.DCOrderRepository.Add(dcOrder);
            UpdateOrderPaymentDetailsForOrder(distributionCenter, dcOrder);
            unitOfWork.SaveChanges();
            responseDTO.Status = true;
            responseDTO.Message = String.Format("DC Order Placed Successfully ");
            responseDTO.Data = this.GetOrderDetailsByOrderId(dcOrder.DCOrderId);

            return responseDTO;


        }

        public void UpdateDCWalletForOrder(int dcId,decimal orderAmount,bool isCredit)
        {
            var dcWallet = unitOfWork.DCWalletRepository.GetByDCId(dcId);
            if(isCredit)
            dcWallet.WalletBalance -= orderAmount;
            else
                dcWallet.WalletBalance += orderAmount;
            dcWallet.AmountDueDate = dcWallet.AmountDueDate.AddDays(10);
            unitOfWork.DCWalletRepository.Update(dcWallet);
        }

        public void UpdateOrderPaymentDetailsForOrder(DistributionCenter distributionCenter,DCOrder dCOrder)
        {
            DCPaymentDetail dCPaymentDetail = new DCPaymentDetail();
            dCPaymentDetail.DCPaymentId = unitOfWork.DashboardRepository.NextNumberGenerator("DCPaymentDetail");
            dCPaymentDetail.CreatedDate=dCPaymentDetail.ModifiedDate = DateTime.Now;
            dCPaymentDetail.CreatedBy = dCPaymentDetail.ModifiedBy= distributionCenter.AgentName;
            dCPaymentDetail.DCId = distributionCenter.DCId;
            dCPaymentDetail.DCOrderId = dCOrder.DCOrderId;
            dCPaymentDetail.IsDeleted = false;
            dCPaymentDetail.PaymentComments = "Initial Order Amount";
            dCPaymentDetail.PaymentDate = DateTime.Now;
            dCPaymentDetail.PaymentDrAmount = dCOrder.OrderTotalPrice;
            unitOfWork.DCPaymentDetailRepository.Add(dCPaymentDetail);
            UpdateDCWalletForOrder(distributionCenter.DCId, dCOrder.OrderTotalPrice, false);
        }

        public ResponseDTO GetDCOrdersById(int dcId)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            responseDTO.Status = true;
            responseDTO.Message = String.Format("DC Order Details");
            var dcOrders = unitOfWork.DCOrderRepository.GetAllDCOrdersByDCId(dcId);
           
            List<DCOrderDTO> dcOrderDTOList = new List<DCOrderDTO>();
            foreach(var dcOrder in dcOrders)
            {
                dcOrderDTOList.Add(DCOrderConvertor.ConvertToDCOrderDto(dcOrder,unitOfWork.ImagePath));
            }
            responseDTO.Data = dcOrderDTOList;
            return responseDTO;
        }

        public DCOrderDTO GetOrderDetailsByOrderId(int orderId)
        {
            
            var dcOrder = unitOfWork.DCOrderRepository.GetDCOrderByOrderId(orderId);
         
            DCOrderDTO dCOrderDTO = DCOrderConvertor.ConvertToDCOrderDto(dcOrder, unitOfWork.ImagePath);
       
            return dCOrderDTO;
        }

        public ResponseDTO DeleteDCOrder(int id)
        {
            throw new NotImplementedException();
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


        public List<DCOrderDTO> GetAllDCOrders()
        {
            throw new NotImplementedException();
        }

      

        public ResponseDTO UpdateDCOrder(DCOrderDTO dCPaymentDTO)
        {
            throw new NotImplementedException();
        }

        public ResponseDTO GetDCOrdersByOrderStatus(int dcId, string orderStatus)
        {
            throw new NotImplementedException();
        }
    }
}
