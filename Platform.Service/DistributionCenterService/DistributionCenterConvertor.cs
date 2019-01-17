using Platform.DTO;
using Platform.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service
{
   public class DistributionCenterConvertor
    {
        public static DistributionCenterDTO ConvertToDistributionCenterDto(DistributionCenter distributionCenter)
        {
            DistributionCenterDTO distributionCenterDTO = new DistributionCenterDTO();
            distributionCenterDTO.AADHAR = distributionCenter.AADHAR;
            distributionCenterDTO.AgentName = distributionCenter.AgentName;
            distributionCenterDTO.AlternateContact = distributionCenter.AlternateContact;
            distributionCenterDTO.Anniversary = distributionCenter.Anniversary.HasValue ? distributionCenter.Anniversary.Value : DateTime.MinValue;
            distributionCenterDTO.Contact = distributionCenter.Contact;
            distributionCenterDTO.CreatedBy = distributionCenter.CreatedBy;
            distributionCenterDTO.CreatedDate = distributionCenter.CreatedDate;
            distributionCenterDTO.DateOfRegistration = distributionCenter.DateOfRegistration.HasValue ? distributionCenter.DateOfRegistration.Value : DateTime.MinValue;
            distributionCenterDTO.DCCode = distributionCenter.DCCode;
            distributionCenterDTO.DCId = distributionCenter.DCId;
            distributionCenterDTO.DCName = distributionCenter.DCName;
            distributionCenterDTO.DOB = distributionCenter.DOB.HasValue ? distributionCenter.DOB.Value : DateTime.MinValue;
            distributionCenterDTO.Email = distributionCenter.Email;
            distributionCenterDTO.FatherName = distributionCenter.FatherName;
            distributionCenterDTO.ModifiedBy = distributionCenter.ModifiedBy;
            distributionCenterDTO.ModifiedDate = distributionCenter.ModifiedDate.HasValue ? distributionCenter.ModifiedDate.Value : DateTime.MinValue;
            distributionCenterDTO.NoOfEmployee = distributionCenter.NoOfEmployee.GetValueOrDefault();
            if (distributionCenter.DCWallets != null)
                distributionCenterDTO.DcWalletBalance = distributionCenter.DCWallets.FirstOrDefault().WalletBalance;
            if (distributionCenter.DCAddresses != null)
                distributionCenterDTO.DCAddressDTO = DCAddressConvertor.ConvertToDCAddressDTO(distributionCenter.DCAddresses.FirstOrDefault());

            return distributionCenterDTO;
        }

        public static void ConvertToDistributionCenterEntity(ref DistributionCenter distributionCenter, DistributionCenterDTO distributionCenterDTO, bool isUpdate)
        {

            if (string.IsNullOrWhiteSpace(distributionCenterDTO.AADHAR) == false)
                distributionCenter.AADHAR = distributionCenterDTO.AADHAR;

            if (string.IsNullOrWhiteSpace(distributionCenterDTO.AgentName) == false)
                distributionCenter.AgentName = distributionCenterDTO.AgentName;

            if (string.IsNullOrWhiteSpace(distributionCenterDTO.AlternateContact) == false)
                distributionCenter.AlternateContact = distributionCenterDTO.AlternateContact;

            if(distributionCenterDTO.Anniversary.Date !=DateTime.MinValue.Date)
              distributionCenter.Anniversary = distributionCenterDTO.Anniversary;

            if (string.IsNullOrWhiteSpace(distributionCenterDTO.Contact) == false)
                distributionCenter.Contact = distributionCenterDTO.Contact;

            if (string.IsNullOrWhiteSpace(distributionCenterDTO.DCName) == false)
                distributionCenter.DCName = distributionCenterDTO.DCName;

            if (distributionCenterDTO.DOB.Date != DateTime.MinValue.Date)
                distributionCenter.DOB = distributionCenterDTO.DOB;

            if (string.IsNullOrWhiteSpace(distributionCenterDTO.Email) == false)
                distributionCenter.Email = distributionCenterDTO.Email;

            if (string.IsNullOrWhiteSpace(distributionCenterDTO.FatherName) == false)
                distributionCenter.FatherName = distributionCenterDTO.FatherName;
           

            if (distributionCenterDTO.NoOfEmployee>0)
                distributionCenter.NoOfEmployee = distributionCenterDTO.NoOfEmployee;


            if (string.IsNullOrWhiteSpace(distributionCenterDTO.Password) == false)
                distributionCenter.Password = distributionCenterDTO.Password;


            if (string.IsNullOrWhiteSpace(distributionCenterDTO.Pin) == false)
                distributionCenter.Pin = distributionCenterDTO.Pin;
        }
    }
}
