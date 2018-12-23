using Platform.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlatformWeb
{
    public static class ResponseHelper
    {
        public static ResponseDTO CreateResponseDTOForException(string message)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            responseDTO.Status = false;
            responseDTO.Message = message;
            responseDTO.Data = new object();
            return responseDTO;
        }
    }
}