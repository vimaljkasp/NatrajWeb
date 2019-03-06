using Platform.DTO;
using Platform.Utilities;
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

     

       
        [Route("api/DCPayments/{id}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(_dCPaymentService.GetAllDCPayementsByDCId(id));
            }
            catch (PlatformModuleException ex)
            {
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }

       

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

       
        [Route("api/DCPayments/{id}")]
        public IHttpActionResult Post(int id, [FromBody]DCPaymentDTO dCPaymentDTO)
        {
            try
            {
                dCPaymentDTO.DCPaymentId = id;
                if (dCPaymentDTO == null)
                    Ok(ResponseHelper.CreateResponseDTOForException("Argument Null"));
                //Update New Customer
             

                return Ok(_dCPaymentService.UpdateDCPaymentDetail(dCPaymentDTO));
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
            
                return Ok(_dCPaymentService.DeleteDCPaymentDetail(id));
            }
            catch (PlatformModuleException ex)
            {
                //Write Log Here
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }
    }
}
