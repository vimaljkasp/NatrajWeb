using Platform.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Repository
{
    public class DockMilkCollectionDtlRepository
    {
        PlatformDBEntities _repository;
        public DockMilkCollectionDtlRepository(PlatformDBEntities repository)
        {
            _repository = repository;
        }
        public List<DockMilkCollectionDtl> GetAll()
        {
            var dockMilkCollections = _repository.DockMilkCollectionDtls.ToList<Sql.DockMilkCollectionDtl>();
            return dockMilkCollections;
        }

        public List<DockMilkCollectionDtl> GetDockMilkCollectionByCount(int? pageNumber, int? count)
        {
            var takePage = pageNumber ?? PagingConstant.DefaultPageNumber;
            var takeCount = count ?? PagingConstant.DefaultRecordCount;

            PlatformDBEntities context = new PlatformDBEntities();

            var dockMilkCollections = context.DockMilkCollectionDtls
                                 .OrderBy(c => c.DockMilkCollectionDtlI)
                                .Skip((takePage - 1) * takeCount)
                                .Take(takeCount)
                                .ToList<Sql.DockMilkCollectionDtl>();

            return dockMilkCollections;
        }

        public DockMilkCollectionDtl GetById(int id)
        {
            var dockMilkCollection = _repository.DockMilkCollectionDtls.Where(x => x.DockMilkCollectionId == id).FirstOrDefault();
            return dockMilkCollection;
        }

        public List<DockMilkCollectionDtl> GetByVLCMilkCollectionId(int dockMilkCollectionId)
        {
            var dockMilkCollectionDtls = _repository.DockMilkCollectionDtls.Where(m => m.DockMilkCollectionId == dockMilkCollectionId).ToList();

            return dockMilkCollectionDtls;
        }


        public void Add(DockMilkCollectionDtl dockMilkCollectionDtl)
        {
            if (dockMilkCollectionDtl != null)
            {
                _repository.DockMilkCollectionDtls.Add(dockMilkCollectionDtl);
                // _repository.SaveChanges();

            }
        }

        public void Update(DockMilkCollectionDtl dockMilkCollectionDtl)
        {

            if (dockMilkCollectionDtl != null)
            {
                _repository.Entry<Sql.DockMilkCollectionDtl>(dockMilkCollectionDtl).State = System.Data.Entity.EntityState.Modified;
                //  _repository.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var dockMilkCollectionDtl = _repository.DockMilkCollectionDtls.Where(x => x.DockMilkCollectionDtlI == id).FirstOrDefault();
            if (dockMilkCollectionDtl != null)
                _repository.DockMilkCollectionDtls.Remove(dockMilkCollectionDtl);

            // _repository.SaveChanges();

        }

    }
}
