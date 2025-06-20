using FluentValidation;
using WebForWork.Domain.Models.Aggregates;
using WebForWork.WebApi.Models;

namespace WebForWork.WebApi.Configurations
{
    public class CreateArticleValidator : AbstractValidator<CreateRequestModel>
    {
        public CreateArticleValidator() 
        {
            RuleFor(x => x.Name).Length(1, Article.invariantLengthMaxName)
                    .WithMessage($"Длина имени превышает допустимое значение ValueMax = {Article.invariantLengthMaxName}");

            RuleFor(x => x.Tags)
                .Must(x => x.Count() <= Article.invariantCountMaxTags)
                .WithMessage($"Количество тегов превышает допустимое значение ValueMax = {Article.invariantCountMaxTags}");
        }
    }
}
