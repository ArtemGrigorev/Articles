using FluentValidation;
using WebForWork.WebApi.Models;

namespace WebForWork.WebApi.Configurations
{
    public class GetArticleValidator : AbstractValidator<GetRequestModel>
    {
        public GetArticleValidator() 
        {
            RuleFor(x => x.Id).Must(x => Guid.TryParse(x, out var guid))
                 .WithMessage("Значение индентификатора не является Guid");
        }
    }
}
