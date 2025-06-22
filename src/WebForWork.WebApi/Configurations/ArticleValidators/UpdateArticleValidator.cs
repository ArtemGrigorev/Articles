using FluentValidation;
using WebForWork.Domain.Models.Aggregates;
using WebForWork.WebApi.Models.Article;

namespace WebForWork.WebApi.Configurations.ArticleValidator
{
    public class UpdateArticleValidator : AbstractValidator<UpdateRequestModel>
    {
        public UpdateArticleValidator()
        {
            RuleFor(x => x.Id).Must(x => Guid.TryParse(x, out var guid))
                   .WithMessage("Значение индентификатора не является Guid");
            RuleFor(x => x.Tags).Must(x => x.Distinct().Count() == x.Count())
                    .WithMessage("Значения тегов в статье не уникально");
            RuleFor(x => x.Name).Length(1, Article.invariantLengthMaxName)
                    .WithMessage($"Длина имени превышает допустимое значение ValueMax = {Article.invariantLengthMaxName}");
            RuleFor(x => x.Tags)
                .Must(x => x.Count() <= Article.invariantCountMaxTags)
                .WithMessage($"Количество тегов превышает допустимое значение ValueMax = {Article.invariantCountMaxTags}");
        }
    }
}
