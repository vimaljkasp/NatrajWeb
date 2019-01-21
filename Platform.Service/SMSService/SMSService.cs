using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service.SMSService
{
    public class SMSService : ISMSService, IDisposable
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        public void SendMessage()
        {
           
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
