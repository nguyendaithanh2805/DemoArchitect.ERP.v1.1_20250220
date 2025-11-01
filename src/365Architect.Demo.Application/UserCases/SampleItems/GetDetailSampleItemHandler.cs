using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _365Architect.Demo.Application.Requests.SampleItems;
using _365Architect.Demo.Contract.Enumerations;
using _365Architect.Demo.Contract.Exceptions;
using _365Architect.Demo.Contract.Shared;
using _365Architect.Demo.Domain.Abstractions.Repositories.Sql;
using _365Architect.Demo.Domain.Constants;
using _365Architect.Demo.Domain.Entities;
using MediatR;

namespace _365Architect.Demo.Application.UserCases.SampleItems
{
    public class GetDetailSampleItemHandler : IRequestHandler<GetDetailSampleItemQuery, Result<SampleItem>>
    {
        private readonly ISampleSqlRepository _sampleSqlRepository;

        public GetDetailSampleItemHandler(ISampleSqlRepository sampleSqlRepository)
        {
            _sampleSqlRepository = sampleSqlRepository;
        }

        public async Task<Result<SampleItem>> Handle(GetDetailSampleItemQuery request, CancellationToken cancellationToken)
        {
            var sample = await _sampleSqlRepository.FindByIdAsync((int)request.SampleId, false, cancellationToken, s => s.Items);

            var sampleItem = sample.Items.FirstOrDefault(s => s.Id == request.Id);

            if (sampleItem == null)
                CustomException.ThrowNotFoundException(typeof(SampleItem), MsgCode.ERR_SAMPLE_ITEM_ID_NOT_FOUND, SampleItemConst.MSG_SAMPLE_ITEM_ID_NOT_FOUND);

            return sampleItem;
        }
    }
}
