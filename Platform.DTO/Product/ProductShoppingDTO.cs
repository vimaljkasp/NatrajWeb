using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    [Validator(typeof(ProductValidator))]
    public class ProductShoppingDTO
    {
        public Int32 ProductId { get; set; }
        public int CategoryId { get; set; }
        public String CategoryName { get; set; }
        public string ProductName { get; set; }
        public string ProductQuantityDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal DiscountedProductPrice { get; set; }

        public String ImageUrl { get; set; }
    }

    public class ProductShoppingValidator : AbstractValidator<ProductDTO>
    {
        public ProductShoppingValidator()
        {
            //   RuleFor(x => x.CustomerId).NotEmpty().WithMessage("The UserName cannot be blank.")
            //                               .Length(1, 10).WithMessage("The User Name cannot be more than 10 characters.");

            //      RuleFor(x => x.WalletBalance).NotEmpty().WithMessage("The Password cannot be blank.");

            //       RuleFor(x => x.BirthDate).LessThan(DateTime.Today).WithMessage("You cannot enter a birth date in the future.");

            //     RuleFor(x => x.Username).Length(8, 999).WithMessage("The user name must be at least 8 characters long.");
        }
    }
}
