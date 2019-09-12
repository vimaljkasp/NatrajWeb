using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    [Validator(typeof(DistributionCenterValidator))]
    public class DistributionCenterDTO
    {
        public DistributionCenterDTO()
        {
            DOB = DateTime.Now.Date;
            Anniversary = DateTime.Now.Date;
            DateOfRegistration = DateTime.Now.Date;            
        }
        public int DCId { get; set; }
        [DisplayName("Wallet Balance")]
        public decimal DcWalletBalance { get; set; }
        [DisplayName("DC Code")]
        public string DCCode { get; set; }
        [DisplayName("DC Name")]
        public string DCName { get; set; }
        [DisplayName("Agent Name")]
        public string AgentName { get; set; }
        [DisplayName("Father Name")]
        public string FatherName { get; set; }
        public string Contact { get; set; }
        [DisplayName("Alternate Contact")]
        public string AlternateContact { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Pin { get; set; }
        public string AADHAR { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Date of birth")]
        public DateTime DOB { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Anniversary { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Date Of Registration")]
        public DateTime DateOfRegistration { get; set; }
        [DisplayName("# Of Employee")]
        public int NoOfEmployee { get; set; }
        public string CreatedBy { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
      
        public string ModifiedBy { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ModifiedDate { get; set; }

        public bool IsActive { get; set; }
    
        public DCAddressDTO DCAddressDTO { get; set; }

    }

    public class DCWalletDTO
    {
        public int WalletId { get; set; }
        public int DCId { get; set; }
        public decimal WalletBalance { get; set; }
        public System.DateTime AmountDueDate { get; set; }
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
