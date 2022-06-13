using FluentValidation;
using WebBaraholkaAPI.Models.Dto.Requests.FoodProducts;

namespace WebBaraholkaAPI.Validation.FoodProducts;

public class AddNewConsumedFoodRecordValidator : AbstractValidator<AddNewConsumedFoodRecordRequest>
{
    public AddNewConsumedFoodRecordValidator()
    {
        RuleFor(r => r.ConsumedFoodProducts)
            .NotEmpty().WithMessage("FoodProducts Information cannot be empty");

        RuleForEach(r => r.ConsumedFoodProducts)
            .ChildRules(fps =>
                {
                    fps.RuleFor(fp => fp.FoodName)
                        .NotEmpty().WithMessage("Food name required field")
                        .MaximumLength(256).WithMessage("Name is too long");
                    
                    fps.RuleFor(r => r.ConsumedMass)
                        .NotEmpty().WithMessage("Proteins is required field")
                        .GreaterThanOrEqualTo(0).WithMessage("Proteins cannot be less then 0");
                }
            );
    }
}