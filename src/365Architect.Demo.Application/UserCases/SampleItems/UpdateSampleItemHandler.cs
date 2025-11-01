using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _365Architect.Demo.Application.Requests.SampleItems;
using _365Architect.Demo.Application.Validators.SampleItems;
using _365Architect.Demo.Contract.DependencyInjection.Extensions;
using _365Architect.Demo.Contract.Enumerations;
using _365Architect.Demo.Contract.Exceptions;
using _365Architect.Demo.Contract.Shared;
using _365Architect.Demo.Domain.Abstractions.Repositories.Sql;
using _365Architect.Demo.Domain.Abstractions.Repositories.Sql.Base;
using _365Architect.Demo.Domain.Constants;
using _365Architect.Demo.Domain.Entities;
using MediatR;

namespace _365Architect.Demo.Application.UserCases.SampleItems
{
    public class UpdateSampleItemHandler : IRequestHandler<UpdateSampleItemCommand, Result<object>>
    {
        private readonly ISampleSqlRepository _sampleSqlRepository;
        private readonly ISqlUnitOfWork _unitOfWork;

        public UpdateSampleItemHandler(ISampleSqlRepository sampleSqlRepository, ISqlUnitOfWork unitOfWork)
        {
            _sampleSqlRepository = sampleSqlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<object>> Handle(UpdateSampleItemCommand request, CancellationToken cancellationToken)
        {
            UpdateSampleItemValidator validator = new();
            validator.ValidateAndThrow(request);

            var sample = await _sampleSqlRepository.FindByIdAsync((int)request.SampleId, true, cancellationToken, s => s.Items);

            var sampleItem = sample.Items.FirstOrDefault(s => s.Id == request.Id);
            if (sampleItem == null)
                CustomException.ThrowNotFoundException(typeof(SampleItem), MsgCode.ERR_SAMPLE_ITEM_ID_NOT_FOUND, SampleItemConst.MSG_SAMPLE_ITEM_ID_NOT_FOUND);
            
            sampleItem.UpdatedAt = DateTime.UtcNow;
            request.MapTo(sampleItem, true);
            using IDbTransaction transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                transaction.Commit();
                return Result<object>.Ok();
            } 
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }

        }
    }
}
