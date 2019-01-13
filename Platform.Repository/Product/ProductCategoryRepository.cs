using Platform.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Repository
{
    public class ProductCategoryRepository
    {
        PlatformDBEntities _repository;
        public ProductCategoryRepository(PlatformDBEntities repository)
        {
            _repository = repository;
        }

        public List<ProductCategory> GetAllProductCategoriess()
        {
            var productCategories = _repository.ProductCategories.ToList();
            return productCategories;
        }

      
    }
}
