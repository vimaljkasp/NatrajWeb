using Platform.DTO;
using Platform.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service
{
    public class LoggedInUserConvertor
    {
        public static LoggedInUserDTO ConvertToLoggedInUserDTO(VLC vLC)
        {
            LoggedInUserDTO loggedInUserDTO = new LoggedInUserDTO();
            loggedInUserDTO.Id = vLC.VLCId;
            loggedInUserDTO.Code = vLC.VLCCode;
            loggedInUserDTO.Name = vLC.VLCName;
            loggedInUserDTO.EnrollmentDate = vLC.VLCEnrollmentDate;
            loggedInUserDTO.AgentName = vLC.AgentName;
            loggedInUserDTO.Contact = vLC.Contact;
            loggedInUserDTO.LoginStatus = true;
            loggedInUserDTO.Email = vLC.Email;
            loggedInUserDTO.Address = vLC.VLCAddress;
            loggedInUserDTO.Village = vLC.Village;
            loggedInUserDTO.City = vLC.City;
            loggedInUserDTO.State = vLC.VLCState;
            loggedInUserDTO.AgentAadhaar = vLC.VLCAgentAadhaar; 
            return loggedInUserDTO;

        }

        public static LoggedInUserDTO ConvertToLoggedInDistributionCenterDTO(DistributionCenter distributionCenter)
        {
            LoggedInUserDTO loggedInUserDTO = new LoggedInUserDTO();
            loggedInUserDTO.Id = distributionCenter.DCId;
            loggedInUserDTO.Code = distributionCenter.DCCode;
            loggedInUserDTO.Name = distributionCenter.DCName;
            loggedInUserDTO.EnrollmentDate = distributionCenter.DateOfRegistration;
            loggedInUserDTO.AgentName = distributionCenter.AgentName;
            loggedInUserDTO.Contact = distributionCenter.Contact;
            loggedInUserDTO.LoginStatus = true;
            loggedInUserDTO.Email = distributionCenter.Email;
            DCAddress dCAddress = distributionCenter.DCAddresses.Where(d => d.IsDefaultAddress).FirstOrDefault();
            if (dCAddress != null)
            {
                loggedInUserDTO.Address = dCAddress.Address;
                loggedInUserDTO.Village = dCAddress.AddressTypeId.ToString();
                loggedInUserDTO.City = dCAddress.City;
                loggedInUserDTO.State = dCAddress.State;
            }
            loggedInUserDTO.AgentAadhaar = distributionCenter.AADHAR;
            return loggedInUserDTO;

        }




    }
}
