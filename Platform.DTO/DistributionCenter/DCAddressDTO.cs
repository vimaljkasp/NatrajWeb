using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    [Validator(typeof(DCAddressValidator))]
    public  class DCAddressDTO
    {
        public int DCAddressId { get; set; }
        public int DCId { get; set; }
        //public string AddressTypeId { get; set; }
        //public bool IsDefaultAddress { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Contact { get; set; }
        //public bool IsDeleted { get; set; }
    }

    public class DCAddressValidator : AbstractValidator<DCAddressDTO>
    {
        public DCAddressValidator()
        {
         ////   RuleFor(x => x.DCId).NotEqual(0).WithMessage("DC Id Is Required");
         //   RuleFor(x => x.AddressTypeId).NotNull().WithMessage("Address Type Is Required");
         //   RuleFor(x => x.PostalCode).NotNull().WithMessage("Postal Code is Required");
         //   RuleFor(x => x.Contact).NotNull().WithMessage("Contact Number Cannnot be blank");
            //  RuleFor(x=>x.Contact).
            //      RuleFor(x => x.WalletBalance).NotEmpty().WithMessage("The Password cannot be blank.");

            //       RuleFor(x => x.BirthDate).LessThan(DateTime.Today).WithMessage("You cannot enter a birth date in the future.");

            //     RuleFor(x => x.Username).Length(8, 999).WithMessage("The user name must be at least 8 characters long.");
        }
    }
}
