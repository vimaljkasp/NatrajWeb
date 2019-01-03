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

        //Put api/Customer/5
        [Route("api/DistributionCenters/{id}")]
        public IHttpActionResult Put(int id, [FromBody]DistributionCenterDTO distributionCenterDTO)
        {
            try
            {
                distributionCenterDTO.DCId = id;
                if (distributionCenterDTO == null)
                    Ok(ResponseHelper.CreateResponseDTOForException("Argument Null"));
                //Update New Customer
                _distributionCenterService.UpdateDistributionCenter(distributionCenterDTO);

                return Ok();
            }
            catch (PlatformModuleException ex)
            {
                //Write Log Here
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }

        [Route("api/DistributionCenters/id/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                //Delete Customer
                _distributionCenterService.DeleteDistriubtionCenter(id);
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
