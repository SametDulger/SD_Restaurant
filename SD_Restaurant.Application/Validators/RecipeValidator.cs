using FluentValidation;
using SD_Restaurant.Application.DTOs;

namespace SD_Restaurant.Application.Validators
{
    public class CreateRecipeDtoValidator : AbstractValidator<CreateRecipeDto>
    {
        public CreateRecipeDtoValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("Ürün seçimi zorunludur");

            RuleFor(x => x.IngredientId)
                .GreaterThan(0).WithMessage("Malzeme seçimi zorunludur");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Miktar 0'dan büyük olmalıdır");

            RuleFor(x => x.Unit)
                .NotEmpty().WithMessage("Birim zorunludur")
                .MaximumLength(20).WithMessage("Birim en fazla 20 karakter olabilir");

            RuleFor(x => x.Instructions)
                .MaximumLength(200).WithMessage("Talimatlar en fazla 200 karakter olabilir");
        }
    }

    public class UpdateRecipeDtoValidator : AbstractValidator<UpdateRecipeDto>
    {
        public UpdateRecipeDtoValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("Ürün seçimi zorunludur");

            RuleFor(x => x.IngredientId)
                .GreaterThan(0).WithMessage("Malzeme seçimi zorunludur");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Miktar 0'dan büyük olmalıdır");

            RuleFor(x => x.Unit)
                .NotEmpty().WithMessage("Birim zorunludur")
                .MaximumLength(20).WithMessage("Birim en fazla 20 karakter olabilir");

            RuleFor(x => x.Instructions)
                .MaximumLength(200).WithMessage("Talimatlar en fazla 200 karakter olabilir");
        }
    }
} 