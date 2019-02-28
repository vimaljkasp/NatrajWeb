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
        public Nullable<int> DockMilkCollectionId { get; set; }
        public int ProductId { get; set; }
        public Nullable<int> TotalCan { get; set; }
        public Nullable<decimal> RejectedQuantity { get; set; }
        public Nullable<int> TotalRejectedCan { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> RatePerUnit { get; set; }
        public Nullable<decimal> FAT { get; set; }
        public Nullable<decimal> CLR { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public string RejectedReason { get; set; }
        public string Comments { get; set; }


    }
}
