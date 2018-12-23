using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public class ProductSales
    {
        public int SalesId { get; set; }

        public DateTime SalesDate { get; set; }

        public decimal SaleQuantity { get; set; }

        public decimal SalesAmount { get; set; }

        public int ProductMappingId { get; set; }

        public string ProductName { get; set; }
    }
}
