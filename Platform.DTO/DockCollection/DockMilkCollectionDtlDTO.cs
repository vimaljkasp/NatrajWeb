using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public class DockMilkCollectionDtlDTO
    {
        public int DockMilkCollectionDtlId { get; set; }
        public int DockMilkCollectionId { get; set; }
        public int ProductId { get; set; }
        public int TotalCan { get; set; }
        public decimal RejectedQuantity { get; set; }
        public int TotalRejectedCan { get; set; }
        public decimal Quantity { get; set; }
        public decimal RatePerUnit { get; set; }
        public Nullable<decimal> FAT { get; set; }
        public Nullable<decimal> CLR { get; set; }
       
        public decimal? TotalAmount { get; set; }
        public string RejectedReason { get; set; }
        public string Comments { get; set; }
        public string ProductName { get; set; }


    }
}
