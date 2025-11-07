using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _365Architect.Demo.Application.Requests.SampleTags;
using _365Architect.Demo.Application.Requests.Tags;
using _365Architect.Demo.Application.Validators.SampleTags;
using _365Architect.Demo.Application.Validators.Tags;
using _365Architect.Demo.Contract.Shared;
using _365Architect.Demo.Domain.Abstractions.Repositories.Sql;
using _365Architect.Demo.Domain.Entities;
using MediatR;

namespace _365Architect.Demo.Application.UserCases.SampleTags
{
    public class GetDetailSampleTagHandler : IRequestHandler<GetDetailSampleTagQuery, Result<SampleTag>>
    {
        private readonly ISampleTagSqlRepository _sampleTagRepository;

        public GetDetailSampleTagHandler(ISampleTagSqlRepository sampleTagRepository)
        {
            _sampleTagRepository = sampleTagRepository;
        }

        public async Task<Result<SampleTag>> Handle(GetDetailSampleTagQuery request, CancellationToken cancellationToken)
        {
            GetDetailSampleTagValidator validator = new();
            validator.ValidateAndThrow(request);

            return await _sampleTagRepository.FindByIdAsync((int)request.Id, false, cancellationToken, st => st.Tag, st => st.Sample);
        }
    }
}
