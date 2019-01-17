using Platform.DTO;
using Platform.Service;
using Platform.Utilities.ExceptionHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace PlatformWeb.Controller
{
    [Route("api/DCProducts")]
    public class DCProductsController : ApiController
    {

        private readonly IProductService _productService;

        public DCProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET api/Customer
        [Route("api/GetProductCategories")]
        public IHttpActionResult GetProductCategories()
        {
            try
            {
                return Ok(_productService.GetAllDCProductCategories());
            }
            catch (PlatformModuleException ex)
            {
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }

        }




        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_productService.GetAllDCProducts());
            }
            catch (PlatformModuleException ex)
            {
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }

        }

        [Route("api/DCProductsByCategoryId/{id}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(_productService.GetAllDCProductsByCategoryId(id));
            }
            catch (PlatformModuleException ex)
            {
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }

        }

        [HttpPost]
        public IHttpActionResult Get([FromUri] string productName)
        {
            try
            {
                ResponseDTO responseDTO = _productService.SearchDCProductsByProductName(productName);
                return Ok(responseDTO);
            }
            catch (PlatformModuleException ex)
            {
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }

        }

        public IHttpActionResult Post(ProductDTO productDTO)
        {
            try
            {
                if (productDTO == null)
                    Ok(ResponseHelper.CreateResponseDTOForException("Argument Null"));
                ResponseDTO responseDTO = _productService.AddProduct(productDTO);
                return Ok(responseDTO);
            }
            catch (PlatformModuleException ex)
            {
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }


  
    }
}
