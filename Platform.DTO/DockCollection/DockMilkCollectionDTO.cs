using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public class DockMilkCollectionDTO
    {
        public int DockMilkCollectionId { get; set; }
        public int VLCId { get; set; }
        public int DockMilkId { get; set; }
        public Nullable<int> TotalCan { get; set; }
        public Nullable<int> TotalRejectedCan { get; set; }
        public Nullable<decimal> TotalQuantity { get; set; }
        public Nullable<decimal> RejectedQuantity { get; set; }
        public decimal Amount { get; set; }
        public Nullable<decimal> Commission { get; set; }
        public decimal TotalAmount { get; set; }
       
        public DateTime CollectionDateTime { get; set; }
        public string CollectionShift { get; set; }
        public int ShiftId { get; set; }
        public string BillNumber { get; set; }
        public string ReceiverName { get; set; }
        public string Comments { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public List<DockMilkCollectionDtlDTO> dockMilkCollectionList { get; set; }


    }
}
