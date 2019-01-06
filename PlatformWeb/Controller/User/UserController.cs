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
    [Route("api/Users")]
    public class UserController : ApiController
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET api/Customer

        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_userService.GetAllUsers());
            }
            catch (PlatformModuleException ex)
            {
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }

        }


        //GET api/Customer/id
        [Route("api/Users/{id}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(_userService.GetUserById(id));
            }
            catch (PlatformModuleException ex)
            {
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }

        //Post api/Customer

        public IHttpActionResult Post([FromBody]UserDTO userDTO)
        {
            try
            {
                if (userDTO == null)
                    Ok(ResponseHelper.CreateResponseDTOForException("Argument Null"));
                //Create New Customer
                _userService.AddUser(userDTO);

                return Ok();
            }
            catch (PlatformModuleException ex)
            {
                //Write Log Here
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }

        //Post api/Customer/5
        [Route("api/Users/{id}")]
        public IHttpActionResult Post(int id, [FromBody]UserDTO userDTO)
        {
            try
            {
                userDTO.UserId = id;
                if (userDTO == null)
                    Ok(ResponseHelper.CreateResponseDTOForException("Argument Null"));
                //Update New Customer
                _userService.UpdateUser(userDTO);

                return Ok();
            }
            catch (PlatformModuleException ex)
            {
                //Write Log Here
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }

        [Route("api/Users/id/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                //Delete Customer
                _userService.DeleteUser(id);
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
