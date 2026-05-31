using ECommerce.Shared.DTOs.BasketDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Shared.Validators
{
    public class BasketItemDTOValidator : AbstractValidator<BasketItemDTO>
    {
        public BasketItemDTOValidator()
        {
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(x => x.Quantity)
                .InclusiveBetween(1, 100).WithMessage("Quantity must be between 1 and 100.");
        }
    }
}
