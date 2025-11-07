using _365Architect.Demo.Domain.Abstractions.Repositories.Sql.Base;
using _365Architect.Demo.Domain.Entities;

namespace _365Architect.Demo.Domain.Abstractions.Repositories.Sql
{
    public interface ISampleTagSqlRepository : IGenericSqlRepository<SampleTag, int>
    {
    }
}
