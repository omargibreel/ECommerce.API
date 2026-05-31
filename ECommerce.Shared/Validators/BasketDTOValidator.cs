using ECommerce.Shared.DTOs.BasketDTOs;
using FluentValidation;

namespace ECommerce.Shared.Validators
{
    public class BasketDTOValidator : AbstractValidator<BasketDTO>
    {
        public BasketDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Basket Id is required.");

            RuleForEach(x => x.Items).SetValidator(new BasketItemDTOValidator());
        }
    }
}