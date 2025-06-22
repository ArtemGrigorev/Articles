using WebForWork.Domain.Models.Aggregates;

namespace WebForWork.Domain.Models
{
    public sealed class ChaptersCatalogDTO
    {
        public Chapter Chapter { get; init; }
        public int Count { get; init; }
    }
}
