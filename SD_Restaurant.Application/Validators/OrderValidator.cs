using FluentValidation;
using SD_Restaurant.Application.DTOs;

namespace SD_Restaurant.Application.Validators
{
    public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderDtoValidator()
        {
            RuleFor(x => x.TableId)
                .GreaterThan(0).WithMessage("Masa seçimi zorunludur");

            RuleFor(x => x.CustomerId)
                .GreaterThan(0).WithMessage("Müşteri seçimi zorunludur");

            RuleFor(x => x.EmployeeId)
                .GreaterThan(0).WithMessage("Personel seçimi zorunludur");

            RuleFor(x => x.OrderItems)
                .NotEmpty().WithMessage("Sipariş kalemleri zorunludur");

            RuleForEach(x => x.OrderItems).SetValidator(new CreateOrderItemDtoValidator());
        }
    }

    public class CreateOrderItemDtoValidator : AbstractValidator<CreateOrderItemDto>
    {
        public CreateOrderItemDtoValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("Ürün seçimi zorunludur");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Miktar 0'dan büyük olmalıdır");

            RuleFor(x => x.DiscountAmount)
                .GreaterThanOrEqualTo(0).WithMessage("İndirim tutarı 0'dan küçük olamaz");
        }
    }
} 