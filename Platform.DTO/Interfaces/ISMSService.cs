using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public interface ISMSService
    {
        void SendMessage(NatrajComponent natrajComponent, SMSType sMSType, string mobileNumber, string message);
    }
}
