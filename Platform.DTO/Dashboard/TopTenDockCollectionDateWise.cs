using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public class TopTenDockCollectionDateWise
    {
        public DateTime CollectionDateTime { get; set; }

        public ShiftEnum ShiftId { get; set; }

        public int TotalCan { get; set; }

        public int TotalRejectedCan { get; set; }

        public decimal TotalQuantity { get; set; }

        public decimal RejectedQuantity { get; set; }

        public decimal Amount { get; set; }

        public decimal Commission { get; set; }

        public decimal TotalAmount { get; set; }

        public string ReceiverName { get; set; }

        public string Comments { get; set; }
    }
}
