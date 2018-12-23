using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    [Validator(typeof(SiteConfigurationValidator))]
    public class SiteConfigurationDTO
    {
        public int Id { get; set; }
        public string KeyData { get; set; }
        public string KeyName { get; set; }
        public string DataVal { get; set; }
        public string DefaultVal { get; set; }
        public string Description { get; set; }
    }

    public class SiteConfigurationValidator : AbstractValidator<SiteConfigurationDTO>
    {
        public SiteConfigurationValidator()
        {
            //   RuleFor(x => x.CustomerId).NotEmpty().WithMessage("The UserName cannot be blank.")
            //                               .Length(1, 10).WithMessage("The User Name cannot be more than 10 characters.");

            //      RuleFor(x => x.WalletBalance).NotEmpty().WithMessage("The Password cannot be blank.");

            //       RuleFor(x => x.BirthDate).LessThan(DateTime.Today).WithMessage("You cannot enter a birth date in the future.");

            //     RuleFor(x => x.Username).Length(8, 999).WithMessage("The user name must be at least 8 characters long.");
        }
    }
}
