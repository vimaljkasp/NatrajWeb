using Platform.DTO;
using Platform.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service
{
    public class DCAddressConvertor
    {
        public static DCAddressDTO ConvertToDCAddressDTO(DCAddress dCAddress)
        {
            DCAddressDTO dCAddressDTO = new DCAddressDTO();
            dCAddressDTO.DCAddressId = dCAddress.DCAddressId;
            dCAddressDTO.Address = dCAddress.Address;
            //    dCAddressDTO.AddressTypeId = dCAddress.AddressTypeId==1 ? "Shop" : "Other";
            dCAddressDTO.City = dCAddress.City;
            dCAddressDTO.Contact = dCAddress.Contact;
            dCAddressDTO.DCId = dCAddress.DCId;
            dCAddressDTO.District = dCAddress.District;
            //  dCAddressDTO.IsDefaultAddress = dCAddress.IsDefaultAddress;
            dCAddressDTO.PostalCode = dCAddress.PostalCode;
            dCAddressDTO.State = dCAddress.State;
            dCAddressDTO.DCName = dCAddress.DistributionCenter != null ? dCAddress.DistributionCenter.DCName : string.Empty;

            return dCAddressDTO;
        }

        public static void ConvertToDCAddressEntity(ref DCAddress dCAddress, DCAddressDTO dCAddressDTO, bool isUpdate)
        {
            dCAddress.DCId = dCAddressDTO.DCId;

            if (string.IsNullOrWhiteSpace(dCAddressDTO.Address) == false)
                dCAddress.Address = dCAddressDTO.Address;

            if (string.IsNullOrWhiteSpace(dCAddressDTO.City) == false)
                dCAddress.City = dCAddressDTO.City;

            if (string.IsNullOrWhiteSpace(dCAddressDTO.Contact) == false)
                dCAddress.Contact = dCAddressDTO.Contact;

            if (string.IsNullOrWhiteSpace(dCAddressDTO.District) == false)
                dCAddress.District = dCAddressDTO.District;

            if (string.IsNullOrWhiteSpace(dCAddressDTO.PostalCode) == false)
                dCAddress.PostalCode = dCAddressDTO.PostalCode;

            if (string.IsNullOrWhiteSpace(dCAddressDTO.State) == false)
                dCAddress.State = dCAddressDTO.State;
        }
    }
}
