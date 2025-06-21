using FluentValidation;
using WebForWork.Domain.Models.Aggregates;
using WebForWork.WebApi.Models;

namespace WebForWork.WebApi.Configurations
{
    public class UpdateArticleValidator : AbstractValidator<UpdateRequestModel>
    {
        public UpdateArticleValidator()
        {
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
