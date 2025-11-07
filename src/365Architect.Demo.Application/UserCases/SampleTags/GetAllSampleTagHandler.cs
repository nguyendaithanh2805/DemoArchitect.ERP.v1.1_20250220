using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _365Architect.Demo.Application.Requests.SampleTags;
using _365Architect.Demo.Application.Requests.Tags;
using _365Architect.Demo.Contract.Shared;
using _365Architect.Demo.Domain.Abstractions.Repositories.Sql;
using _365Architect.Demo.Domain.Entities;
using MediatR;

namespace _365Architect.Demo.Application.UserCases.SampleTags
{
    public class GetAllSampleTagHandler : IRequestHandler<GetAllSampleTagQuery, Result<List<SampleTag>>>
    {
        private readonly ISampleTagSqlRepository _sampleTagSqlRepository;

        public GetAllSampleTagHandler(ISampleTagSqlRepository sampleTagSqlRepository)
        {
            _sampleTagSqlRepository = sampleTagSqlRepository;
        }

        public Task<Result<List<SampleTag>>> Handle(GetAllSampleTagQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult<Result<List<SampleTag>>>(_sampleTagSqlRepository.FindAll(null, false, st => st.Tag, st => st.Sample).ToList());
        }
    }
}
