using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public class LoggedInUserDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        public string AgentName { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Village { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string LoginType { get; set; }
      

        public string TokenString { get; set; }
 
        public string AgentAadhaar { get; set; }
        
        public bool LoginStatus { get; set; }
    }
}
