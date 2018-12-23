using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
   public class CustomerBalance
    {

        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public decimal PendingAmount { get; set; }

        public string MobileNumber { get; set; }
    }
}
