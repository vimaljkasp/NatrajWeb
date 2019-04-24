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
        public static string ImagePath = configurationRepository.GetConfiguration("DC", "ProductImagePath", @"http://service.natrajdairy.com/img/");

        public static string MilkRatePath = configurationRepository.GetConfiguration("DC","MilkRateChartPath",@"http://service.natrajdairy.com/MilkRate/");


        public static string SenderMobileNumber = configurationRepository.GetConfiguration("SMS", "SenderNumber", "9566812835");
        public static string SMSServiceUserName = configurationRepository.GetConfiguration("SMS", "SMSServiceUserName", "adam");
        public static string SMSServicePassword = configurationRepository.GetConfiguration("SMS", "SMSServicePassword", "12345");
        public static bool IsDockCommonCommissionEnabled = Convert.ToBoolean(configurationRepository.GetConfiguration("DockMilkCollection", 
            "IsDockCommonCommissionEnabled", "False"));
        public static decimal DockCommonCommission = Convert.ToDecimal(configurationRepository.GetConfiguration("DockMilkCollection",
            "DockCommonCommission", "0.01"));

        public static string VLCCollectionMessage = configurationRepository.GetConfiguration("VLC","MilkCollectionMessage",
            "Your Collection Details for Collection Date:{0},Total Quantity:{1},Tota; Amount:{2}");
        public static string DockCollectionMessage = configurationRepository.GetConfiguration("DockMilkCollection","DockCollectionMessage",
            "Your Collection Details for Collection Date:{0},Total Quantity:{1},Tota; Amount:{2}");
        public static string ForgotPasswordOTPMessage = configurationRepository.GetConfiguration("Common","ForgotPassowordOTPMsg",
            "Dear  Customer{0} is your one time password (OTP). Please enter the OTP to proceed. Thank You");
        public static string CompanyName = configurationRepository.GetConfiguration("ContactUs", "CompanyName", "Natraj Dairy");


        public static string[] CompanyAddress = configurationRepository.GetConfiguration("ContactUs","CompanyAddress", @"Unit: Natraj Dairy, Narsakhedi, Nimbahera, Distt. Chittorgarh, Rajasthan,
                                Office: Natraj Dairy, Patel chowk, Choti sadri, Distt. Pratapgarh, Rajasthan" ).Split(',');
        public static string[] CompanyEmail = configurationRepository.GetConfiguration("ContactUs", "CompanyEmail", "sunil@Natrajdairy.com, info@Natrajdairy.com" ).Split(',');
        public static string[] CompanyPhone = configurationRepository.GetConfiguration("ContactUs", "CompanyPhone", "+91 6350505864, +91 9929575888").Split(',');
        public static string CompanyDescription = configurationRepository.GetConfiguration("ContactUs", "CompanyDescription", "A Milk Production Company");






    }
}
