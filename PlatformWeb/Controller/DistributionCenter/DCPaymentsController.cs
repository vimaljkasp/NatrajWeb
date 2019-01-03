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
    [Route("api/DCPayments")]
    public class DCPaymentsController : ApiController
    {

        private readonly IDCPaymentService _dCPaymentService;

        public DCPaymentsController(IDCPaymentService dCPaymentService)
        {
            _dCPaymentService = dCPaymentService;
        }

        // GET api/Customer

        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_dCPaymentService.GetAllDCPayments());
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
        [Route("api/DCPayments/{id}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(_dCPaymentService.GetDCPayementsByDCId(id));
            }
            catch (PlatformModuleException ex)
            {
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }

        //Post api/Customer

        public IHttpActionResult Post([FromBody]DCPaymentDTO dCPaymentDTO)
        {
            try
            {
                if (dCPaymentDTO == null)
                    Ok(ResponseHelper.CreateResponseDTOForException("Argument Null"));
                //Create New Distribution Center
                ResponseDTO responseDTO = _dCPaymentService.AddDCPaymentDetail(dCPaymentDTO);

                return Ok(responseDTO);
            }
            catch (PlatformModuleException ex)
            {
                //Write Log Here
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }

        //Put api/Customer/5
        [Route("api/DCPayments/{id}")]
        public IHttpActionResult Put(int id, [FromBody]DCPaymentDTO dCPaymentDTO)
        {
            try
            {
                dCPaymentDTO.DCPaymentId = id;
                if (dCPaymentDTO == null)
                    Ok(ResponseHelper.CreateResponseDTOForException("Argument Null"));
                //Update New Customer
                _dCPaymentService.UpadeDCPaymentDetail(dCPaymentDTO);

                return Ok();
            }
            catch (PlatformModuleException ex)
            {
                //Write Log Here
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }

        [Route("api/DCPayments/id/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                //Delete Customer
                _dCPaymentService.DeleteDCPaymentDetail(id);
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
