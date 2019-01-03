﻿using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{

    [Validator(typeof(DCPaymentValidator))]
    public class DCPaymentDTO
    {
        public int DCPaymentId { get; set; }
        public int DCId { get; set; }
        public int DCOrderId { get; set; }
        public decimal PaymentCrAmount { get; set; }
        public decimal PaymentDrAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentReceivedBy { get; set; }
        public string PaymentMode { get; set; }
        public string PaymentComments { get; set; }
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }


    public class DCPaymentValidator : AbstractValidator<DCPaymentDTO>
    {
        public DCPaymentValidator()
        {
            RuleFor(x => x.DCId).NotEqual(0).WithMessage("DC Id Is Required");

            //RuleFor(x => x.DCName).NotEmpty().MinimumLength(3).MaximumLength(100).WithMessage("The DC name is cannot be blank.");
            //RuleFor(x => x.AgentName).NotNull().WithMessage("Customer Name Cannot be NULL");
            //RuleFor(x => x.Email).EmailAddress().WithMessage("Given Email Is Not Valid.");
            //RuleFor(x => x.Password).NotNull().WithMessage("Password Cannnot be blank");
            ////  RuleFor(x=>x.Contact).
            //      RuleFor(x => x.WalletBalance).NotEmpty().WithMessage("The Password cannot be blank.");

            //       RuleFor(x => x.BirthDate).LessThan(DateTime.Today).WithMessage("You cannot enter a birth date in the future.");

            //     RuleFor(x => x.Username).Length(8, 999).WithMessage("The user name must be at least 8 characters long.");
        }
    }
}