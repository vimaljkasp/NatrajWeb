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

        public int Id { get; set; }

        public string VLCCode { get; set; }

        public LoginType LoginType { get; set; }
    }


    public class ResetPasswordDTO
    {
        public string OTP { get; set; }

        public int Id { get; set; }

        public string Password { get; set; }

        public LoginType LoginType { get; set; }
    }

    public class ForgotPasswordDTO
    {
        public LoginType LoginType { get; set; }

        public string UserName { get; set; }
    }
}
