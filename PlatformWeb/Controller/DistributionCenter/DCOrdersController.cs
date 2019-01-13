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
    [Route("api/DCOrders")]
    public class DCOrdersController : ApiController
    {

        private readonly IDCOrderService _dCOrderService;

        public DCOrdersController(IDCOrderService dCOrderService)
        {
            _dCOrderService = dCOrderService;
        }

        // GET api/Customer

        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_dCOrderService.GetAllDCOrders());
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
        [Route("api/DCOrders/{id}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(_dCOrderService.GetDCOrdersById(id));
            }
            catch (PlatformModuleException ex)
            {
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }

        //Post api/Customer

        public IHttpActionResult Post([FromBody]CreateDCOrderDTO dCOrderDTO)
        {
            try
            {
                if (dCOrderDTO == null)
                    Ok(ResponseHelper.CreateResponseDTOForException("Argument Null"));
                //Create New Distribution Center
                ResponseDTO responseDTO = _dCOrderService.AddDCOrder(dCOrderDTO);

                return Ok(responseDTO);
            }
            catch (PlatformModuleException ex)
            {
                //Write Log Here
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }

        //Post api/Customer/5
        [Route("api/DCOrders/{id}")]
        public IHttpActionResult Post(int id, [FromBody]DCOrderDTO dCOrderDTO)
        {
            try
            {
                dCOrderDTO.DCOrderId = id;
                if (dCOrderDTO == null)
                    Ok(ResponseHelper.CreateResponseDTOForException("Argument Null"));
                //Update New Customer
                _dCOrderService.UpdateDCOrder(dCOrderDTO);

                return Ok();
            }
            catch (PlatformModuleException ex)
            {
                //Write Log Here
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }

        [Route("api/UpdateDCOrderStatus/{id}")]
        public IHttpActionResult PostUpdateOrderStatus(int id, [FromBody]DCOrderStatusDTO dCOrderDTO)
        {
            try
            {
                dCOrderDTO.DCOrderId = id;
                if (dCOrderDTO == null)
                    Ok(ResponseHelper.CreateResponseDTOForException("Argument Null"));
                //Update New Customer
                _dCOrderService.UpdateDCOrderStatus(dCOrderDTO);

                return Ok();
            }
            catch (PlatformModuleException ex)
            {
                //Write Log Here
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }

        [Route("api/DCOrders/id/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                //Delete Customer
                _dCOrderService.DeleteDCOrder(id);
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
