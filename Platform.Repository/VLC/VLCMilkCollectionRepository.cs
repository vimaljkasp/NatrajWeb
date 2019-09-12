using Platform.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Platform.DTO;

namespace Platform.Repository
{
    public class VLCMilkCollectionRepository
    {
        PlatformDBEntities _repository;
        public VLCMilkCollectionRepository(PlatformDBEntities repository)
        {
            _repository = repository;
        }
        public List<VLCMilkCollection> GetAll()
        {
            var vlcMilkCollections = _repository.VLCMilkCollections.ToList<Sql.VLCMilkCollection>();
            return vlcMilkCollections;
        }

        public List<VLCMilkCollection> GetVLCMilkCollectionByCount(int? pageNumber, int? count)
        {
            var takePage = pageNumber ?? PagingConstant.DefaultPageNumber;
            var takeCount = count ?? PagingConstant.DefaultRecordCount;

            PlatformDBEntities context = new PlatformDBEntities();

            var vlcMilkCollections = context.VLCMilkCollections
                                 .OrderBy(c => c.VLCMilkCollectionId)
                                .Skip((takePage - 1) * takeCount)
                                .Take(takeCount)
                                .ToList<Sql.VLCMilkCollection>();

            return vlcMilkCollections;
        }

        public VLCMilkCollection GetById(int id)
        {
            var vlcMilkCollection = _repository.VLCMilkCollections.FirstOrDefault(x => x.VLCMilkCollectionId == id);
            return vlcMilkCollection;
        }

        public VLCMilkCollection GetCollectionByShiftDateProduct(DateTime collectionDate, ShiftEnum shift, MilkTypeEnum productId, int customerId)
        {
            DateTime collectionDat = Convert.ToDateTime(collectionDate).Date;
            var vlcmilk = _repository.VLCMilkCollections.Where(v => v.ShiftId == (int)shift &&
            System.Data.Entity.DbFunctions.TruncateTime(v.CollectionDateTime) == collectionDat && v.CustomerId == customerId).FirstOrDefault();
            if (vlcmilk != null)
            {
                if (_repository.VLCMilkCollectionDtls.Where(v => v.VLCMilkCollectionId == vlcmilk.VLCMilkCollectionId && v.ProductId == (int)productId).Any())
                    return vlcmilk;

            }
            return null;


        }


        public List<VLCMilkCollection> GetByVLCIdAndCollectionDateShift(int vlcId, DateTime collectionDate, ShiftEnum shift, int? pageNumber)
        {
            DateTime collectionDat = Convert.ToDateTime(collectionDate).Date;

            var takePage = pageNumber ?? PagingConstant.DefaultPageNumber;
            var takeCount = PagingConstant.DefaultRecordCount;
            var vlcMilkCollection = _repository.VLCMilkCollections
                 .Where(x => x.VLCId == vlcId
                 && System.Data.Entity.DbFunctions.TruncateTime(x.CollectionDateTime) == collectionDat
                 && x.ShiftId == (int)shift)

                .OrderByDescending(x => x.CollectionDateTime)
                .Skip((takePage - 1) * takeCount)
                .Take(takeCount)
                .Include("Customer")
               .ToList<Sql.VLCMilkCollection>();
            //&& x.CollectionDate.Value.Date==collectionDate.Date && && x.CollectionShift.Equals(shift,StringComparison.CurrentCultureIgnoreCase)
            return vlcMilkCollection;
        }


        public void Add(VLCMilkCollection vlcMilkCollection)
        {
            if (vlcMilkCollection != null)
            {
                _repository.VLCMilkCollections.Add(vlcMilkCollection);
                // _repository.SaveChanges();

            }
        }

        public void Update(VLCMilkCollection vlcMilkCollection)
        {

            if (vlcMilkCollection != null)
            {
                _repository.Entry<Sql.VLCMilkCollection>(vlcMilkCollection).State = System.Data.Entity.EntityState.Modified;
                //  _repository.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var vlcMilkCollection = _repository.VLCMilkCollections.Where(x => x.VLCMilkCollectionId == id).FirstOrDefault();
            if (vlcMilkCollection != null)
                _repository.VLCMilkCollections.Remove(vlcMilkCollection);

            // _repository.SaveChanges();

        }

    }
}
