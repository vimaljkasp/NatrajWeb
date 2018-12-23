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
    [Route("api/Vlcs")]
    public class VLCController : ApiController
    {

        private readonly IVLCService _vlcService;

        public VLCController(IVLCService vLCService)
        {
            _vlcService = vLCService;
        }

        // GET api/Customer

        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_vlcService.GetAllVLCAgents());
            }
            catch (PlatformModuleException ex)
            {
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }

        }


        public IHttpActionResult Get([FromUri] int? pageNumber,[FromUri] int? count)
        {
            try
            {
                return Ok(_vlcService.GetAllVLCAgentsByPageCount(pageNumber,count));
            }
            catch (PlatformModuleException ex)
            {
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }

        }

        //GET api/Customer/id
        [Route("api/vlcs/{id}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(_vlcService.GetVLCById(id));
            }
            catch (PlatformModuleException ex)
            {
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }

        //Post api/Customer

        public IHttpActionResult Post([FromBody]VLCDTO vLCDTO)
        {
            try
            {
                if (vLCDTO == null)
                    Ok(ResponseHelper.CreateResponseDTOForException("Argument Null"));
                //Create New Customer
                _vlcService.AddVLC(vLCDTO);

                return Ok();
            }
            catch (PlatformModuleException ex)
            {
                //Write Log Here
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }

        //Put api/Customer/5
        [Route("api/vlcs/{id}")]
        public IHttpActionResult Put(int id, [FromBody]VLCDTO vLCDTO)
        {
            try
            {
                vLCDTO.VLCId = id;
                if (vLCDTO == null)
                    Ok(ResponseHelper.CreateResponseDTOForException("Argument Null"));
                //Update New Customer
                _vlcService.UpdateVLC(vLCDTO);

                return Ok();
            }
            catch (PlatformModuleException ex)
            {
                //Write Log Here
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }

        [Route("api/vlcs/id/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                //Delete Customer
                _vlcService.DeleteVLC(id);
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
