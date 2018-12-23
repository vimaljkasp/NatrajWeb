using Platform.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Repository
{
    public class VLCMilkCollectionDtlRepository
    {
        PlatformDBEntities _repository;
        public VLCMilkCollectionDtlRepository(PlatformDBEntities repository)
        {
            _repository = repository;
        }
        public List<VLCMilkCollectionDtl> GetAll()
        {
            var vlcMilkCollections = _repository.VLCMilkCollectionDtls.ToList<Sql.VLCMilkCollectionDtl>();
            return vlcMilkCollections;
        }

        public List<VLCMilkCollectionDtl> GetVLCMilkCollectionByCount(int? pageNumber, int? count)
        {
            var takePage = pageNumber ?? PagingConstant.DefaultPageNumber;
            var takeCount = count ?? PagingConstant.DefaultRecordCount;

            PlatformDBEntities context = new PlatformDBEntities();

            var vlcMilkCollections = context.VLCMilkCollectionDtls
                                 .OrderBy(c => c.VLCMilkCollectionId)
                                .Skip((takePage - 1) * takeCount)
                                .Take(takeCount)
                                .ToList<Sql.VLCMilkCollectionDtl>();

            return vlcMilkCollections;
        }

        public VLCMilkCollectionDtl GetById(int id)
        {
            var vlcMilkCollection = _repository.VLCMilkCollectionDtls.FirstOrDefault(x => x.VLCMilkCollectionId == id);
            return vlcMilkCollection;
        }

        public List<VLCMilkCollectionDtl> GetByVLCMilkCollectionId(int vlcMilkCollectionId)
        {
            var vlcMilkCollectionDtls = _repository.VLCMilkCollectionDtls.Where(m => m.VLCMilkCollectionId == vlcMilkCollectionId).ToList();
                
            return vlcMilkCollectionDtls;
        }


        public void Add(VLCMilkCollectionDtl vlcMilkCollectionDtl)
        {
            if (vlcMilkCollectionDtl != null)
            {
                _repository.VLCMilkCollectionDtls.Add(vlcMilkCollectionDtl);
                // _repository.SaveChanges();

            }
        }

        public void Update(VLCMilkCollectionDtl vlcMilkCollectionDtl)
        {

            if (vlcMilkCollectionDtl != null)
            {
                _repository.Entry<Sql.VLCMilkCollectionDtl>(vlcMilkCollectionDtl).State = System.Data.Entity.EntityState.Modified;
                //  _repository.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var vlcMilkCollectionDtl = _repository.VLCMilkCollectionDtls.Where(x => x.VLCMilkCollectionDtlId == id).FirstOrDefault();
            if (vlcMilkCollectionDtl != null)
                _repository.VLCMilkCollectionDtls.Remove(vlcMilkCollectionDtl);

            // _repository.SaveChanges();

        }

    }
}
