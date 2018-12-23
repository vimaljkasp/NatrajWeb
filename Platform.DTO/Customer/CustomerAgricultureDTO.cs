using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
   public class CustomerAgricultureDTO
    {
        public int CustAgriId { get; set; }
        public int CustomerId { get; set; }
        public int VLCId { get; set; }
        public Nullable<int> NoOfCow { get; set; }
        public Nullable<int> NoOfBuffelo { get; set; }
        public Nullable<decimal> AgricultureLand { get; set; }
        public Nullable<decimal> MilkProductionQty { get; set; }
        public string Comments { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
