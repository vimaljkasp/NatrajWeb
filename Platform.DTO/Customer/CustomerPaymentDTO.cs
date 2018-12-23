using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    [Validator(typeof(CustomerPaymentValidator))]
    public class CustomerPaymentDTO
    {
        public int CustomerPaymentId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ProductName { get; set; }
        public string OrderNumber { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public int OrderId { get; set; }
        public decimal PaymentCrAmount { get; set; }
        public decimal PaymentDrAmount { get; set; }
        public System.DateTime PaymentDate { get; set; }
        public string PaymentReceivedBy { get; set; }
        public string PaymentComments { get; set; }
        public PaymentMode PaymentMode { get; set; }
        public string Ref2 { get; set; }

    }

    public class CustomerPaymentValidator : AbstractValidator<CustomerPaymentDTO>
    {
        public CustomerPaymentValidator()
        {
            //   RuleFor(x => x.CustomerId).NotEmpty().WithMessage("The UserName cannot be blank.")
            //                               .Length(1, 10).WithMessage("The User Name cannot be more than 10 characters.");

      //      RuleFor(x => x.WalletBalance).NotEmpty().WithMessage("The Password cannot be blank.");

            //       RuleFor(x => x.BirthDate).LessThan(DateTime.Today).WithMessage("You cannot enter a birth date in the future.");

            //     RuleFor(x => x.Username).Length(8, 999).WithMessage("The user name must be at least 8 characters long.");
        }
    }

    public enum PaymentMode
    {
        Cash,
        Cheque,
        NEFT
    }
}
