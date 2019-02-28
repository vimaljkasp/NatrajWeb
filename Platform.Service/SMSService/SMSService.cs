using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Platform.DTO;
using Platform.Repository;
using Platform.Sql;
using Platform.Utilities;

namespace Platform.Service
{
    public  class SMSService : ISMSService, IDisposable
    {
        UnitOfWork unitOfWork = new UnitOfWork();

   

        public void SendEmailInBackgroundThread(NatrajSMSLog natrajSMSLog)
        {
            Thread bgThread = new Thread(new ParameterizedThreadStart(SendMessage));
            bgThread.IsBackground = true;
            bgThread.Start(natrajSMSLog);
        }

        public void SendMessage(Object natrajSMS)
        {
            NatrajSMSLog natrajSMSLog = (NatrajSMSLog)natrajSMS;
            natrajSMSLog.Sender = unitOfWork.NatrajConfigurationSettings.SenderMobileNumber;
            using (var web = new System.Net.WebClient())
            {
                try
                {
                    string userName = unitOfWork.NatrajConfigurationSettings.SMSServiceUserName;
                    string userPassword = unitOfWork.NatrajConfigurationSettings.SMSServicePassword;
                    string msgRecepient =natrajSMSLog.Receiver;
                    string msgText = natrajSMSLog.SMSMessage;
                    string url = "https://www.txtguru.in/imobile/api.php?username="+ userName+
                        "&password="+ userPassword+"&source=UPDATE&dmobile="+ msgRecepient + "&message="+ msgText;
                    string result = web.DownloadString(url);
                    if (result.Contains("OK"))
                    {
                        natrajSMSLog.SMSErrorMsg = "Success";
                        unitOfWork.SMSRepository.Update(natrajSMSLog);
                        unitOfWork.SaveChanges();
                    }
                    else
                    {
                        natrajSMSLog.SMSErrorMsg = "Error";
                        unitOfWork.SMSRepository.Update(natrajSMSLog);
                        unitOfWork.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    //Catch and show the exception if needed. Donot supress. :)  

                }


            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void SendMessage(NatrajComponent natrajComponent, SMSType sMSType, string mobileNumber, string message)
        {
            throw new NotImplementedException();
        }
    }
}
