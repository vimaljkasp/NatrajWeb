using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public class CustomerDetailsDTO
    {
        
        public CustomerDto customerProfileDetails { get; set; }
        public CustomerBankDTO customerBankDetails { get; set; }
        public CustomerAgricultureDTO customerAgricultureDetails { get; set; }
    }
}
