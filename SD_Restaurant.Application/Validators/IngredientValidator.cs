using FluentValidation;
using SD_Restaurant.Application.DTOs;

namespace SD_Restaurant.Application.Validators
{
    public class CreateIngredientDtoValidator : AbstractValidator<CreateIngredientDto>
    {
        public CreateIngredientDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Malzeme adı zorunludur")
                .MaximumLength(100).WithMessage("Malzeme adı en fazla 100 karakter olabilir");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir");

            RuleFor(x => x.Unit)
                .NotEmpty().WithMessage("Birim zorunludur")
                .MaximumLength(20).WithMessage("Birim en fazla 20 karakter olabilir");

            RuleFor(x => x.Cost)
                .GreaterThanOrEqualTo(0).WithMessage("Maliyet negatif olamaz");
        }
    }

    public class UpdateIngredientDtoValidator : AbstractValidator<UpdateIngredientDto>
    {
        public UpdateIngredientDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Geçerli bir ID gerekli");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Malzeme adı zorunludur")
                .MaximumLength(100).WithMessage("Malzeme adı en fazla 100 karakter olabilir");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir");

            RuleFor(x => x.Unit)
                .NotEmpty().WithMessage("Birim zorunludur")
                .MaximumLength(20).WithMessage("Birim en fazla 20 karakter olabilir");

            RuleFor(x => x.Cost)
                .GreaterThanOrEqualTo(0).WithMessage("Maliyet negatif olamaz");
        }
    }
} 