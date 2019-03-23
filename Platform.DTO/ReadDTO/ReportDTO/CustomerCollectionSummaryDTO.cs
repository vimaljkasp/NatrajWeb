using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public class CustomerCollectionSummaryDTO
    {
        public int CustomerId { get; set; }
   

        public List<CustomerCollectionSummaryDtlDTO> customerCollectionSummaryDtlDTOList { get; set; }

    }

    public class CustomerCollectionSummaryDtlDTO
    {
        public DateTime CollectionDate { get; set; }
        public decimal TotalQuantity { get; set; }
        public decimal TotalAmount { get; set; }
        public string Shift { get; set; }
        public string MiilkType { get; set; }
    }
}
