using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Sql;

namespace Platform.Repository
{
    public class CustomerRepository 
    {

        PlatformDBEntities _repository ;
        public CustomerRepository(PlatformDBEntities repository)
        {
            _repository = repository;
        }
        public List<Customer> GetAll()
        {
            var customers = _repository.Customers.ToList<Sql.Customer>();
            return customers;
        }

        public List<Customer> GetCustomerByVLCId(int vlcId)
        {
            var customers = _repository.Customers.Where(v=>v.VLCId==vlcId).ToList<Sql.Customer>();
            return customers;
        }

        public List<Customer> GetCustomerListByVLCId(int vlcId,int? pageNumber)
        {
            var takePage = pageNumber ?? PagingConstant.DefaultPageNumber;
            var takeCount = PagingConstant.DefaultRecordCount;
            var customers = _repository.Customers
                 .Where(v => v.VLCId == vlcId)
                .OrderBy(c=>c.DateOfJoinVLC)
                .Skip((takePage - 1) * takeCount)
                                .Take(takeCount)
                                .ToList<Sql.Customer>();
            return customers;
        }


        public List<Customer> GetCustomerByCount(int? pageNumber, int? count)
        {
            var takePage = pageNumber ?? PagingConstant.DefaultPageNumber;
            var takeCount = count ?? PagingConstant.DefaultRecordCount;

         

            var customers = _repository.Customers
                                 .OrderBy(c => c.CustomerId)
                                .Skip((takePage - 1) * takeCount)
                                .Take(takeCount)
                                .ToList<Sql.Customer>();

            return customers;
        }

        public int GetCustomerCodeIdByVLC(int vlcId)
        {
          return _repository.Customers.Where(v => v.VLCId == vlcId).Max(c => c.CustomerCode)+1;
        }

        public Customer GetById(int id)
        {
           
                var customer = _repository.Customers.FirstOrDefault(x => x.CustomerId == id);
             
           
            
            return customer;
        }


        public void Add(Customer customer)
        {
            if (customer != null)
            {
                _repository.Customers.Add(customer);
              // _repository.SaveChanges();

            }
        }

        public void Update(Customer customer)
        {
         
                if (customer != null)
                {
                    _repository.Entry<Sql.Customer>(customer).State = System.Data.Entity.EntityState.Modified;
                  //  _repository.SaveChanges();
                }     

       
            
        }

        public void Delete(int id)
        {
            var customer = _repository.Customers.Where(x => x.CustomerId == id).FirstOrDefault();
            if (customer != null)
                _repository.Customers.Remove(customer);

           // _repository.SaveChanges();

        }

        public Customer GetCustomerByMobileNumber(string mobileNumber)
        {
            var custmer = _repository.Customers.Where(x => x.Contact == mobileNumber).FirstOrDefault();
            return custmer;
        }


        public Customer GetCustomerByEmail(string email)
        {
            var custmer = _repository.Customers.Where(x => x.Email == email).FirstOrDefault();
            return custmer;
        }




    }
}
