using Platform.DTO;

using Platform.Utilities;

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

        //Post api/Customer/5
        [Route("api/DCAddresses/{id}")]
        public IHttpActionResult Post(int id, [FromBody]DCAddressDTO dCAddressDTO)
        {
            try
            {
                dCAddressDTO.DCId = id;
                if (dCAddressDTO == null)
                    Ok(ResponseHelper.CreateResponseDTOForException("Argument Null"));
               

                return Ok(_dCAddressService.UpdateDCAddress(dCAddressDTO));
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
