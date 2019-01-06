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
    [Route("api/VLCMilkCollections")]
    public class VLCMilkCollectionsController : ApiController
    {

        private readonly IVLCMilkCollectionService _vlcMilkCollectionService;

        public VLCMilkCollectionsController(IVLCMilkCollectionService vlcMilkCollectionService)
        {
            _vlcMilkCollectionService = vlcMilkCollectionService;
        }

        // GET api/Customer

        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_vlcMilkCollectionService.GetAllVLCMilkCollection());
            }
            catch (PlatformModuleException ex)
            {
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }

        }


        //GET api/Customer/id
        [Route("api/VLCMilkCollections/{id}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(_vlcMilkCollectionService.GetVLCMilkCollectionById(id));
            }
            catch (PlatformModuleException ex)
            {
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }

        [HttpPost]
        public IHttpActionResult Get([FromUri] int vlcId,[FromUri]DateTime collectionDate,[FromUri] int shift,[FromUri] int pageNumber)
        {
            try
            {
                ResponseDTO responseDTO = _vlcMilkCollectionService.GetVLCCustomerCollectionsByDateAndShift(vlcId, collectionDate, shift, pageNumber);
                return Ok(responseDTO);
            }
            catch (PlatformModuleException ex)
            {
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }

        }

        //Post api/Customer

        public IHttpActionResult Post([FromBody]VLCMilkCollectionDTO vLCMilkCollectionDTO)
        {
            try
            {
                if (vLCMilkCollectionDTO == null)
                    Ok(ResponseHelper.CreateResponseDTOForException("Argument Null"));
                //Create New Customer
              ResponseDTO responseDTO= _vlcMilkCollectionService.AddVLCMilkCollection(vLCMilkCollectionDTO);

                return Ok(responseDTO);
            }
            catch (PlatformModuleException ex)
            {
                //Write Log Here
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }

        //Post api/Customer/5
        [Route("api/VLCMilkCollections/{id}")]
        public IHttpActionResult Post(int id, [FromBody]VLCMilkCollectionDTO vLCMilkCollectionDTO)
        {
            try
            {
                vLCMilkCollectionDTO.VLCMilkCollectionId = id;
                if (vLCMilkCollectionDTO == null)
                    Ok(ResponseHelper.CreateResponseDTOForException("Argument Null"));
                //Update New Customer
                ResponseDTO responseDTO = _vlcMilkCollectionService.UpdateVLCMilkCollection(vLCMilkCollectionDTO);

                return Ok(responseDTO);
            }
            catch (PlatformModuleException ex)
            {
                //Write Log Here
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }

        [Route("api/DeleteVLCMilkCollection/{id}")]
        [HttpPost]
        public IHttpActionResult DeleteVLCMilkCollection(int id)
        {
            try
            {
                //Delete Customer
                ResponseDTO responseDTO =  _vlcMilkCollectionService.DeleteVLCMilkCollection(id);
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
