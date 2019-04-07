using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Repository
{
    public static class NatrajConfigurationHelper
    {
        public static ConfigurationRepository configurationRepository = new ConfigurationRepository(new Sql.PlatformDBEntities());
        public static string ImagePath = @"http://service.natrajdairy.com/img/";

        public static string MilkRatePath = @"http://service.natrajdairy.com/MilkRate/";


        public static string SenderMobileNumber = configurationRepository.GetConfiguration("SMS", "SenderNumber", "9566812835");
        public static string SMSServiceUserName = configurationRepository.GetConfiguration("SMS", "SMSServiceUserName", "adam");
        public static string SMSServicePassword = configurationRepository.GetConfiguration("SMS", "SMSServiceUserName", "12345");
        public static bool IsDockCommonCommissionEnabled = Convert.ToBoolean(configurationRepository.GetConfiguration("DockMilkCollection", "IsDockCommonCommissionEnabled", "False"));
        public static decimal DockCommonCommission = Convert.ToDecimal(configurationRepository.GetConfiguration("DockMilkCollection", "DockCommonCommission", "0.01"));

        public static string VLCCollectionMessage = "Your Collection Details for Collection Date:{0},Total Quantity:{1},Tota; Amount:{2}";
        public static string DockCollectionMessage = "Your Collection Details for Collection Date:{0},Total Quantity:{1},Tota; Amount:{2}";
        public static string ForgotPasswordOTPMessage = "Dear  Customer,{0} is your one time password (OTP). Please enter the OTP to proceed. Thank You";
        public static string CompanyName = "Natraj Dairy";


        public static string[] CompanyAddress = new string[] { "Unit: Natraj Dairy, Narsakhedi, Nimbahera, Distt. Chittorgarh, Rajasthan"
                                "Office: Natraj Dairy, Patel chowk, Choti sadri, Distt. Pratapgarh, Rajasthan" };
        public static string[] CompanyEmail = new string[] { "sunil@Natrajdairy.com", "info@Natrajdairy.com" };
        public static string[] CompanyPhone = new string[] { "+91 6350505864", "+91 9929575888" };
        public static string CompanyDescription = "A Milk Production Company";






    }
}
