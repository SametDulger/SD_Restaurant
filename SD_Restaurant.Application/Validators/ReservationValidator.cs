using FluentValidation;
using SD_Restaurant.Application.DTOs;

namespace SD_Restaurant.Application.Validators
{
    public class CreateReservationDtoValidator : AbstractValidator<CreateReservationDto>
    {
        public CreateReservationDtoValidator()
        {
            RuleFor(x => x.TableId)
                .GreaterThan(0).WithMessage("Masa seçimi zorunludur");

            RuleFor(x => x.CustomerName)
                .NotEmpty().WithMessage("Müşteri adı zorunludur")
                .MaximumLength(50).WithMessage("Müşteri adı en fazla 50 karakter olabilir");

            RuleFor(x => x.CustomerPhone)
                .MaximumLength(20).WithMessage("Telefon en fazla 20 karakter olabilir");

            RuleFor(x => x.ReservationDate)
                .GreaterThan(DateTime.Today).WithMessage("Rezervasyon tarihi bugünden sonra olmalıdır");

            RuleFor(x => x.GuestCount)
                .InclusiveBetween(1, 20).WithMessage("Misafir sayısı 1-20 arasında olmalıdır");

            RuleFor(x => x.SpecialRequests)
                .MaximumLength(500).WithMessage("Özel istekler en fazla 500 karakter olabilir");
        }
    }

    public class UpdateReservationDtoValidator : AbstractValidator<UpdateReservationDto>
    {
        public UpdateReservationDtoValidator()
        {
            RuleFor(x => x.CustomerName)
                .NotEmpty().WithMessage("Müşteri adı zorunludur")
                .MaximumLength(50).WithMessage("Müşteri adı en fazla 50 karakter olabilir");

            RuleFor(x => x.CustomerPhone)
                .MaximumLength(20).WithMessage("Telefon en fazla 20 karakter olabilir");

            RuleFor(x => x.ReservationDate)
                .GreaterThan(DateTime.Today).WithMessage("Rezervasyon tarihi bugünden sonra olmalıdır");

            RuleFor(x => x.GuestCount)
                .InclusiveBetween(1, 20).WithMessage("Misafir sayısı 1-20 arasında olmalıdır");

            RuleFor(x => x.SpecialRequests)
                .MaximumLength(500).WithMessage("Özel istekler en fazla 500 karakter olabilir");
        }
    }
} 