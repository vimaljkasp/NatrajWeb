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
            vlcDto.VLCState = vLC.VLCState;
            vlcDto.Password = vLC.Password;
            vlcDto.AlternateContact = vLC.AlternateContact;
            vlcDto.CreatedBy = vLC.CreatedBy;
            vlcDto.CreatedDate = vLC.CreatedDate;
            vlcDto.IsDeleted = vLC.IsDeleted;
            vlcDto.ModifiedBy = vLC.ModifiedBy;
            vlcDto.ModifiedDate = vLC.ModifiedDate;
            vlcDto.VLCAgentAadhaar = vLC.VLCAgentAadhaar;
            vlcDto.CLR = vLC.CLR;
            vlcDto.FAT = vLC.FAT;
            vlcDto.DifferenceCLR = vLC.DifferenceCLR;
            vlcDto.ApplicableRate = vLC.ApplicableRate;
            vlcDto.Shift = vLC.CurrentShift.GetValueOrDefault();
            vlcDto.HouseRent = vLC.HouseRent.GetValueOrDefault();
            vlcDto.MachineRent = vLC.MachineRent.GetValueOrDefault();
            vlcDto.MilkCommission = vLC.MilkCommission.GetValueOrDefault();
            return vlcDto;


        }

        public static void ConvertToVLCEntity(ref VLC vLC, VLCDTO vlcDTO, bool isUpdate)
        {

            if (string.IsNullOrWhiteSpace(vlcDTO.VLCCode) == false)
                vLC.VLCCode = vlcDTO.VLCCode;
            if (string.IsNullOrWhiteSpace(vlcDTO.VLCName) == false)
                vLC.VLCName = vlcDTO.VLCName;
            if (string.IsNullOrWhiteSpace(vlcDTO.AgentName) == false)
                vLC.AgentName = vlcDTO.AgentName;
            if (string.IsNullOrWhiteSpace(vlcDTO.Contact) == false)
                vLC.Contact = vlcDTO.Contact;
            if (string.IsNullOrWhiteSpace(vlcDTO.Email) == false)
                vLC.Email = vlcDTO.Email;
            if (string.IsNullOrWhiteSpace(vlcDTO.VLCAddress) == false)
                vLC.VLCAddress = vlcDTO.VLCAddress;
            if (string.IsNullOrWhiteSpace(vlcDTO.Village) == false)
                vLC.Village = vlcDTO.Village;
            if (string.IsNullOrWhiteSpace(vlcDTO.City) == false)
                vLC.City = vlcDTO.City;
            if (string.IsNullOrWhiteSpace(vlcDTO.VLCState) == false)
                vLC.VLCState = vlcDTO.VLCState;
            if (string.IsNullOrWhiteSpace(vlcDTO.Password) == false)
                vLC.Password = vlcDTO.Password;
            if (string.IsNullOrWhiteSpace(vlcDTO.AlternateContact) == false)
                vLC.AlternateContact = vlcDTO.AlternateContact;

            vLC.IsDeleted = false;
            if (string.IsNullOrWhiteSpace(vlcDTO.VLCAgentAadhaar) == false)
                vLC.VLCAgentAadhaar = vlcDTO.VLCAgentAadhaar;
            if (vlcDTO.CLR.HasValue)
                vLC.CLR = vlcDTO.CLR.Value;
            if (vlcDTO.FAT.HasValue)
                vLC.FAT = vlcDTO.FAT.Value;
            if (vlcDTO.DifferenceCLR.HasValue)
                vLC.DifferenceCLR = vlcDTO.DifferenceCLR.Value;
            if (vlcDTO.Shift > 0)
                vLC.CurrentShift = vlcDTO.Shift;
            if (vlcDTO.ApplicableRate > 0)
                vLC.ApplicableRate = vlcDTO.ApplicableRate;
            if (vlcDTO.MachineRent > 0)
                vLC.MachineRent = vlcDTO.MachineRent;
            if (vlcDTO.HouseRent > 0)
                vLC.HouseRent = vlcDTO.HouseRent;
            if (vlcDTO.MilkCommission > 0)
                vLC.MilkCommission = vlcDTO.MilkCommission;

        }


        public static VLCWalletDTO ConvertToVLCWalletDTO(VLCWallet vLCWallet)
        {
            VLCWalletDTO vLCWalletDTO = new VLCWalletDTO();
            vLCWalletDTO.VLCId = vLCWallet.VLCId;
            vLCWalletDTO.VLCCode = vLCWallet.VLC != null ? vLCWallet.VLC.VLCCode : string.Empty;
            vLCWalletDTO.VLCName = vLCWallet.VLC != null ? vLCWallet.VLC.VLCName : string.Empty;
            vLCWalletDTO.WalletBalance = vLCWallet.WalletBalance;
            vLCWalletDTO.WalletId = vLCWallet.WalletId;
            return vLCWalletDTO;
        }

    }
}
