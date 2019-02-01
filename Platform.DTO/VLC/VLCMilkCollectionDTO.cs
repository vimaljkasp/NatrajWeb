using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public class VLCMilkCollectionDTO
    {
        public int VLCMilkCollectionId { get; set; }
        public int VLCId { get; set; }
        public string VLCName { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public Nullable<decimal> TotalQuantity { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
       public DateTime CollectionDateTime { get; set; }
        public string CollectionShift { get; set; }
        public int ShiftId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
         public List<VLCMilkCollectionDtlDTO> vLCMilkCollectionDtlDTOList { get; set; }
 
    }
}
