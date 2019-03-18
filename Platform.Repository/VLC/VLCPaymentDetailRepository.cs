using Platform.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Repository
{
    public class VLCPaymentDetailRepository
    {

        PlatformDBEntities _repository;
        public VLCPaymentDetailRepository(PlatformDBEntities repository)
        {
            _repository = repository;
        }


        public List<VLCPaymentDetail> GetAll()
        {
            var vLCPayments = _repository.VLCPaymentDetails.ToList<Sql.VLCPaymentDetail>();
            return vLCPayments;
        }

        public VLCPaymentDetail GetPaymentDetailByPaymentId(int paymentId)
        {
            var vLCPayments = _repository.VLCPaymentDetails.Where(v => v.VLCPaymentId == paymentId).FirstOrDefault();
            return vLCPayments;
        }

        public List<VLCPaymentDetail> GetAllVLCPaymentDetailByVLCId(int vlcId)
        {
            var vlcPayments = _repository.VLCPaymentDetails.Where(v => v.VLCId == vlcId).ToList<Sql.VLCPaymentDetail>();
            return vlcPayments;
        }

        public List<VLCPaymentDetail> GetAllVLCPaymentDetailByDockId(int docKId)
        {
            var vlcPayments = _repository.VLCPaymentDetails.Where(v => v.DockMilkCollectionId == docKId).ToList<Sql.VLCPaymentDetail>();
            return vlcPayments;
        }

        public VLCPaymentDetail GetVLCPaymentDetailByDockCollectionId(int dockMilkCollectionId)
        {
            var vLCPayments = _repository.VLCPaymentDetails.Where(v => v.DockMilkCollectionId == dockMilkCollectionId).FirstOrDefault();
            return vLCPayments;
        }

        public void Add(VLCPaymentDetail vlcPaymentDetail)
        {
            if (vlcPaymentDetail != null)
            {
                _repository.VLCPaymentDetails.Add(vlcPaymentDetail);

            }
        }

        public void Update(VLCPaymentDetail vlcPaymentDetail)
        {

            if (vlcPaymentDetail != null)
            {
                _repository.Entry<Sql.VLCPaymentDetail>(vlcPaymentDetail).State = System.Data.Entity.EntityState.Modified;

            }



        }

        public void Delete(int id)
        {
            var vlcPaymentDetail = _repository.VLCPaymentDetails.Where(x => x.VLCPaymentId == id).FirstOrDefault();
            if (vlcPaymentDetail != null)
                _repository.VLCPaymentDetails.Remove(vlcPaymentDetail);

            // _repository.SaveChanges();

        }
    }
}
