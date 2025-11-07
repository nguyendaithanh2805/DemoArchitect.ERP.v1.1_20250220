using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _365Architect.Demo.Application.Requests.Samples;
using _365Architect.Demo.Application.Requests.Tags;
using _365Architect.Demo.Application.Validators.Samples;
using _365Architect.Demo.Application.Validators.Tags;
using _365Architect.Demo.Contract.Shared;
using _365Architect.Demo.Domain.Abstractions.Repositories.Sql;
using _365Architect.Demo.Domain.Entities;
using MediatR;

namespace _365Architect.Demo.Application.UserCases.Tags
{
    public class GetDetailTagHandler : IRequestHandler<GetDetailTagQuery, Result<Tag>>
    {
        private readonly ITagSqlRepository _tagSqlRepository;

        public GetDetailTagHandler(ITagSqlRepository tagSqlRepository)
        {
            _tagSqlRepository = tagSqlRepository;
        }

        public async Task<Result<Tag>> Handle(GetDetailTagQuery request, CancellationToken cancellationToken)
        {
            GetDetailTagValidator validator = new();
            validator.ValidateAndThrow(request);

            return await _tagSqlRepository.FindByIdAsync((int)request.Id, false, cancellationToken, t => t.SampleTags);
        }
    }
}
