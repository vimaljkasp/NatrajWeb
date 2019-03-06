using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public class VLCWalletDTO
    {
        public int WalletId { get; set; }
        public int VLCId { get; set; }
        public decimal WalletBalance { get; set; }
        public System.DateTime AmountDueDate { get; set; }
    }
}
