using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public class VLCMilkCollectionDtlDTO
    {
        public int VLCMilkCollectionDtlId { get; set; }
        public Nullable<int> VLCMilkCollectionId { get; set; }
        public int ProductId { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> RatePerUnit { get; set; }
        public Nullable<decimal> FAT { get; set; }
        public Nullable<decimal> CLR { get; set; }
        public Nullable<decimal> Amount { get; set; }
 

    }
}
