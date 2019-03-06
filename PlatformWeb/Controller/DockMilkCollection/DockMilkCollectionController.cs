using Platform.DTO;
using Platform.Utilities;
using System;

using System.Web.Http;

namespace PlatformWeb.Controller
{
    [Route("api/DockMilkCollections")]
    public class DockMilkCollectionsController : ApiController
    {

        private readonly IDockMilkCollectionService _dockMilkCollectionService;

        public DockMilkCollectionsController(IDockMilkCollectionService dockMilkCollectionService)
        {
            _dockMilkCollectionService = dockMilkCollectionService;
        }

        // GET api/Customer

        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_dockMilkCollectionService.GetAllDockMilkCollection());
            }
            catch (PlatformModuleException ex)
            {
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }

        }


        //GET api/Customer/id
        [Route("api/DockMilkCollections/{id}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(_dockMilkCollectionService.GetDockMilkCollectionById(id));
            }
            catch (PlatformModuleException ex)
            {
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }

        [HttpPost]
        public IHttpActionResult Get( [FromUri]DateTime collectionDate, [FromUri] int shift, [FromUri] int pageNumber)
        {
            try
            {
                ResponseDTO responseDTO = _dockMilkCollectionService.GetDockMilkCollectionsByDateAndShift( collectionDate, shift, pageNumber);
                return Ok(responseDTO);
            }
            catch (PlatformModuleException ex)
            {
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }

        }

        //Post api/Customer

        public IHttpActionResult Post([FromBody]DockMilkCollectionDTO vLCMilkCollectionDTO)
        {
            try
            {
                if (vLCMilkCollectionDTO == null)
                    Ok(ResponseHelper.CreateResponseDTOForException("Argument Null"));
                //Create New Customer
                ResponseDTO responseDTO = _dockMilkCollectionService.AddDockMilkCollection(vLCMilkCollectionDTO);

                return Ok(responseDTO);
            }
            catch (PlatformModuleException ex)
            {
                //Write Log Here
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }

        //Post api/Customer/5
        [Route("api/DockMilkCollections/{id}")]
        public IHttpActionResult Post(int id, [FromBody]DockMilkCollectionDTO vLCMilkCollectionDTO)
        {
            try
            {
                vLCMilkCollectionDTO.DockMilkCollectionId = id;
                if (vLCMilkCollectionDTO == null)
                    Ok(ResponseHelper.CreateResponseDTOForException("Argument Null"));
                //Update New Customer
                ResponseDTO responseDTO = _dockMilkCollectionService.UpdateDockMilkCollection(vLCMilkCollectionDTO);

                return Ok(responseDTO);
            }
            catch (PlatformModuleException ex)
            {
                //Write Log Here
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }

        [Route("api/DeleteDockMilkCollection/{id}")]
        [HttpPost]
        public IHttpActionResult DeleteDockMilkCollection(int id)
        {
            try
            {
                //Delete Customer
                ResponseDTO responseDTO = _dockMilkCollectionService.DeleteDockMilkCollection(id);
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
