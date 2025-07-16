using FluentValidation;
using SD_Restaurant.Application.DTOs;

namespace SD_Restaurant.Application.Validators
{
    public class CreateCustomerDtoValidator : AbstractValidator<CreateCustomerDto>
    {
        public CreateCustomerDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ad zorunludur")
                .MaximumLength(50).WithMessage("Ad en fazla 50 karakter olabilir");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad zorunludur")
                .MaximumLength(50).WithMessage("Soyad en fazla 50 karakter olabilir");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz")
                .MaximumLength(100).WithMessage("E-posta en fazla 100 karakter olabilir");

            RuleFor(x => x.Phone)
                .MaximumLength(20).WithMessage("Telefon en fazla 20 karakter olabilir");

            RuleFor(x => x.Address)
                .MaximumLength(200).WithMessage("Adres en fazla 200 karakter olabilir");
        }
    }

    public class UpdateCustomerDtoValidator : AbstractValidator<UpdateCustomerDto>
    {
        public UpdateCustomerDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ad zorunludur")
                .MaximumLength(50).WithMessage("Ad en fazla 50 karakter olabilir");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad zorunludur")
                .MaximumLength(50).WithMessage("Soyad en fazla 50 karakter olabilir");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz")
                .MaximumLength(100).WithMessage("E-posta en fazla 100 karakter olabilir");

            RuleFor(x => x.Phone)
                .MaximumLength(20).WithMessage("Telefon en fazla 20 karakter olabilir");

            RuleFor(x => x.Address)
                .MaximumLength(200).WithMessage("Adres en fazla 200 karakter olabilir");
        }
    }
} 