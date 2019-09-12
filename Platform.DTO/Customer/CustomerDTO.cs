using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public string CustomerName { get; set; }
        public int VLCId { get; set; }
        public string VLCName { get; set; }
        public string FatherName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DOB { get; set; }
        public GenderEnum Gender { get; set; }        
        public string AddressLine { get; set; }
        public string Village { get; set; }
        public string Tehsil { get; set; }
        public string District { get; set; }
        public string AddressState { get; set; }
        [MaxLength(6)]
        public string PostalCode { get; set; }
        public string Password { get; set; }
        [MaxLength(12)]
        public string Contact { get; set; }

        public int ApplicableRate { get; set; }
        [MaxLength(12)]
        public string AlternateContact { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Aniversary { get; set; }
        public string Occupation { get; set; }
        public decimal CustomerBalance { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfJoinVLC { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public string ServiceResponse
        {
            get;
            set;
        }
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
