using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Sql;

namespace Platform.Repository
{
    public class CustomerBankRepository
    {

        PlatformDBEntities _repository;
        public CustomerBankRepository(PlatformDBEntities repository)
        {
            _repository = repository;
        }
        public List<CustomerBank> GetAll()
        {
            var customerBanks = _repository.CustomerBanks.ToList<Sql.CustomerBank>();
            return customerBanks;
        }

        public List<CustomerBank> GetCustomerByCount(int? pageNumber, int? count)
        {
            var takePage = pageNumber ?? PagingConstant.DefaultPageNumber;
            var takeCount = count ?? PagingConstant.DefaultRecordCount;

            PlatformDBEntities context = new PlatformDBEntities();

            var customerBanks = context.CustomerBanks
                                 .OrderBy(c => c.CustomerBankId)
                                .Skip((takePage - 1) * takeCount)
                                .Take(takeCount)
                                .ToList<Sql.CustomerBank>();

            return customerBanks;
        }

        public CustomerBank GetByCustomerId(int id)
        {

            var customerBank = _repository.CustomerBanks.FirstOrDefault(x => x.CustomerId == id);



            return customerBank;
        }


        public void Add(CustomerBank customerBank)
        {
            if (customerBank != null)
            {
                _repository.CustomerBanks.Add(customerBank);
                // _repository.SaveChanges();

            }
        }

        public void Update(CustomerBank customerBank)
        {

            if (customerBank != null)
            {
                _repository.Entry<Sql.CustomerBank>(customerBank).State = System.Data.Entity.EntityState.Modified;
                //  _repository.SaveChanges();
            }



        }

        public void Delete(int id)
        {
            var customerBank = _repository.CustomerBanks.Where(x => x.CustomerBankId == id).FirstOrDefault();
            if (customerBank != null)
                _repository.CustomerBanks.Remove(customerBank);

            // _repository.SaveChanges();

        }

   




    }
}
