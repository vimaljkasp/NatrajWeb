using Platform.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Repository
{
    public class DCOrderDtlRepository
    {
        PlatformDBEntities _repository;
        public DCOrderDtlRepository(PlatformDBEntities repository)
        {
            _repository = repository;
        }


        public List<DCOrderDtl> GetAll()
        {
            var dcOrderDtls = _repository.DCOrderDtls.ToList<Sql.DCOrderDtl>();
            return dcOrderDtls;
        }


        public DCOrderDtl GetById(int id)
        {
            var dcOrderDtl = _repository.DCOrderDtls.Where(o=>o.DCOrderDtlId==id).FirstOrDefault();
            return dcOrderDtl;
        }

        //public List<DCOrder> GetAllDCOrderDtlsByDCId(int dCid)
        //{
        //    var dcOrderDtls = _repository.DCOrderDtls.Where(v => v.DCId == dCid).ToList<Sql.DCOrder>();
        //    return dcOrderDtls;
        //}

        public void Add(DCOrderDtl dCOrderDtl)
        {
            if (dCOrderDtl != null)
            {
                _repository.DCOrderDtls.Add(dCOrderDtl);

            }
        }

        public void Update(DCOrderDtl dCOrderDtl)
        {

            if (dCOrderDtl != null)
            {
                _repository.Entry<Sql.DCOrderDtl>(dCOrderDtl).State = System.Data.Entity.EntityState.Modified;

            }



        }

        public void Delete(int id)
        {
            var dCOrderDtl = _repository.DCOrderDtls.Where(x => x.DCOrderId == id).FirstOrDefault();
            if (dCOrderDtl != null)
                _repository.DCOrderDtls.Remove(dCOrderDtl);

            // _repository.SaveChanges();

        }
    }
}

