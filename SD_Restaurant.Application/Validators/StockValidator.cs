using FluentValidation;
using SD_Restaurant.Application.DTOs;

namespace SD_Restaurant.Application.Validators
{
    public class CreateStockDtoValidator : AbstractValidator<CreateStockDto>
    {
        public CreateStockDtoValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("Ürün seçimi zorunludur");

            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage("Miktar 0'dan küçük olamaz");

            RuleFor(x => x.Unit)
                .NotEmpty().WithMessage("Birim zorunludur")
                .MaximumLength(20).WithMessage("Birim en fazla 20 karakter olabilir");

            RuleFor(x => x.MinimumStock)
                .GreaterThanOrEqualTo(0).WithMessage("Minimum stok 0'dan küçük olamaz");

            RuleFor(x => x.Location)
                .MaximumLength(100).WithMessage("Konum en fazla 100 karakter olabilir");

            RuleFor(x => x.Cost)
                .GreaterThanOrEqualTo(0).WithMessage("Maliyet 0'dan küçük olamaz");
        }
    }

    public class UpdateStockDtoValidator : AbstractValidator<UpdateStockDto>
    {
        public UpdateStockDtoValidator()
        {
            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage("Miktar 0'dan küçük olamaz");

            RuleFor(x => x.Unit)
                .NotEmpty().WithMessage("Birim zorunludur")
                .MaximumLength(20).WithMessage("Birim en fazla 20 karakter olabilir");

            RuleFor(x => x.MinimumStock)
                .GreaterThanOrEqualTo(0).WithMessage("Minimum stok 0'dan küçük olamaz");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Konum zorunludur")
                .MaximumLength(100).WithMessage("Konum en fazla 100 karakter olabilir");

            RuleFor(x => x.Cost)
                .GreaterThanOrEqualTo(0).WithMessage("Maliyet 0'dan küçük olamaz");
        }
    }
} 