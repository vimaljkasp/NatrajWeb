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
    }
}
