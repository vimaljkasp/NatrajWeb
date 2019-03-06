using Platform.DTO;
using Platform.Utilities;

using System.Web.Http;

namespace PlatformWeb.Controller
{
    [Route("api/DistributionCenters")]
    public class DistributionCentersController : ApiController
    {

        private readonly IDistributionCenterService _distributionCenterService;

        public DistributionCentersController(IDistributionCenterService distributionCenter)
        {
            _distributionCenterService = distributionCenter;
        }

        // GET api/Customer

        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_distributionCenterService.GetAllDistributionCenters());
            }
            catch (PlatformModuleException ex)
            {
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }

        }

        [HttpPost]
        [Route("api/GetDCCentersByCity")]
        public IHttpActionResult GetDCCentersByCity([FromUri] string city, [FromUri] int pageNumber)
        {
            try
            {
                return Ok(_distributionCenterService.GetDistributionCentersByCity(city, pageNumber));
            }
            catch (PlatformModuleException ex)
            {
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }

        }




        //GET api/Customer/id
        [Route("api/DistributionCenters/{id}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(_distributionCenterService.GetDistributionCenterByCenterId(id));
            }
            catch (PlatformModuleException ex)
            {
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }

        //Post api/Customer

        public IHttpActionResult Post([FromBody]DistributionCenterDTO distributionCenterDTO)
        {
            try
            {
                if (distributionCenterDTO == null)
                    Ok(ResponseHelper.CreateResponseDTOForException("Argument Null"));
                //Create New Distribution Center
                ResponseDTO responseDTO = _distributionCenterService.AddDistributionCenter(distributionCenterDTO);

                return Ok(responseDTO);
            }
            catch (PlatformModuleException ex)
            {
                //Write Log Here
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }

        //Post api/Customer/5
        [Route("api/DistributionCenters/{id}")]
        public IHttpActionResult Post(int id, [FromBody]DistributionCenterDTO distributionCenterDTO)
        {
            try
            {
                distributionCenterDTO.DCId = id;
                if (distributionCenterDTO == null)
                    Ok(ResponseHelper.CreateResponseDTOForException("Argument Null"));
                //Update New Customer
             

                return Ok(_distributionCenterService.UpdateDistributionCenter(distributionCenterDTO));
            }
            catch (PlatformModuleException ex)
            {
                //Write Log Here
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }


        [HttpPost]
        [Route("api/UpdateDCStatus/{id}")]
        public IHttpActionResult UpdateDCStatus(int id, [FromUri]bool isActive)
        {
            try
            {
               
                if (id <= 0)
                    Ok(ResponseHelper.CreateResponseDTOForException("DC Id Not Valid"));
                //Update New Customer


                return Ok(_distributionCenterService.UpdateDistributionCenterStatus(id, isActive));
            }
            catch (PlatformModuleException ex)
            {
                //Write Log Here
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }



        [Route("api/DeleteDistributionCenters/{id}")]
        [HttpPost]
        public IHttpActionResult DeleteDistributionCenter(int id)
        {
            try
            {
                //Delete Customer
               
                return Ok(_distributionCenterService.DeleteDistriubtionCenter(id));
            }
            catch (PlatformModuleException ex)
            {
                //Write Log Here
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }
    }
}
