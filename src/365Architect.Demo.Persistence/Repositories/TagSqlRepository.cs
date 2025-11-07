using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using _365Architect.Demo.Contract.Enumerations;
using _365Architect.Demo.Contract.Exceptions;
using _365Architect.Demo.Domain.Abstractions.Repositories.Sql;
using _365Architect.Demo.Domain.Constants;
using _365Architect.Demo.Domain.Entities;
using _365Architect.Demo.Persistence.Repositories.Base;

namespace _365Architect.Demo.Persistence.Repositories
{
    public class TagSqlRepository : GenericSqlRepository<Tag, int>, ITagSqlRepository
    {
        public TagSqlRepository(ApplicationDbContext context) : base(context)
        {
        }

        public void Add(Tag entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            base.Add(entity);
        }

        public void Update(Tag entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            base.Update(entity);
        }

        public async Task<Tag?> FindByIdAsync(int id, bool isTracking = false, CancellationToken cancellationToken = default, params Expression<Func<Tag, object>>[] includeProperties)
        {
            Tag? tag = await base.FindByIdAsync(id, isTracking, cancellationToken, includeProperties);
            if (tag is null)
                CustomException.ThrowNotFoundException(typeof(Tag), MsgCode.ERR_TAG_ID_NOT_FOUND, TagConst.MSG_TAG_ID_NOT_FOUND);

            return tag;
        }
    }
}
