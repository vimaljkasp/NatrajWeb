using Platform.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Repository
{
   public  class DCPaymentDetailRepository
    {

        PlatformDBEntities _repository;
        public DCPaymentDetailRepository(PlatformDBEntities repository)
        {
            _repository = repository;
        }


        public List<DCPaymentDetail> GetAll()
        {
            var dcAddress = _repository.DCPaymentDetails.ToList<Sql.DCPaymentDetail>();
            return dcAddress;
        }

        public DCPaymentDetail GetPaymentDetailByPaymentId(int paymentId)
        {
            var dcAddress = _repository.DCPaymentDetails.Where(v => v.DCPaymentId == paymentId).FirstOrDefault();
            return dcAddress;
        }

        public List<DCPaymentDetail> GetAllDCPaymentDetailByDCId(int dCid)
        {
            var dcAddress = _repository.DCPaymentDetails.Where(v => v.DCId == dCid).ToList<Sql.DCPaymentDetail>();
            return dcAddress;
        }

        public List<DCPaymentDetail> GetAllDCPaymentDetailByOrderId(int orderId)
        {
            var dcAddress = _repository.DCPaymentDetails.Where(v => v.DCOrderId == orderId).ToList<Sql.DCPaymentDetail>();
            return dcAddress;
        }

        public void Add(DCPaymentDetail dCPaymentDetail)
        {
            if (dCPaymentDetail != null)
            {
                _repository.DCPaymentDetails.Add(dCPaymentDetail);

            }
        }

        public void Update(DCPaymentDetail dCPaymentDetail)
        {

            if (dCPaymentDetail != null)
            {
                _repository.Entry<Sql.DCPaymentDetail>(dCPaymentDetail).State = System.Data.Entity.EntityState.Modified;

            }



        }

        public void Delete(int id)
        {
            var dCPaymentDetail = _repository.DCPaymentDetails.Where(x => x.DCPaymentId == id).FirstOrDefault();
            if (dCPaymentDetail != null)
                _repository.DCPaymentDetails.Remove(dCPaymentDetail);

            // _repository.SaveChanges();

        }
    }
}
