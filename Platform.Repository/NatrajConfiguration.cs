using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Repository
{
   public class NatrajConfigurationSettings
    {
        public string ImagePath { get; set; }

        public string MilkRatePath { get; set; }

        public string SenderMobileNumber { get; set; }

        public string SMSServiceUserName { get; set; }

        public string SMSServicePassword { get; set; }

        public string VLCCollectionMessage { get; set; }

        public string DockCollectionMessage { get; set; }

        public string ForgotPasswordOTPMessage { get; set; }

        public bool IsDockCommonCommissionEnabled { get; set; }

        public decimal DockCommonCommission { get; set; }
    }
}
