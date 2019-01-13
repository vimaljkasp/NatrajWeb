using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public class DCOrderStatusDTO
    {

        public int DCOrderId { get; set; }

        public DateTime DeliveryExpectedDate { get; set; }

        public OrderStatus OrderStatus { get; set; }
    }
}
