using Platform.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Platform.Repository
{
    public class DockMilkCollectionRepository
    {
        PlatformDBEntities _repository;
        public DockMilkCollectionRepository(PlatformDBEntities repository)
        {
            _repository = repository;
        }
        public List<DockMilkCollection> GetAll()
        {
            var vlcMilkCollections = _repository.DockMilkCollections.ToList<Sql.DockMilkCollection>();
            return vlcMilkCollections;
        }

        public List<DockMilkCollection> GetDockMilkCollectionByCount(int? pageNumber, int? count)
        {
            var takePage = pageNumber ?? PagingConstant.DefaultPageNumber;
            var takeCount = count ?? PagingConstant.DefaultRecordCount;

            PlatformDBEntities context = new PlatformDBEntities();

            var dockMilkCollections = context.DockMilkCollections
                                 .OrderBy(c => c.DockMilkCollectionId)
                                .Skip((takePage - 1) * takeCount)
                                .Take(takeCount)
                                .ToList<Sql.DockMilkCollection>();

            return dockMilkCollections;
        }

        public DockMilkCollection GetById(int id)
        {
            var dockMilkCollection = _repository.DockMilkCollections.FirstOrDefault(x => x.DockMilkCollectionId == id);
            return dockMilkCollection;
        }

        public DockMilkCollection GetCollectionByShiftDateProduct(DateTime collectionDate, int shift, int productId, int vlcId)
        {
            DateTime collectionDat = Convert.ToDateTime(collectionDate).Date;
            var dockMilk = _repository.DockMilkCollections.Where(v => v.ShiftId == shift &&
            System.Data.Entity.DbFunctions.TruncateTime(v.CollectionDateTime) == collectionDat && v.VLCId == vlcId).FirstOrDefault();
            if (dockMilk != null)
            {
                if (_repository.DockMilkCollectionDtls.Where(v => v.DockMilkCollectionId == dockMilk.DockMilkCollectionId && v.ProductId == productId).Any())
                    return dockMilk;

            }
            return null;


        }


        public List<DockMilkCollection> GetDockCollectionByDateShift( DateTime collectionDate, int shift, int? pageNumber)
        {
            DateTime collectionDat = Convert.ToDateTime(collectionDate).Date;

            var takePage = pageNumber ?? PagingConstant.DefaultPageNumber;
            var takeCount = PagingConstant.DefaultRecordCount;
            var dockMilkCollection = _repository.DockMilkCollections
                 .Where(x => System.Data.Entity.DbFunctions.TruncateTime(x.CollectionDateTime) == collectionDat
                 && x.ShiftId == shift)

                .OrderByDescending(x => x.CollectionDateTime)
                .Skip((takePage - 1) * takeCount)
                .Take(takeCount)
                .Include("VLC")
               .ToList<Sql.DockMilkCollection>();
            return dockMilkCollection;
        }


        public void Add(DockMilkCollection dockMilkCollection)
        {
            if (dockMilkCollection != null)
            {
                _repository.DockMilkCollections.Add(dockMilkCollection);
                // _repository.SaveChanges();

            }
        }

        public void Update(DockMilkCollection dockMilkCollection)
        {

            if (dockMilkCollection != null)
            {
                _repository.Entry<Sql.DockMilkCollection>(dockMilkCollection).State = System.Data.Entity.EntityState.Modified;
                //  _repository.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var dockMilkCollection = _repository.DockMilkCollections.Where(x => x.DockMilkCollectionId == id).FirstOrDefault();
            if (dockMilkCollection != null)
                _repository.DockMilkCollections.Remove(dockMilkCollection);

            // _repository.SaveChanges();

        }

    }
}
