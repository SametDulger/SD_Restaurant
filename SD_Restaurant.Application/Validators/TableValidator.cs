using FluentValidation;
using SD_Restaurant.Application.DTOs;

namespace SD_Restaurant.Application.Validators
{
    public class CreateTableDtoValidator : AbstractValidator<CreateTableDto>
    {
        public CreateTableDtoValidator()
        {
            RuleFor(x => x.TableNumber)
                .NotEmpty().WithMessage("Masa numarası zorunludur")
                .MaximumLength(20).WithMessage("Masa numarası en fazla 20 karakter olabilir");

            RuleFor(x => x.Capacity)
                .InclusiveBetween(1, 20).WithMessage("Kapasite 1-20 arasında olmalıdır");

            RuleFor(x => x.Location)
                .MaximumLength(100).WithMessage("Konum en fazla 100 karakter olabilir");
        }
    }

    public class UpdateTableDtoValidator : AbstractValidator<UpdateTableDto>
    {
        public UpdateTableDtoValidator()
        {
            RuleFor(x => x.TableNumber)
                .NotEmpty().WithMessage("Masa numarası zorunludur")
                .MaximumLength(20).WithMessage("Masa numarası en fazla 20 karakter olabilir");

            RuleFor(x => x.Capacity)
                .InclusiveBetween(1, 20).WithMessage("Kapasite 1-20 arasında olmalıdır");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Durum zorunludur")
                .MaximumLength(20).WithMessage("Durum en fazla 20 karakter olabilir");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Konum zorunludur")
                .MaximumLength(100).WithMessage("Konum en fazla 100 karakter olabilir");
        }
    }
} 