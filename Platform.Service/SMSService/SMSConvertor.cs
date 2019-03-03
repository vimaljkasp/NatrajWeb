

using Platform.DTO;
using Platform.Sql;
using Platform.Utilities;

namespace Platform.Service
{
    public class SMSConvertor
    {
        public static void ConvertToSMSMessage(ref NatrajSMSLog natrajSMSLog, NatrajComponent natrajComponent, SMSType sMSType, string mobileNumber, string message)
        {
             natrajSMSLog.ComponentId =(int) natrajComponent;
            natrajSMSLog.Receiver = mobileNumber;
            natrajSMSLog.SMSMessage = message;
            natrajSMSLog.SMSSentTime = DateTimeHelper.GetISTDateTime();
            natrajSMSLog.SMSTypeId = (int)sMSType;
            natrajSMSLog.SMSStatus = (int)SMSStatusId.Queue;
            natrajSMSLog.Sender = "9566812835";
          
        }

   }
}
