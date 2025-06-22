using FluentValidation;
using WebForWork.WebApi.Models.Article;
namespace WebForWork.WebApi.Configurations.ArticleValidator
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
