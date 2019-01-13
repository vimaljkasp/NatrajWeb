using Platform.DTO;
using Platform.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service
{
    public class CustomerConvertor
    {
        public static CustomerDto ConvertToCustomerDto(Customer customer)
        {
            CustomerDto customerDto = new CustomerDto();

            if (customer != null)
            {

                customerDto.CustomerId = customer.CustomerId;
                customerDto.CustomerCodeId = customer.CustomerCode;
                customerDto.CustomerName = customer.CustomerName;
                customerDto.FatherName = customer.FatherName;
                customerDto.DOB = customer.DOB.HasValue ? customer.DOB.Value : DateTime.MinValue;
                customerDto.AddressLine = customer.CustomerAddress;
                customerDto.Village = customer.Village;
                customerDto.Tehsil = customer.Tehsil;
                customerDto.District = customer.District;
                customerDto.AddressState = customer.AddressState;

                customerDto.Contact = customer.Contact;
                customerDto.AlternateContact = customer.AlternateContact;
                customerDto.Email = customer.Email;
                customerDto.PostalCode = customer.PostalCode;
                customerDto.Gender = customer.Gender;
                customerDto.Aniversary = customer.Aniversary.GetValueOrDefault();
                customerDto.Occupation = customer.Occupation;
                customerDto.VLCId = customer.VLCId.GetValueOrDefault();
                customerDto.DateOfJoinVLC = customer.DateOfJoinVLC.GetValueOrDefault();
                customerDto.CreatedBy = customer.CreatedBy;
                customerDto.CreatedDate = customer.CreatedDate;
                customerDto.Password = string.Empty;
                customerDto.ModifiedDate = customer.ModifiedDate.GetValueOrDefault();
                customerDto.ModifiedBy = customer.ModifiedBy;
                customerDto.IsDeleted = customer.IsDeleted.GetValueOrDefault();

            }
            return customerDto;
        }
        public static CustomerSearchDTO ConvertToCustomerSearchDto(Customer customer)
        {
            CustomerSearchDTO customerSearchDTO = new CustomerSearchDTO();

            if (customerSearchDTO != null)
            {
                customerSearchDTO.CustomerId = customer.CustomerId;
                customerSearchDTO.CustomerCodeId = customer.CustomerCode;
                customerSearchDTO.CustomerName = customer.CustomerName;

            }

            return customerSearchDTO;
        }



        public static void ConvertToCustomerEntity(ref Customer customer, CustomerDto customerdto, bool isUpdate)
        {
           
            if (string.IsNullOrWhiteSpace(customerdto.CustomerName) == false)
            {
                customer.CustomerName = customerdto.CustomerName;
            }

            if(string.IsNullOrWhiteSpace(customerdto.AddressLine)==false)
            {
                customer.CustomerAddress = customerdto.AddressLine;
            }
            if (string.IsNullOrWhiteSpace(customerdto.FatherName) == false)
            {
                customer.FatherName = customerdto.FatherName;
            }
            if (string.IsNullOrWhiteSpace(customerdto.Village) == false)
            {
                customer.Village = customerdto.Village;
            }
            if (string.IsNullOrWhiteSpace(customerdto.Tehsil) == false)
            {
                customer.Tehsil = customerdto.Tehsil;
            }
            if (string.IsNullOrWhiteSpace(customerdto.District) == false)
            {
                customer.District = customerdto.District;

            }


            if (string.IsNullOrWhiteSpace(customerdto.Gender) == false)
            {
                customer.Gender = customerdto.Gender;

            }

            if (string.IsNullOrWhiteSpace(customerdto.AddressState) == false)
            {
                customer.AddressState = customerdto.AddressState;

            }
            if (string.IsNullOrWhiteSpace(customerdto.Contact) == false)
            {
                customer.Contact = customerdto.Contact;

            }
            if (string.IsNullOrWhiteSpace(customerdto.AlternateContact) == false)
            {
                customer.AlternateContact = customerdto.AlternateContact;

            }
            if (string.IsNullOrWhiteSpace(customerdto.PostalCode) == false)
            {
                customer.PostalCode = customerdto.PostalCode;

            }
            if (string.IsNullOrWhiteSpace(customerdto.Email) == false)
            {
                customer.Email = customerdto.Email;

            }
            if (string.IsNullOrWhiteSpace(customerdto.Password) == false)
            {
                customer.PIN = customerdto.Password;

            }
            if (string.IsNullOrWhiteSpace(customerdto.Occupation) == false)
            {
                customer.Occupation = customerdto.Occupation;
            }

            
            customer.DOB = customerdto.DOB.HasValue ? customerdto.DOB.Value: (DateTime ?)null;
            customer.Aniversary = customerdto.Aniversary.HasValue ? customerdto.Aniversary.Value : (DateTime?)null;


        }
    }
}
