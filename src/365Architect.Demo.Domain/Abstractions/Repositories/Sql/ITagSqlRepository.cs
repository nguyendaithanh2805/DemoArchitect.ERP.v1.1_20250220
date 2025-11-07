using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _365Architect.Demo.Domain.Abstractions.Repositories.Sql.Base;
using _365Architect.Demo.Domain.Entities;

namespace _365Architect.Demo.Domain.Abstractions.Repositories.Sql
{
    public interface ITagSqlRepository : IGenericSqlRepository<Tag, int>
    {
    }
}
