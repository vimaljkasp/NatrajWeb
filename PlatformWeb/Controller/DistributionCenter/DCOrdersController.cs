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



        [Route("api/GetDCOrdersByStatus/{id}")]
        public IHttpActionResult GetDCOrdersByStatus(int id,[FromUri] string orderStatus)
        {
            try
            {
                return Ok(_dCOrderService.GetDCOrdersByOrderStatus(id,orderStatus));
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
        public IHttpActionResult Post(int id, [FromBody]CreateDCOrderDTO dCOrderDTO)
        {
            try
            {
                dCOrderDTO.DCOrderId = id;
                if (dCOrderDTO == null)
                    Ok(ResponseHelper.CreateResponseDTOForException("Argument Null"));
               
            var responseDTO= _dCOrderService.UpdateDCOrder(dCOrderDTO);

                return Ok(responseDTO);
            }
            catch (PlatformModuleException ex)
            {
                //Write Log Here
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }

        [Route("api/UpdateDCOrderStatus/{id}")]
        public IHttpActionResult UpdateDCOrderStatus(int id,[FromBody] DCOrderStatusDTO dCOrderStatusDTO)
        {
            try
            {
               
                dCOrderStatusDTO.DCOrderId = id;
                if (dCOrderStatusDTO == null)
                    Ok(ResponseHelper.CreateResponseDTOForException("Argument Null"));
                
                

                return Ok(_dCOrderService.UpdateDCOrderStatus(dCOrderStatusDTO));
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
                
              
                return Ok(_dCOrderService.DeleteDCOrder(id));
            }
            catch (PlatformModuleException ex)
            {
                //Write Log Here
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }
    }
}
