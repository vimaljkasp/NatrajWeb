using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public class ProductOrders
    {

        public int OrderId { get; set; }

        public string OrderNumber { get; set; }

        public int ProductMappingId { get; set; }

        public int ProductOrderDetailId { get; set; }

        public string ProductName { get; set; }

        public decimal Quantity { get; set; }

        public decimal Amount { get; set; }

        public String OrderStatus { get; set; }

        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerMobileNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public string OrderComments { get; set; }

        public string OrderAddress { get; set; }

        public string OrderPriority { get; set; }

        public DateTime ExpectedDeliveryDate { get; set; }
    }

   
}
