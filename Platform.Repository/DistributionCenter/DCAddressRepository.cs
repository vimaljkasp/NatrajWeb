using Platform.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Repository
{
    public class DCAddressRepository
    {
        PlatformDBEntities _repository;
        public DCAddressRepository(PlatformDBEntities repository)
        {
            _repository = repository;
        }


        public List<DCAddress> GetAll()
        {
            var dcAddress = _repository.DCAddresses.Where(v => v.IsDeleted == false).ToList<Sql.DCAddress>();
            return dcAddress;
        }

        public DCAddress GetById(int id)
        {
            var dcAddress = _repository.DCAddresses.Where(v => v.DCAddressId == id && (v.IsDeleted == null || v.IsDeleted == false)).FirstOrDefault();
            return dcAddress;
        }

        public DCAddress GetDefaultAddressByDCId(int dCid)
        {
            var dcAddress = _repository.DCAddresses.Where(v => v.DCId == dCid && v.IsDefaultAddress==true && (v.IsDeleted == null || v.IsDeleted == false)).FirstOrDefault();
            return dcAddress;
        }

        public List<DCAddress> GetAllDCAddressByDCId(int dCid)
        {
            var dcAddress = _repository.DCAddresses.Include("DistributionCenter").Where(v => v.DCId == dCid && (v.IsDeleted == null || v.IsDeleted == false)).ToList<Sql.DCAddress>();
            return dcAddress;
        }

        public void Add(DCAddress dCAddress)
        {
            if (dCAddress != null)
            {
                _repository.DCAddresses.Add(dCAddress);

            }
        }

        public void Update(DCAddress dCAddress)
        {

            if (dCAddress != null)
            {
                _repository.Entry<Sql.DCAddress>(dCAddress).State = System.Data.Entity.EntityState.Modified;

            }



        }

        public void Delete(int id)
        {
            var dcAddress = _repository.DCAddresses.Where(x => x.DCAddressId == id).FirstOrDefault();
            if (dcAddress != null)
                dcAddress.IsDeleted = true;

            // _repository.SaveChanges();

        }
    }
}
