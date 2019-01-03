using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    [Validator(typeof(CustomerValidator))]
    public class CustomerDto
    {
        public int CustomerId { get; set; }

        public int CustomerCodeId { get; set; }
        public string CustomerName { get; set; }
        public int VLCId { get; set; }
        public string FatherName { get; set; }
        public DateTime? DOB { get; set; }
        public string Gender { get; set; }
        public string AddressLine { get; set; }
        public string Village { get; set; }
        public string Tehsil { get; set; }
        public string District { get; set; }
        public string AddressState { get; set; }
        public string PostalCode { get; set; }
        public string Password { get; set; }
        public string Contact { get; set; }

        
        public string AlternateContact { get; set; }
        public string Email { get; set; }
        public DateTime? Aniversary { get; set; }
        public string Occupation { get; set; }
        public decimal CustomerBalance { get; set; }
      
        public DateTime? DateOfJoinVLC { get; set;}
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }
    }

    public class CustomerValidator : AbstractValidator<CustomerDto>
    {
        public CustomerValidator()
        {
       //     RuleFor(x => x.VLCId).NotEmpty().WithMessage("The VLCId cannot be blank.");
       //     RuleFor(x => x.CustomerName).NotNull().WithMessage("Customer Name Cannot be NULL");                       

            //      RuleFor(x => x.WalletBalance).NotEmpty().WithMessage("The Password cannot be blank.");

            //       RuleFor(x => x.BirthDate).LessThan(DateTime.Today).WithMessage("You cannot enter a birth date in the future.");

            //     RuleFor(x => x.Username).Length(8, 999).WithMessage("The user name must be at least 8 characters long.");
        }
    }
}
