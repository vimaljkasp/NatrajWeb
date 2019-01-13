using Platform.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Repository
{
    public class ProductRepository
    {
        PlatformDBEntities _repository;
        public ProductRepository(PlatformDBEntities repository)
        {
            _repository = repository;
        }

        public List<Product> GetAllProducts()
        {
            var products = _repository.Products.Include("ProductCategory").ToList();
            return products;
        }

        public List<Product> GetProductsByProductName(string productName)
        {
            var products = _repository.Products.Include("ProductCategory").Where(p=>p.Name.Contains(productName)).ToList();
            return products;
        }


        public List<Product> GetAllProductsByCategoryId(int categoryId)
        {
            var products = _repository.Products.Include("ProductCategory").Where(p=>p.CategoryId==categoryId).ToList();
            return products;
        }
    }
}
