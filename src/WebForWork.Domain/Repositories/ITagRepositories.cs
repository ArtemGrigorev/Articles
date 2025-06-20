using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebForWork.Domain.Models.Aggregates;
using WebForWork.Domain.Models.Entities;

namespace WebForWork.Domain.Repositories
{
    public interface ITagRepositories
    {
        Task<List<Tag>> GetTagsByNamesAsync(List<string> names, CancellationToken cancellationToken);
    }
}
