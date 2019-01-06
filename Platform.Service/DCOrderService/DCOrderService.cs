using Platform.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service
{
    public class DCOrderService : IDCOrderService,IDisposable
    {
        public ResponseDTO AddDCOrder(DCOrderDTO dCPaymentDTO)
        {
            throw new NotImplementedException();
        }

        public ResponseDTO DeleteDCOrder(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<DCOrderDTO> GetAllDCOrders()
        {
            throw new NotImplementedException();
        }

        public ResponseDTO GetDCOrdersById(int dcId)
        {
            throw new NotImplementedException();
        }

        public ResponseDTO UpdateDCOrder(DCOrderDTO dCPaymentDTO)
        {
            throw new NotImplementedException();
        }
    }
}
