using Platform.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Repository
{
    public class DCOrderRepository
    {
        PlatformDBEntities _repository;
        public DCOrderRepository(PlatformDBEntities repository)
        {
            _repository = repository;
        }


        public List<DCOrder> GetAll()
        {
            var dCOrders = _repository.DCOrders.ToList<Sql.DCOrder>();
            return dCOrders;
        }

        public DCOrder GetDCOrderByOrderId(int orderId)
        {
            var dcOrder = _repository.DCOrders.Include("DCOrderDtls").Include("DCAddress").Include("DCOrderDtls.Product").Where(d => d.DCOrderId == orderId && d.IsDeleted == false).FirstOrDefault();
            return dcOrder;
        }

        public List<DCOrder> GetAllDCOrdersByDCId(int dCid)
        {
            var dCOrders = _repository.DCOrders.Include("DCAddress").Include("DCOrderDtls.Product").Where(v => v.DCId == dCid && v.IsDeleted == false).OrderByDescending(v => v.OrderDate).ToList<Sql.DCOrder>();
            return dCOrders;
        }

        public void Add(DCOrder dCOrder)
        {
            if (dCOrder != null)
            {
                _repository.DCOrders.Add(dCOrder);

            }
        }

        public void Update(DCOrder dCOrder)
        {

            if (dCOrder != null)
            {
                _repository.Entry<Sql.DCOrder>(dCOrder).State = System.Data.Entity.EntityState.Modified;

            }



        }

        public void Delete(int id)
        {
            var dCOrder = _repository.DCOrders.Where(x => x.DCOrderId == id).FirstOrDefault();
            if (dCOrder != null)
                dCOrder.IsDeleted = true;

            // _repository.SaveChanges();

        }
    }
}

