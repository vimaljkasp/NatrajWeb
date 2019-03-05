using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public interface IDCOrderService
    {
        List<DCOrderDTO> GetAllDCOrders();

      ResponseDTO GetDCOrdersById(int dcId);

        ResponseDTO GetDCOrdersByOrderStatus(int dcId,string orderStatus);

        ResponseDTO AddDCOrder(CreateDCOrderDTO dcOrderDTO);

        ResponseDTO UpdateDCOrder(CreateDCOrderDTO dCPaymentDTO);

        ResponseDTO GetOrderDetailsByOrderId(int orderId);

        ResponseDTO UpdateDCOrderStatus(DCOrderStatusDTO dCPaymentDTO);

        ResponseDTO DeleteDCOrder(int id);

    }
}
