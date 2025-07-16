using FluentValidation;
using SD_Restaurant.Application.DTOs;

namespace SD_Restaurant.Application.Validators
{
    public class CreatePaymentDtoValidator : AbstractValidator<CreatePaymentDto>
    {
        public CreatePaymentDtoValidator()
        {
            RuleFor(x => x.OrderId)
                .GreaterThan(0).WithMessage("Sipariş seçimi zorunludur");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Tutar 0'dan büyük olmalıdır");

            RuleFor(x => x.PaymentMethod)
                .NotEmpty().WithMessage("Ödeme yöntemi zorunludur")
                .MaximumLength(50).WithMessage("Ödeme yöntemi en fazla 50 karakter olabilir");

            RuleFor(x => x.Currency)
                .NotEmpty().WithMessage("Para birimi zorunludur")
                .MaximumLength(10).WithMessage("Para birimi en fazla 10 karakter olabilir");

            RuleFor(x => x.TransactionId)
                .NotEmpty().WithMessage("İşlem ID zorunludur")
                .MaximumLength(100).WithMessage("İşlem ID en fazla 100 karakter olabilir");

            RuleFor(x => x.Notes)
                .MaximumLength(500).WithMessage("Notlar en fazla 500 karakter olabilir");
        }
    }

    public class UpdatePaymentDtoValidator : AbstractValidator<UpdatePaymentDto>
    {
        public UpdatePaymentDtoValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Tutar 0'dan büyük olmalıdır");

            RuleFor(x => x.PaymentMethod)
                .NotEmpty().WithMessage("Ödeme yöntemi zorunludur")
                .MaximumLength(50).WithMessage("Ödeme yöntemi en fazla 50 karakter olabilir");

            RuleFor(x => x.Currency)
                .NotEmpty().WithMessage("Para birimi zorunludur")
                .MaximumLength(10).WithMessage("Para birimi en fazla 10 karakter olabilir");

            RuleFor(x => x.TransactionId)
                .NotEmpty().WithMessage("İşlem ID zorunludur")
                .MaximumLength(100).WithMessage("İşlem ID en fazla 100 karakter olabilir");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Durum zorunludur")
                .MaximumLength(20).WithMessage("Durum en fazla 20 karakter olabilir");

            RuleFor(x => x.Notes)
                .MaximumLength(500).WithMessage("Notlar en fazla 500 karakter olabilir");
        }
    }
} 