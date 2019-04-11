using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
   public class VLCPaymentDTO
    {
        public int VLCPaymentId { get; set; }
        public int VLCId { get; set; }
        public int DockMilkCollectionId { get; set; }
        public decimal PaymentCrAmount { get; set; }
        public decimal PaymentDrAmount { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime PaymentDate { get; set; }
        public string PaymentReceivedBy { get; set; }
        public PaymentModeEnum PaymentMode { get; set; }
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
