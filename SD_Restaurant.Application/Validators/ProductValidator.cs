using FluentValidation;
using SD_Restaurant.Application.DTOs;

namespace SD_Restaurant.Application.Validators
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ürün adı zorunludur")
                .MaximumLength(100).WithMessage("Ürün adı en fazla 100 karakter olabilir");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Fiyat 0'dan büyük olmalıdır");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Kategori seçimi zorunludur");

            RuleFor(x => x.Unit)
                .NotEmpty().WithMessage("Birim zorunludur")
                .MaximumLength(20).WithMessage("Birim en fazla 20 karakter olabilir");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir");
        }
    }

    public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Geçerli bir ID gerekir");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ürün adı zorunludur")
                .MaximumLength(100).WithMessage("Ürün adı en fazla 100 karakter olabilir");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Fiyat 0'dan büyük olmalıdır");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Kategori seçimi zorunludur");

            RuleFor(x => x.Unit)
                .NotEmpty().WithMessage("Birim zorunludur")
                .MaximumLength(20).WithMessage("Birim en fazla 20 karakter olabilir");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir");
        }
    }
} 