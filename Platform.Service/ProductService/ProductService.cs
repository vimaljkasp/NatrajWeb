using Platform.DTO;
using Platform.Repository;
using Platform.Sql;
using Platform.Utilities;
using System;
using System.Collections.Generic;


namespace Platform.Service
{
    public class ProductService : IProductService, IDisposable
    {

        private UnitOfWork unitOfWork = new UnitOfWork();

        public ResponseDTO AddProduct(ProductDTO productDTO)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            Product product = new Product();
            product.ProductId = unitOfWork.DashboardRepository.NextNumberGenerator("Product");
            ProductConvertor.ConvertProductDTOToProductEntity(ref product, productDTO);
            unitOfWork.ProductRepository.Add(product);
            unitOfWork.SaveChanges();
            responseDTO.Status = true;
            responseDTO.Message = "Product   Added Successfully";
            return responseDTO;
        }

        public ResponseDTO DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseDTO GetAllDCProductCategories()
        {
            ResponseDTO responseDTO = new ResponseDTO();
            List<ProductCategoryDTO> productCategoryList = new List<ProductCategoryDTO>();
            var productCategories = unitOfWork.ProductCategoryRepository.GetAllProductCategoriess();
            if (productCategories != null)
            {
                foreach (var productCategory in productCategories)
                    productCategoryList.Add(ProductConvertor.ConvertToProductCategoryDTO(productCategory));
                responseDTO.Status = true;
                responseDTO.Message = "Product Category List";
                responseDTO.Data = productCategoryList;
                return responseDTO;
            }
            else
            {
                throw new PlatformModuleException("No Products Found");
            }
        }

        public ResponseDTO GetAllDCProducts()
        {
            ResponseDTO responseDTO = new ResponseDTO();
            List<ProductShoppingDTO> productList = new List<ProductShoppingDTO>();
            var products = unitOfWork.ProductRepository.GetAllProducts();
            if (products != null)
            {
                foreach (var product in products)
                    productList.Add(ProductConvertor.ConvertToProductShoppingDTO(product, NatrajConfigurationHelper.ImagePath));
                responseDTO.Status = true;
                responseDTO.Message = "Product List";
                responseDTO.Data = productList;
                return responseDTO;
            }
            else
            {
                throw new PlatformModuleException("No Products Found");
            }

        }

        public ResponseDTO GetAllDCProductsByCategoryId(int categoryId)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            List<ProductShoppingDTO> productList = new List<ProductShoppingDTO>();
            var products = unitOfWork.ProductRepository.GetAllProductsByCategoryId(categoryId);
            if (products != null)
            {
                foreach (var product in products)
                    productList.Add(ProductConvertor.ConvertToProductShoppingDTO(product, NatrajConfigurationHelper.ImagePath));
                responseDTO.Status = true;
                responseDTO.Message = "Product List By Category";
                responseDTO.Data = productList;
                return responseDTO;
            }
            else
            {
                throw new PlatformModuleException("No Products Found");
            }

        }

        /// <summary>
        /// Create by Anil for unit price on UI
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ResponseDTO GetProductByProductId(int productId)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            Product product = new Product();
            ProductDTO productDTO = new ProductDTO();
            List<Product> products = unitOfWork.ProductRepository.GetAllProducts();
            product = products.Find(x => x.ProductId == productId);
            ProductConvertor.ConvertProductEntityToProductDTO(ref productDTO, product);
            responseDTO.Status = true;
            responseDTO.Message = "Product single by Id";
            responseDTO.Data = productDTO;
            return responseDTO;
        }

        public ResponseDTO SearchDCProductsByProductName(string productName)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            List<ProductShoppingDTO> productList = new List<ProductShoppingDTO>();
            var products = unitOfWork.ProductRepository.GetProductsByProductName(productName);
            if (products != null)
            {
                foreach (var product in products)
                    productList.Add(ProductConvertor.ConvertToProductShoppingDTO(product, NatrajConfigurationHelper.ImagePath));
                responseDTO.Status = true;
                responseDTO.Message = "Product List";
                responseDTO.Data = productList;
                return responseDTO;
            }
            else
            {
                throw new PlatformModuleException("No Products Found");
            }
        }

        public ResponseDTO UpdateProduct(ProductDTO productDTO)
        {
            throw new NotImplementedException();
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (unitOfWork != null)
                {
                    unitOfWork.Dispose();
                    unitOfWork = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}
