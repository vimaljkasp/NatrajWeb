using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
   public class ChangePasswordDTO
    {
        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }

        public int VLCId { get; set; }

        public string VLCCode { get; set; }

        public LoginType LoginType { get; set; }
    }
}
