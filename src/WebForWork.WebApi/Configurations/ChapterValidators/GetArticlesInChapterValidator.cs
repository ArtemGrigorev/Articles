using FluentValidation;
using WebForWork.WebApi.Models.Chapter;

namespace WebForWork.WebApi.Configurations.ChapterValidators
{
    public class GetArticlesInChapterValidator : AbstractValidator<GetRequestModel>
    {
        public GetArticlesInChapterValidator()
        {
            RuleFor(x => x.Id).Must(x => Guid.TryParse(x, out var guid))
                 .WithMessage("Значение индентификатора не является Guid");
        }
    }
}
