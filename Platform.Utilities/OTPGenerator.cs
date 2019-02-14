using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Utilities
{
    public static class OTPGenerator
    {

        public static string GetSixDigitOTP()
        {
            Random generator = new Random();
            String number= generator.Next(0, 999999).ToString("D6");
            return number;
        }

    }
}
