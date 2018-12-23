using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    [Validator(typeof(UserValidator))]
   
    public class UserDTO
    {

        public int UserId { get; set; }
        public int? RoleId { get; set; }
        public string Name { get; set; }
        public string Permission { get; set; }
        public string SecurityCode { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public DateTime? DateOfCreation { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    public class UserValidator : AbstractValidator<UserDTO>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("The UserName cannot be blank.")
                                        .Length(1, 10).WithMessage("The User Name cannot be more than 10 characters.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("The Password cannot be blank.");

            //       RuleFor(x => x.BirthDate).LessThan(DateTime.Today).WithMessage("You cannot enter a birth date in the future.");

            //     RuleFor(x => x.Username).Length(8, 999).WithMessage("The user name must be at least 8 characters long.");
        }
    }
}
