﻿using FluentValidation;
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
    [Validator(typeof(VLCValidator))]
    public class VLCDTO
    {
        public VLCDTO()
        {
            VLCEnrollmentDate = DateTime.Now.Date;
        }
        public int VLCId { get; set; }
        [DisplayName("Code")]
        public string VLCCode { get; set; }    
        [DisplayName("Name")]
        public string VLCName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Enrollment Date")]
        public DateTime? VLCEnrollmentDate { get; set; }
        [DisplayName("Agent Name")]
        public string AgentName { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        [DisplayName("Address")]
        public string VLCAddress { get; set; }
        public string Village { get; set; }
        public string City { get; set; }
        [DisplayName("State")]
        public string VLCState { get; set; }
        public string Password { get; set; }
        [DisplayName("Alternate Contact")]
        public string AlternateContact { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        [DisplayName("Agent AADHAR")]
        public string VLCAgentAadhaar { get; set; }

        public int Shift { get; set; }
        [DisplayName("Applicable Rate")]
        public int ApplicableRate { get; set; }

        public Decimal? CLR { get; set; }

        public Decimal? FAT { get; set; }
        [DisplayName("Different CLR")]
        public Decimal? DifferenceCLR { get; set; }

        [DisplayName("Machine Rent")]
        public decimal MachineRent { get; set; }
        [DisplayName("House Rent")]
        public decimal HouseRent { get; set; }
        [DisplayName("Milk Commission")]
        public decimal MilkCommission { get; set; }
    }
    public class VLCValidator : AbstractValidator<VLCDTO>
    {
        public VLCValidator()
        {
         //   RuleFor(x => x.VLCName).NotEmpty().MinimumLength(3).MaximumLength(100).WithMessage("The DC name is cannot be blank.");
         //   RuleFor(x => x.AgentName).NotNull().WithMessage("Customer Name Cannot be NULL");
         //   RuleFor(x => x.Email).EmailAddress().WithMessage("Given Email Is Not Valid.");
         //   RuleFor(x => x.Password).NotNull().WithMessage("Password Cannnot be blank");
            //  RuleFor(x=>x.Contact).
            //      RuleFor(x => x.WalletBalance).NotEmpty().WithMessage("The Password cannot be blank.");

            //       RuleFor(x => x.BirthDate).LessThan(DateTime.Today).WithMessage("You cannot enter a birth date in the future.");

            //     RuleFor(x => x.Username).Length(8, 999).WithMessage("The user name must be at least 8 characters long.");
        }
    }
}
