using Platform.DTO;
using Platform.Utilities;
using System.Web.Http;

namespace PlatformWeb.Controller
{
    [Route("api/Customers")]
    public class CustomersController : ApiController
    {

        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET api/Customer

        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_customerService.GetAllCustomers());
            }
            catch (PlatformModuleException ex)
            {
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }

        }

        [HttpPost]
        [Route("api/GetCustomerListByVLCId/{id}")]
        public IHttpActionResult GetCustomerListByVLCId(int id, [FromUri] int pageNumber)
        {
            try
            {
                return Ok(_customerService.GetCustomerListByVLCId(id, pageNumber));
            }
            catch (PlatformModuleException ex)
            {
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }

        }


        [HttpPost]
        [Route("api/GetCustomerDetailsByCustomerId/{id}")]
        public IHttpActionResult GetCustomerDetailsByCustomerId(int id)
        {
            try
            {
                return Ok(_customerService.GetCustomerDetailsByCustomerId(id));
            }
            catch (PlatformModuleException ex)
            {
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }

        }

        [HttpPost]
        [Route("api/GetCustomerListForSearchByVLCId/{id}")]
        public IHttpActionResult GetCustomerListForSearchByVLCId(int id)
        {
            try
            {
                return Ok(_customerService.GetCustomerListForSearchByVLCId(id));
            }
            catch (PlatformModuleException ex)
            {
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }

      //GET api/Customer/id
      [Route("api/customers/{id}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(_customerService.GetCustomerByCustomerId(id));
            }
            catch (PlatformModuleException ex)
            {
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }

        //Post api/Customer

        public IHttpActionResult Post([FromBody]CustomerDto customer)
        {
            try
            {
                if (customer == null)
                    return Ok(ResponseHelper.CreateResponseDTOForException("Argument Null"));
                //Create New Customer
                ResponseDTO responseDTO = _customerService.AddCustomer(customer);

                return Ok(responseDTO);
            }
            catch (PlatformModuleException ex)
            {
                //Write Log Here
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }

        //Post api/Customer/5
        [Route("api/customers/{id}")]
        public IHttpActionResult Post(int id, [FromBody]CustomerDto customerDTO)
        {
            try
            {
                customerDTO.CustomerId = id;
                if (customerDTO == null)
                    return Ok(ResponseHelper.CreateResponseDTOForException("Argument Null"));
                
            

                return Ok(_customerService.UpdateCustomer(customerDTO));
            }
            catch (PlatformModuleException ex)
            {
                //Write Log Here
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }

        [Route("api/customers/id/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                //Delete Customer
                _customerService.DeleteCustomer(id);
                return Ok();
            }
            catch (PlatformModuleException ex)
            {
                //Write Log Here
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }
    }
}
