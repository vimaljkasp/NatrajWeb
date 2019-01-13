
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public interface IProductService
    {
        ResponseDTO GetAllDCProducts();

        ResponseDTO GetAllDCProductCategories();

        ResponseDTO GetAllDCProductsByCategoryId(int categoryId);

        ResponseDTO GetProductByProductId(int productId);

        ResponseDTO AddProduct(ProductDTO productDTO);

        ResponseDTO UpdateProduct(ProductDTO productDTO);

        ResponseDTO DeleteProduct(int id);


        ResponseDTO SearchDCProductsByProductName(string productName);
    }
}
