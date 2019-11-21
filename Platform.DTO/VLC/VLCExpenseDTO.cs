using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public class VLCExpenseDTO
    {
        public int VLCExpenseId { get; set; }
        [DisplayName("VLC")]
        public int VLCId { get; set; }

        [DisplayName("VLC Name")]
        public string VLCName { get; set; }

        [DisplayName("Expense Reason")]
        public VLCExpenseEnum ExpenseReason { get; set; }
        [DisplayName("Credit Amount")]
        public decimal PaymentCrAmount { get; set; }
        [DisplayName("Debit Amount")]
        public decimal PaymentDrAmount { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Expense Date")]
        public DateTime? ExpenseDate { get; set; }
        [DisplayName("Expense Commnents")]
        public string ExpenseComments { get; set; }
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
