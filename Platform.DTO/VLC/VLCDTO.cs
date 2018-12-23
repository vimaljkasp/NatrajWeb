using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
   public class VLCDTO
    {

        public int VLCId { get; set; }
        public string VLCCode { get; set; }
        public string VLCName { get; set; }
        public DateTime? VLCEnrollmentDate { get; set; }
        public string AgentName { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string VLCAddress { get; set; }
        public string Village { get; set; }
        public string City { get; set; }
        public string VLCState { get; set; }
        public string Password { get; set; }
        public string AlternateContact { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string VLCAgentAadhaar { get; set; }
    }
}
