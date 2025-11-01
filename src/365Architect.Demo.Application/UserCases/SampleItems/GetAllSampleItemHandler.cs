using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _365Architect.Demo.Application.Requests.SampleItems;
using _365Architect.Demo.Application.Requests.Samples;
using _365Architect.Demo.Contract.DependencyInjection.Extensions;
using _365Architect.Demo.Contract.Shared;
using _365Architect.Demo.Domain.Abstractions.Repositories.Sql;
using _365Architect.Demo.Domain.Entities;
using MediatR;

namespace _365Architect.Demo.Application.UserCases.SampleItems
{
    public class GetAllSampleItemHandler : IRequestHandler<GetAllSampleItemQuery, Result<List<SampleItem>>>
    {
        private readonly ISampleSqlRepository _sampleSqlRepository;

        public GetAllSampleItemHandler(ISampleSqlRepository sampleSqlRepository)
        {
            _sampleSqlRepository = sampleSqlRepository;
        }

        public async Task<Result<List<SampleItem>>> Handle(GetAllSampleItemQuery request, CancellationToken cancellationToken)
        {
            var sample = await _sampleSqlRepository.FindByIdAsync((int)request.SampleId, true, cancellationToken, s => s.Items);

            return sample.Items.ToList();
        }
    }
}
