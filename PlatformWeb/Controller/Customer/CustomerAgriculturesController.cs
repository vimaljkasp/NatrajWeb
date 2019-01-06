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
    [Route("api/CustomerAgricultures")]
    public class CustomerAgriculturesController : ApiController
    {

        private readonly ICustomerAgricultureService _customerAgricultureService;

        public CustomerAgriculturesController(ICustomerAgricultureService customerAgricultureService)
        {
            _customerAgricultureService = customerAgricultureService;
        }

        // GET api/Customer

        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_customerAgricultureService.GetAllCustomerAgriCultures());
            }
            catch (PlatformModuleException ex)
            {
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }

        }


        //GET api/Customer/id
        [Route("api/CustomerAgricultures/{id}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(_customerAgricultureService.GetCustomerAgriCultureById(id));
            }
            catch (PlatformModuleException ex)
            {
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }

        //Post api/Customer

        public IHttpActionResult Post([FromBody]CustomerAgricultureDTO customerAgricultureDTO)
        {
            try
            {
                if (customerAgricultureDTO == null)
                    return Ok(ResponseHelper.CreateResponseDTOForException("Argument Null"));
                //Create New Customer
              ResponseDTO responseDTO=  _customerAgricultureService.AddCustomerAgriculture(customerAgricultureDTO);

                return Ok(responseDTO);
            }
            catch (PlatformModuleException ex)
            {
                //Write Log Here
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }

        //Post api/Customer/5
        [Route("api/CustomerAgricultures/{id}")]
        public IHttpActionResult Post(int id, [FromBody]CustomerAgricultureDTO customerAgricultureDTO)
        {
            try
            {
                customerAgricultureDTO.CustomerId = id;
                if (customerAgricultureDTO == null)
                    return Ok(ResponseHelper.CreateResponseDTOForException("Argument Null"));
                //Update New Customer
                ResponseDTO responseDTO = _customerAgricultureService.UpdateCustomerAgriculture(customerAgricultureDTO);

                return Ok(responseDTO);
            }
            catch (PlatformModuleException ex)
            {
                //Write Log Here
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }

        [Route("api/CustomerAgricultures/id/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                //Delete Customer
                ResponseDTO responseDTO = _customerAgricultureService.DeleteCustomerAgriculture(id);
                return Ok(responseDTO);
            }
            catch (PlatformModuleException ex)
            {
                //Write Log Here
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }
    }
}
