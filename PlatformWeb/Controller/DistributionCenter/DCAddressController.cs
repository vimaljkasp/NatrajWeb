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
    [Route("api/DCAddresses")]
    public class DCAddressessController : ApiController
    {

        private readonly IDCAddressService _dCAddressService;

        public DCAddressessController(IDCAddressService dCAddressService)
        {
            _dCAddressService = dCAddressService;
        }

        // GET api/Customer

        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_dCAddressService.GetAllDCAddresses());
            }
            catch (PlatformModuleException ex)
            {
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }

        }

        //[HttpPost]
        //[Route("api/GetCustomerListByVLCId/{id}")]
        //public IHttpActionResult GetCustomerListByVLCId(int id, [FromUri] int pageNumber)
        //{
        //    try
        //    {
        //        return Ok(_customerService.GetCustomerListByVLCId(id, pageNumber));
        //    }
        //    catch (PlatformModuleException ex)
        //    {
        //        return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
        //    }

        //}


        //[HttpPost]
        //[Route("api/GetCustomerDetailsByCustomerId/{id}")]
        //public IHttpActionResult GetCustomerDetailsByCustomerId(int id)
        //{
        //    try
        //    {
        //        return Ok(_customerService.GetCustomerDetailsByCustomerId(id));
        //    }
        //    catch (PlatformModuleException ex)
        //    {
        //        return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
        //    }

        //}


        //GET api/Customer/id
        [Route("api/DCAddresses/{id}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(_dCAddressService.GetDefaultDCAddressByDCId(id));
            }
            catch (PlatformModuleException ex)
            {
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }

        //Post api/Customer

        public IHttpActionResult Post([FromBody]DCAddressDTO dCAddressDTO)
        {
            try
            {
                if (dCAddressDTO == null)
                    Ok(ResponseHelper.CreateResponseDTOForException("Argument Null"));
                //Create New Distribution Center
                ResponseDTO responseDTO = _dCAddressService.AddDCAddress(dCAddressDTO);

                return Ok(responseDTO);
            }
            catch (PlatformModuleException ex)
            {
                //Write Log Here
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }

        //Put api/Customer/5
        [Route("api/DCAddresses/{id}")]
        public IHttpActionResult Put(int id, [FromBody]DCAddressDTO dCAddressDTO)
        {
            try
            {
                dCAddressDTO.DCAddressId = id;
                if (dCAddressDTO == null)
                    Ok(ResponseHelper.CreateResponseDTOForException("Argument Null"));
                //Update New Customer
                _dCAddressService.UpadeDCAddress(dCAddressDTO);

                return Ok();
            }
            catch (PlatformModuleException ex)
            {
                //Write Log Here
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }

        [Route("api/DCAddresses/id/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                //Delete Customer
                _dCAddressService.DeleteDCAddress(id);
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
