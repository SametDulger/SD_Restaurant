using FluentValidation;
using SD_Restaurant.Application.DTOs;

namespace SD_Restaurant.Application.Validators
{
    public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Kategori adı zorunludur")
                .MaximumLength(50).WithMessage("Kategori adı en fazla 50 karakter olabilir");

            RuleFor(x => x.Description)
                .MaximumLength(200).WithMessage("Açıklama en fazla 200 karakter olabilir");
        }
    }

    public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Kategori adı zorunludur")
                .MaximumLength(50).WithMessage("Kategori adı en fazla 50 karakter olabilir");

            RuleFor(x => x.Description)
                .MaximumLength(200).WithMessage("Açıklama en fazla 200 karakter olabilir");
        }
    }
} 