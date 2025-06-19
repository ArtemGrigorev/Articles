using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebForWork.WebApi.Configurations
{
    public static class Extensions
    {
        public static void AddToModelState(this ValidationResult result, ModelStateDictionary modelState)
        {
            if (result.IsValid)
                return;

            foreach (var error in result.Errors)
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
        }
    }
}
