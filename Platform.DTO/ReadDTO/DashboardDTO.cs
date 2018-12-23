using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
   public class DashboardDTO
    {

        //get last 10 orders
        public List<ProductOrders> productOrderList { get; set; }

        //get today's Sales
        public List<ProductSales> productSalesList { get; set; }

        //Get  10 customer Walllet
        public List<CustomerBalance> customerBalanceList { get; set; }
    }
}
