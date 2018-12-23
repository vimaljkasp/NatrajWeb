using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
   public class CustomerBankDTO
    {
        public int CustomerBankId { get; set; }
        public int CustomerId { get; set; }
        public int VLCId { get; set; }
        public string IFSCCode { get; set; }
        public string AccountNo { get; set; }
        public string Branch { get; set; }
        public string BankName { get; set; }
        public string AccountHolderName { get; set; }
        public string UPIId { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
