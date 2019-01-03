﻿using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    [Validator(typeof(DCOrderValidator))]
    public class DCOrderDTO
    {

        public int DCOrderId { get; set; }
        public string DCOrderNumber { get; set; }
        public System.DateTime OrderDate { get; set; }
        public string BillNumber { get; set; }
        public int DCId { get; set; }
        public int OrderAddressId { get; set; }
        public decimal OrderPrice { get; set; }
        public decimal SGST { get; set; }
        public decimal CGST { get; set; }
        public decimal IGST { get; set; }
        public decimal OrderTax { get; set; }
        public decimal OrderDiscount { get; set; }
        public decimal OrderTotalPrice { get; set; }
        public decimal OrderPaidAmount { get; set; }
        public decimal TotalOrderQuantity { get; set; }
        public decimal TotalActualQuantity { get; set; }
        public int OrderStatusId { get; set; }
        public DateTime DeliveryExpectedDate { get; set; }
        public DateTime DeliveredDate { get; set; }
        public string DeliveredBy { get; set; }
        public string OrderComments { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        List<DCOrderDtlDTO> dcOrderDtlList { get; set; }
    }

    public class DCOrderValidator : AbstractValidator<DCOrderDTO>
    {
        public DCOrderValidator()
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


    public class DCOrderDtlDTO
    {
        public int DCOrderDtlId { get; set; }
        public int DCOrderId { get; set; }
        public int ProductId { get; set; }
        public decimal QuantityOrdered { get; set; }
        public decimal ActualQuantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal OrderTotal { get; set; }
        public decimal OrderTax { get; set; }
        public decimal OrderTotalPrice { get; set; }
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
    }
}