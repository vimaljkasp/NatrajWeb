using Platform.DTO;
using Platform.Utilities;
using System.Web.Http;

namespace PlatformWeb.Controller
{
    [Route("api/VLCPayments")]
    public class VLCPaymentsController : ApiController
    {

        private readonly IVLCPaymentService _vLCPaymentService;

        public VLCPaymentsController(IVLCPaymentService vLCPaymentService)
        {
            _vLCPaymentService = vLCPaymentService;
        }



        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_vLCPaymentService.GetAllVLCPayments());
            }
            catch (PlatformModuleException ex)
            {
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }

        }




        [Route("api/VLCPayments/{id}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(_vLCPaymentService.GetAllVLCPayementsByVLCId(id));
            }
            catch (PlatformModuleException ex)
            {
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }



        public IHttpActionResult Post([FromBody]VLCPaymentDTO vLCPaymentDTO)
        {
            try
            {
                if (vLCPaymentDTO == null)
                    Ok(ResponseHelper.CreateResponseDTOForException("Argument Null"));
                //Create New Distribution Center
                ResponseDTO responseDTO = _vLCPaymentService.AddVLCPaymentDetail(vLCPaymentDTO);

                return Ok(responseDTO);
            }
            catch (PlatformModuleException ex)
            {
                //Write Log Here
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }


        [Route("api/VLCPayments/{id}")]
        public IHttpActionResult Post(int id, [FromBody]VLCPaymentDTO vLCPaymentDTO)
        {
            try
            {
                vLCPaymentDTO.VLCPaymentId = id;
                if (vLCPaymentDTO == null)
                    Ok(ResponseHelper.CreateResponseDTOForException("Argument Null"));
                //Update New Customer


                return Ok(_vLCPaymentService.UpdateVLCPaymentDetail(vLCPaymentDTO));
            }
            catch (PlatformModuleException ex)
            {
                //Write Log Here
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }

        [Route("api/VLCPayments/id/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                //Delete Customer

                return Ok(_vLCPaymentService.DeleteVLCPaymentDetail(id));
            }
            catch (PlatformModuleException ex)
            {
                //Write Log Here
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }
    }
}
