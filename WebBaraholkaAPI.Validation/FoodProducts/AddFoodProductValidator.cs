using FluentValidation;
using WebBaraholkaAPI.Models.Dto.Requests.FoodProducts;

namespace WebBaraholkaAPI.Validation.FoodProducts;

public class AddFoodProductValidator : AbstractValidator<AddFoodProductRequest>
{
    public AddFoodProductValidator()
    {
        RuleFor(r => r.FoodProducts)
            .NotEmpty().WithMessage("FoodProducts Information cannot be empty");

        RuleForEach(r => r.FoodProducts)
            .ChildRules(fps =>
                {
                    fps.RuleFor(fp => fp.Name)
                        .NotEmpty().WithMessage("Name required field")
                        .MaximumLength(256).WithMessage("Name is too long");
                    
                    fps.RuleFor(r => r.Proteins)
                        .NotEmpty().WithMessage("Proteins is required field")
                        .GreaterThanOrEqualTo(0).WithMessage("Proteins cannot be less then 0");

                    fps.RuleFor(r => r.Fats)
                        .NotEmpty().WithMessage("Fats is required field")
                        .GreaterThanOrEqualTo(0).WithMessage("Fats cannot be less then 0");
        
                    fps.RuleFor(r => r.Carbohydrates)
                        .NotEmpty().WithMessage("Carbohydrates is required field")
                        .GreaterThanOrEqualTo(0).WithMessage("Carbohydrates cannot be less then 0");
        
                    fps.RuleFor(r => r.EnergyValue)
                        .NotEmpty().WithMessage("EnergyValue is required field")
                        .GreaterThanOrEqualTo(0).WithMessage("EnergyValue cannot be less then 0");
                }
            );
    }
}