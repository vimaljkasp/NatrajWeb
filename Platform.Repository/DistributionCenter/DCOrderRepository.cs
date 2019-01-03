using Platform.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Repository
{
   public  class DCOrderRepository
    {
        PlatformDBEntities _repository;
        public DCOrderRepository(PlatformDBEntities repository)
        {
            _repository = repository;
        }
    }
}
