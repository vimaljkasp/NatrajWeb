﻿
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Platform.Service;
using Platform.Utilities.ExceptionHandler;
using System.Configuration;
using Platform.DTO;

namespace PlatformWeb.Controller
{

    [Route("api/login")]
    public class LoginController : ApiController
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [AllowAnonymous]
        public IHttpActionResult Post([FromUri]String userName,[FromUri]String password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userName))
                    return Ok(ResponseHelper.CreateResponseDTOForException("UserName cannot be Balank"));
                else if (string.IsNullOrWhiteSpace(password))
                    return Ok(ResponseHelper.CreateResponseDTOForException("Password cannot be Balank"));
                else
                {
                    LoggedInUserDTO loggedInUserDTO = _loginService.ValidateLoginAndCreateLoginSession(userName, password);
                    if (loggedInUserDTO.LoginStatus)
                    {

                        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("PlatformSecretKey"));
                        var signingCredientials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                        string baseAddress = ConfigurationManager.AppSettings["BaseUri"].ToString();
                        var tokenOptions = new JwtSecurityToken(
                            issuer: baseAddress,
                            audience: baseAddress,
                            claims: new List<Claim>(),
                            expires: DateTime.Now.AddMinutes(6),
                            signingCredentials: signingCredientials);
                        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                        loggedInUserDTO.TokenString = tokenString;
                        ResponseDTO responseDTO = new ResponseDTO();
                        responseDTO.Data = loggedInUserDTO;
                        responseDTO.Status = true;
                        responseDTO.Message = "Login Successful";
                        return Ok(responseDTO);
                    }
                    else
                    {
                        return Ok(ResponseHelper.CreateResponseDTOForException("User Name or Password is InCorrect"));
                    }
                }


            }
            catch (PlatformModuleException ex)
            {
                //Write Log Here
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }
    }
}