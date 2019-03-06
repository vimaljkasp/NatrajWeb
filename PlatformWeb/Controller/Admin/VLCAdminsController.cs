using Platform.DTO;
using Platform.Utilities;
using System.Web.Http;

namespace PlatformWeb.Controller
{
    [Route("api/VLCAdminsController")]
    public class VLCAdminsController : ApiController
    {
        private readonly IVLCAdminService _adminService;

        public VLCAdminsController(IVLCAdminService adminService)
        {
            _adminService = adminService;
        }
        //Post api/Customer/5
        [Route("api/updateMilkRate/")]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_adminService.UpdateMilkFixedRateDetails());
            }
            catch (PlatformModuleException ex)
            {
                //Write Log Here
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }
    }
}