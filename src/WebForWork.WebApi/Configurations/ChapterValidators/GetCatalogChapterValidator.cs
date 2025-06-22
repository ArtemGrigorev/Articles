using FluentValidation;
using WebForWork.WebApi.Models.Chapter;

namespace WebForWork.WebApi.Configurations.ChapterValidators
{
    public class GetCatalogChapterValidator : AbstractValidator<GetCatalogRequestModel>
    {
        public GetCatalogChapterValidator()
        {
            RuleFor(x => x.Page).Must(x => int.TryParse(x, out var d))
                 .WithMessage("Параметр страницы не является числом");
        }
    }
}
