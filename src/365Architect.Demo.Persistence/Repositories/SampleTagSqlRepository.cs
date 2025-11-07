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
    public class SampleTagSqlRepository : GenericSqlRepository<SampleTag, int>, ISampleTagSqlRepository
    {
        public SampleTagSqlRepository(ApplicationDbContext context) : base(context)
        {
        }
        public void Add(SampleTag entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            base.Add(entity);
        }

        public void Update(SampleTag entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            base.Update(entity);
        }

        public async Task<SampleTag?> FindByIdAsync(int id, bool isTracking = false, CancellationToken cancellationToken = default, params Expression<Func<SampleTag, object>>[] includeProperties)
        {
            SampleTag? sampleTag = await base.FindByIdAsync(id, isTracking, cancellationToken, includeProperties);
            if (sampleTag is null)
                CustomException.ThrowNotFoundException(typeof(SampleTag), MsgCode.ERR_SAMPLE_TAG_ID_NOT_FOUND, SampleTagConst.MSG_SAMPLE_TAG_ID_NOT_FOUND);

            return sampleTag;
        }
    }
}
