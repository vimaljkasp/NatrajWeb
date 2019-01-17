using Platform.DTO;
using Platform.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service
{
    public class ProductConvertor
    {
        public static ProductShoppingDTO ConvertToProductShoppingDTO(Product product,string path)
        {
            ProductShoppingDTO productShoppingDTO = new ProductShoppingDTO();
            productShoppingDTO.CategoryId = product.CategoryId;
            productShoppingDTO.CategoryName = product.ProductCategory.CategoryName;
            productShoppingDTO.DiscountedProductPrice = product.DiscountedRate;
            productShoppingDTO.ProductId = product.ProductId;
            productShoppingDTO.ProductName = product.Name;
            productShoppingDTO.ProductPrice = product.Rate;
            productShoppingDTO.ProductQuantityDescription = product.Description;
            productShoppingDTO.ImageUrl = Path.Combine(path, "PROD" + product.ProductId.ToString()+".jpg");
            return productShoppingDTO;

        }

        public static ProductCategoryDTO ConvertToProductCategoryDTO(ProductCategory productCategory)
        {
            ProductCategoryDTO productCategoryDTO = new ProductCategoryDTO();
            productCategoryDTO.CategoryId = productCategory.CategoryId;
            productCategoryDTO.CategoryName = productCategory.CategoryName;
            return productCategoryDTO;
        }

        public static void ConvertProductDTOToProductEntity(ref Product product,ProductDTO productDTO)
        {
            product.CategoryId = productDTO.CategoryId;
            product.Description = productDTO.ProductQuantityDescription;
            product.DiscountedRate = productDTO.DiscountedProductPrice;
            product.Rate = productDTO.ProductPrice;
            product.SubCategoryId = product.CategoryId;
            
        }
    }
}
