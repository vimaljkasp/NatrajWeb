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
    [Route("api/CustomerBanks")]
    public class CustomerBanksController : ApiController
    {

        private readonly ICustomerBankService _customerBankService;

        public CustomerBanksController(ICustomerBankService customerBankService)
        {
            _customerBankService = customerBankService;
        }

        // GET api/CustomerBank

        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_customerBankService.GetAllCustomerBanks());
            }
            catch (PlatformModuleException ex)
            {
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }

        }


        //GET api/Customer/id
        [Route("api/customerBanks/{id}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(_customerBankService.GetCustomerBankById(id));
            }
            catch (PlatformModuleException ex)
            {
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }

        //Post api/Customer

        public IHttpActionResult Post([FromBody]CustomerBankDTO customerBank)
        {
            try
            {
                if (customerBank == null)
                    Ok(ResponseHelper.CreateResponseDTOForException("Argument Null"));
                //Create New Customer
              ResponseDTO responseDTO=  _customerBankService.AddCustomerBank(customerBank);

                return Ok(responseDTO);
            }
            catch (PlatformModuleException ex)
            {
                //Write Log Here
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }

        //Put api/Customer/5
        [Route("api/customerBanks/{id}")]
        public IHttpActionResult Post(int id, [FromBody]CustomerBankDTO customerBankDTO)
        {
            try
            {
                customerBankDTO.CustomerId = id;
                if (customerBankDTO == null)
                return  Ok(ResponseHelper.CreateResponseDTOForException("Argument Null"));
                //Update New Customer
             

                return Ok(_customerBankService.UpdateCustomerBank(customerBankDTO));
            }
            catch (PlatformModuleException ex)
            {
                //Write Log Here
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }

        [Route("api/customerBanks/id/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                //Delete Customer
                ResponseDTO responseDTO = _customerBankService.DeleteCustomerBank(id);
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
