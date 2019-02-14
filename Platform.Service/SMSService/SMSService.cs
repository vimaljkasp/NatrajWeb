using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Platform.DTO;
using Platform.Sql;

namespace Platform.Service
{
    public class SMSService : ISMSService, IDisposable
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        public void SendMessage(NatrajComponent natrajComponent,SMSType sMSType,string mobileNumber,string message)
        {
            NatrajSMSLog natrajSMSLog = new NatrajSMSLog();
            natrajSMSLog.SMSId = unitOfWork.DashboardRepository.NextNumberGenerator("SMS");
            natrajSMSLog.ComponentId = (int)natrajComponent;
            natrajSMSLog.Receiver = mobileNumber;
            natrajSMSLog.Sender = unitOfWork.ConfigurationRepository.GetConfiguration("SMS", "SenderNumber", "9566812835");
            natrajSMSLog.SMSErrorMsg = string.Empty;
            natrajSMSLog.SMSMessage = message;
            natrajSMSLog.SMSSentTime = DateTime.Now;
            natrajSMSLog.SMSTypeId = (int)sMSType;
            unitOfWork.SMSRepository.Add(natrajSMSLog);
            unitOfWork.SaveChanges();

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
