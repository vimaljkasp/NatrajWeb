using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Sql;

namespace Platform.Repository
{
    public class UserRepository
    {

        PlatformDBEntities _repository;
        public UserRepository(PlatformDBEntities repository)
        {
            _repository = repository;
        }
        public List<User> GetAll()
        {
            var users = _repository.Users.ToList<Sql.User>();
            return users;
        }

        public List<User> GetUsersByCount(int? pageNumber, int? count)
        {
            var takePage = pageNumber ?? PagingConstant.DefaultPageNumber;
            var takeCount = count ?? PagingConstant.DefaultRecordCount;

            PlatformDBEntities context = new PlatformDBEntities();

            var users = context.Users
                                 .OrderBy(c => c.UserId)
                                .Skip((takePage - 1) * takeCount)
                                .Take(takeCount)
                                .ToList<Sql.User>();

            return users;
        }

        public User GetById(int id)
        {

            var user = _repository.Users.FirstOrDefault(x => x.UserId == id);



            return user;
        }


        public void Add(User user)
        {
            if (user != null)
            {
                _repository.Users.Add(user);
                // _repository.SaveChanges();

            }
        }

        public void Update(User user)
        {

            if (user != null)
            {
                _repository.Entry<Sql.User>(user).State = System.Data.Entity.EntityState.Modified;
                //  _repository.SaveChanges();
            }



        }

        public void Delete(int id)
        {
            var user = _repository.Users.Where(x => x.UserId == id).FirstOrDefault();
            if (user != null)
                _repository.Users.Remove(user);

            // _repository.SaveChanges();

        }






    }
}
