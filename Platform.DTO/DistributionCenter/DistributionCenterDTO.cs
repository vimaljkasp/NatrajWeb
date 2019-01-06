using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    [Validator(typeof(DistributionCenterValidator))]
    public class DistributionCenterDTO
    {
        public int DCId { get; set; }
        public string DCCode { get; set; }
        public string DCName { get; set; }
        public string AgentName { get; set; }
        public string FatherName { get; set; }
        public string Contact { get; set; }
        public string AlternateContact { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Pin { get; set; }
        public string AADHAR { get; set; }
        public DateTime DOB { get; set; }
        public DateTime Anniversary { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public int NoOfEmployee { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    
        public DCAddressDTO DCAddressDTO { get; set; }

    }


    public class DistributionCenterValidator : AbstractValidator<DistributionCenterDTO>
    {
        public DistributionCenterValidator()
        {
            RuleFor(x => x.DCName).NotEmpty().MinimumLength(3).MaximumLength(100).WithMessage("The DC name is cannot be blank.");
            RuleFor(x => x.AgentName).NotNull().WithMessage("Customer Name Cannot be NULL");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Given Email Is Not Valid.");
        //    RuleFor(x => x.Password).NotNull().WithMessage("Password Cannnot be blank");
          //  RuleFor(x=>x.Contact).
            //      RuleFor(x => x.WalletBalance).NotEmpty().WithMessage("The Password cannot be blank.");

            //       RuleFor(x => x.BirthDate).LessThan(DateTime.Today).WithMessage("You cannot enter a birth date in the future.");

            //     RuleFor(x => x.Username).Length(8, 999).WithMessage("The user name must be at least 8 characters long.");
        }
    }
}
