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
    [Validator(typeof(DCOrderValidator))]
    public class DCOrderDTO
    {
        

        public DCOrderDTO()
        {

            OrderDate = DateTime.Now.Date;
            DeliveredDate = DateTime.Now.Date;
            DeliveryExpectedDate = DateTime.Now.Date;           

            dcOrderDtlList = new List<DCOrderDtlDTO>() { new DCOrderDtlDTO {
                ProductDescription = "",
                ProductId = 0,
                ProductImageUrl = "",
                TotalPrice = 0,
                QuantityOrdered = 0,
                ProductName = "",
                UnitPrice = 0 } };
            dCAddressDTO = new DCAddressDTO();
        }

        public int DCOrderId { get; set; }
        [DisplayName("Order #")]
        public string DCOrderNumber { get; set; }
        [DataType(DataType.Date)]        
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Date")]
        public DateTime? OrderDate { get; set; }
        [DisplayName("DCID")]
        public int DCId { get; set; }
        //public decimal OrderPrice { get; set; }

        //public decimal OrderTax { get; set; }
        [DisplayName("Discount")]
        public decimal OrderDiscount { get; set; }
        [DisplayName("Total Price")]
        public decimal OrderTotalPrice { get; set; }
        [DisplayName("DC Name")]
        public String DCName { get; set; }
        [DisplayName("Paid Amount")]
        public decimal OrderPaidAmount { get; set; }
        [DisplayName("Total Qty")]
        public decimal TotalOrderQuantity { get; set; }
        [DisplayName("Actual Qty")]
        public decimal TotalActualQuantity { get; set; }
        [DisplayName("Status")]
        public OrderStatus OrderStatus { get; set; }

        [DisplayName("Expected Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DeliveryExpectedDate { get; set; }
        [DisplayName("Delivery Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DeliveredDate { get; set; }
        [DisplayName("Delivered By")]
        public string DeliveredBy { get; set; }
        [DisplayName("Comments")]
        public string OrderComments { get; set; }


        public List<DCOrderDtlDTO> dcOrderDtlList { get; set; }

        public DCAddressDTO dCAddressDTO { get; set; }
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
        public decimal UnitPrice { get; set; }

        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImageUrl { get; set; }
        public decimal QuantityOrdered { get; set; }
        public decimal ActualQuantity { get; set; }
        //public decimal ActualQuantity { get; set; }
        //public decimal Price { get; set; }
        //public decimal OrderTax { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
