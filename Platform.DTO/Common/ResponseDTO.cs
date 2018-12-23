using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public class ResponseDTO
    {
        //public ResponseDTO()
        //{
        //    this.Value = new T();
        //}
        public string Message { get; set; }

        public bool Status { get; set; }

        public Object Data { get; set; }
    }
}
