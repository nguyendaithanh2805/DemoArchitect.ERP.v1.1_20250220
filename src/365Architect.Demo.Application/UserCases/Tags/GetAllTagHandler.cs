using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _365Architect.Demo.Application.Requests.Samples;
using _365Architect.Demo.Application.Requests.Tags;
using _365Architect.Demo.Contract.Shared;
using _365Architect.Demo.Domain.Abstractions.Repositories.Sql;
using _365Architect.Demo.Domain.Entities;
using MediatR;

namespace _365Architect.Demo.Application.UserCases.Tags
{
    public class GetAllTagHandler : IRequestHandler<GetAllTagQuery, Result<List<Tag>>>
    {
        private readonly ITagSqlRepository _tagSqlRepository;

        public GetAllTagHandler(ITagSqlRepository tagSqlRepository)
        {
            _tagSqlRepository = tagSqlRepository;
        }

        public Task<Result<List<Tag>>> Handle(GetAllTagQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult<Result<List<Tag>>>(_tagSqlRepository.FindAll(null, false, t => t.SampleTags).ToList());
        }
    }
}
