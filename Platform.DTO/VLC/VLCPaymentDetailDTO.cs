using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
   public class VLCPaymentDetailDTO
    {
        public int VLCPaymentId { get; set; }
        public int VLCId { get; set; }
        public int DockMilkCollectionId { get; set; }
        public Nullable<decimal> PaymentCrAmount { get; set; }
        public Nullable<decimal> PaymentDrAmount { get; set; }
        public System.DateTime PaymentDate { get; set; }
        public string PaymentReceivedBy { get; set; }
        public string PaymentMode { get; set; }
        public string PaymentComments { get; set; }
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    }
}
