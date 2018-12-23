using Platform.DTO;
using Platform.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service
{
    public class VLCConvertor
    {
        public static VLCDTO ConvertToVLCDto(VLC vLC)
        {
            VLCDTO vlcDto = new VLCDTO();
            vlcDto.VLCId = vLC.VLCId;
            vlcDto.VLCCode = vLC.VLCCode;
            vlcDto.VLCName = vLC.VLCName;
            vlcDto.VLCEnrollmentDate = vLC.VLCEnrollmentDate.HasValue ? vLC.VLCEnrollmentDate.Value : DateTime.MinValue;
            vlcDto.AgentName = vLC.AgentName;
            vlcDto.Contact = vLC.Contact;
            vlcDto.Email = vLC.Email;
            vlcDto.VLCAddress = vLC.VLCAddress;
            vlcDto.Village = vLC.Village;
            vlcDto.City = vLC.City;
            vlcDto.Password = vLC.Password;
            vlcDto.AlternateContact = vLC.AlternateContact;
            vlcDto.CreatedBy = vLC.CreatedBy;
            vlcDto.CreatedDate = vLC.CreatedDate;
            vlcDto.IsDeleted = vLC.IsDeleted;
            vlcDto.ModifiedBy = vLC.ModifiedBy;
            vlcDto.ModifiedDate = vLC.ModifiedDate;
            vlcDto.VLCAgentAadhaar = vLC.VLCAgentAadhaar;

            return vlcDto;


}

        public static void ConvertToVLCEntity(ref VLC vLC, VLCDTO vlcDTO, bool isUpdate)
        {
            if (isUpdate)
                vLC.VLCId = vlcDTO.VLCId;
            vLC.VLCCode = vlcDTO.VLCCode;
            vLC.VLCName = vlcDTO.VLCName;
            vLC.VLCEnrollmentDate = vlcDTO.VLCEnrollmentDate.HasValue ? vlcDTO.VLCEnrollmentDate.Value : DateTime.MinValue;
            if(string.IsNullOrWhiteSpace(vlcDTO.AgentName))
            vLC.AgentName = vlcDTO.AgentName;
            if (string.IsNullOrWhiteSpace(vlcDTO.Contact))
                vLC.Contact = vlcDTO.Contact;
            if (string.IsNullOrWhiteSpace(vlcDTO.Email))
                vLC.Email = vlcDTO.Email;
            if (string.IsNullOrWhiteSpace(vlcDTO.VLCAddress))
                vLC.VLCAddress = vlcDTO.VLCAddress;
            if (string.IsNullOrWhiteSpace(vlcDTO.Village))
                vLC.Village = vlcDTO.Village;
            if (string.IsNullOrWhiteSpace(vlcDTO.City))
                vLC.City = vlcDTO.City;
            if (string.IsNullOrWhiteSpace(vlcDTO.Password))
                vLC.Password = vlcDTO.Password;
            if (string.IsNullOrWhiteSpace(vlcDTO.AlternateContact))
                vLC.AlternateContact = vlcDTO.AlternateContact;
            if (string.IsNullOrWhiteSpace(vlcDTO.CreatedBy))
                vLC.CreatedBy = vlcDTO.CreatedBy;
            vLC.CreatedDate = vlcDTO.CreatedDate;

            vLC.IsDeleted = false;
            if (string.IsNullOrWhiteSpace(vlcDTO.ModifiedBy))
                vLC.ModifiedBy = vlcDTO.ModifiedBy;
            vLC.ModifiedDate = vlcDTO.ModifiedDate;
            if (string.IsNullOrWhiteSpace(vlcDTO.VLCAgentAadhaar))
                vLC.VLCAgentAadhaar = vlcDTO.VLCAgentAadhaar;


        }
    }
}
