using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Sql;

namespace Platform.Repository
{
    public class CustomerAgricultureRepository
    {

        PlatformDBEntities _repository;
        public CustomerAgricultureRepository(PlatformDBEntities repository)
        {
            _repository = repository;
        }
        public List<CustomerAgriculture> GetAll()
        {
            var customerAgricultures = _repository.CustomerAgricultures.ToList<Sql.CustomerAgriculture>();
            return customerAgricultures;
        }

        public List<CustomerAgriculture> GetCustomerByCount(int? pageNumber, int? count)
        {
            var takePage = pageNumber ?? PagingConstant.DefaultPageNumber;
            var takeCount = count ?? PagingConstant.DefaultRecordCount;

            PlatformDBEntities context = new PlatformDBEntities();

            var customerAgricultures = context.CustomerAgricultures
                                  .Where(c=>c.IsDeleted ==false)
                                 .OrderBy(c => c.CustAgriId)
                                .Skip((takePage - 1) * takeCount)
                                .Take(takeCount)
                                .ToList<Sql.CustomerAgriculture>();

            return customerAgricultures;
        }

        public CustomerAgriculture GetByCustomerId(int id)
        {

            var customerAgriculture = _repository.CustomerAgricultures.FirstOrDefault(x => x.CustomerId == id && x.IsDeleted==false);



            return customerAgriculture;
        }


        public void Add(CustomerAgriculture customerAgriculture)
        {
            if (customerAgriculture != null)
            {
                _repository.CustomerAgricultures.Add(customerAgriculture);
                // _repository.SaveChanges();

            }
        }

        public void Update(CustomerAgriculture customerAgriculture)
        {

            if (customerAgriculture != null)
            {
                _repository.Entry<Sql.CustomerAgriculture>(customerAgriculture).State = System.Data.Entity.EntityState.Modified;
                //  _repository.SaveChanges();
            }



        }

        public void Delete(int id)
        {
            var customerAgriculture = _repository.CustomerAgricultures.Where(x => x.CustAgriId == id).FirstOrDefault();
           
            if (customerAgriculture != null)
                customerAgriculture.IsDeleted = true;

            // _repository.SaveChanges();

        }






    }
}
