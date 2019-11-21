using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    [Validator(typeof(CreateDCOrderDTOValidator))]
    public class CreateDCOrderDTO
    {
        public int DCId { get; set; }

        public int DCOrderId { get; set; }
        
        public string OrderComments { get; set; }

        public List<CreateDCOrderDtlDTO> CreateDCOrderDtlList { get; set; }
    }


    public class CreateDCOrderDtlDTO
    {
        public int DCOrderDtlId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }

        public decimal QuantityOrdered { get; set; }
        public decimal ActualQuantity { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class CreateDCOrderDTOValidator : AbstractValidator<CreateDCOrderDTO>
    {
        public CreateDCOrderDTOValidator()
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
