using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Sql;

namespace Platform.Repository
{
    public class DistributionCenterRepository
    {

        PlatformDBEntities _repository;
        public DistributionCenterRepository(PlatformDBEntities repository)
        {
            _repository = repository;
        }
        
        public List<DistributionCenter> GetAll()
        {
            var distributionCenters = _repository.DistributionCenters.ToList<Sql.DistributionCenter>();
            return distributionCenters;
        }

        public List<DistributionCenter> GetDistributionCenterByVLCId(int dcId)
        {
            var distributionCenters = _repository.DistributionCenters.Where(v => v.DCId == dcId).ToList<Sql.DistributionCenter>();
            return distributionCenters;
        }

        public List<DistributionCenter> GetDistributionCenterListByCity(string city, int? pageNumber)
        {
            var takePage = pageNumber ?? PagingConstant.DefaultPageNumber;
            var takeCount = PagingConstant.DefaultRecordCount;
            var dcIds = _repository.DCAddresses.Where(d => d.IsDefaultAddress).Select(d => d.DCId).ToList();
            var distributionCenters = _repository.DistributionCenters
                 .Where(d=>d.DCId.Equals(dcIds))
                .OrderBy(c => c.DateOfRegistration)
                .Skip((takePage - 1) * takeCount)
                                .Take(takeCount)
                                .ToList<Sql.DistributionCenter>();
            return distributionCenters;
        }


        //public List<DistributionCenter> GetDistributionCenterListForSearchByVLCId(int vlcId)
        //{
        //    var distributionCenters = _repository.DistributionCenters
        //         .Where(v => v.VLCId == vlcId)
        //        .OrderBy(c => c.DistributionCenterId)
        //        .ToList<Sql.DistributionCenter>();
        //    return distributionCenters;
        //}

        public List<DistributionCenter> GetDistributionCenterByCount(int? pageNumber, int? count)
        {
            var takePage = pageNumber ?? PagingConstant.DefaultPageNumber;
            var takeCount = count ?? PagingConstant.DefaultRecordCount;



            var distributionCenters = _repository.DistributionCenters
                                 .OrderBy(c => c.DCId)
                                .Skip((takePage - 1) * takeCount)
                                .Take(takeCount)
                                .ToList<Sql.DistributionCenter>();

            return distributionCenters;
        }

        //public int GetDistributionCenterCodeIdByVLC(string city)
        //{
        //    return _repository.DistributionCenters.Where(v => v.VLCId == vlcId).Max(c => c.DistributionCenterCode) + 1;
        //}

        public DistributionCenter GetById(int id)
        {

            var distributionCenter = _repository.DistributionCenters.Include("DCAddresses").Include("DCWallets").FirstOrDefault(x => x.DCId == id);



            return distributionCenter;
        }


        public void Add(DistributionCenter distributionCenter)
        {
            if (distributionCenter != null)
            {
                _repository.DistributionCenters.Add(distributionCenter);
               

            }
        }

        public void Update(DistributionCenter distributionCenter)
        {

            if (distributionCenter != null)
            {
                _repository.Entry<Sql.DistributionCenter>(distributionCenter).State = System.Data.Entity.EntityState.Modified;
               
            }



        }

        public void Delete(int id)
        {
            var distributionCenter = _repository.DistributionCenters.Where(x => x.DCId == id).FirstOrDefault();
            if (distributionCenter != null)
                _repository.DistributionCenters.Remove(distributionCenter);

            // _repository.SaveChanges();

        }

        public DistributionCenter GetDistributionCenterByMobileNumber(string mobileNumber)
        {
            var distributionCenter = _repository.DistributionCenters.Where(x => x.Contact == mobileNumber).FirstOrDefault();
            return distributionCenter;
        }


        public DistributionCenter GetDistributionCenterByEmail(string email)
        {
            var distributionCenter = _repository.DistributionCenters.Where(x => x.Email == email).FirstOrDefault();
            return distributionCenter;
        }


        public DistributionCenter GetDistributionCenterByCode(string dcCode)
        {
            var distributionCenter = _repository.DistributionCenters.Where(x => x.DCCode == dcCode).FirstOrDefault();
            return distributionCenter;
        }



    }
}
