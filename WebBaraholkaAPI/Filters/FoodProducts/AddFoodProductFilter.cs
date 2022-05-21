using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebBaraholkaAPI.Core.Enums;
using WebBaraholkaAPI.Core.Responses;
using WebBaraholkaAPI.Models.Dto.Requests.FoodProducts;

namespace WebBaraholkaAPI.Filters.FoodProducts;

public class AddFoodProductFilter : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        ValidationResult validationResult = 
            ((IValidator<AddFoodProductRequest>) context.HttpContext.RequestServices.GetService(typeof(IValidator<AddFoodProductRequest>)))
            .Validate((AddFoodProductRequest) context.ActionArguments["request"]);
        
        if(!validationResult.IsValid)
        {
            context.Result = new ObjectResult(new CommandResultResponse<string>()
            {
                Status = CommandResultStatus.Failed,
                Body = null,
                Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
            });

            return;
        }
        
        await next();
    }
}