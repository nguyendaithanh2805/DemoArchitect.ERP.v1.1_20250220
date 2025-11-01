using System.Data;
using _365Architect.Demo.Application.Requests.SampleItems;
using _365Architect.Demo.Application.Validators.SampleItems;
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
    public class DeleteSampleItemHandler : IRequestHandler<DeleteSampleItemCommand, Result<object>>
    {
        private readonly ISampleSqlRepository _sampleSqlRepository;
        private readonly ISqlUnitOfWork _unitOfWork;

        public DeleteSampleItemHandler(ISampleSqlRepository sampleSqlRepository, ISqlUnitOfWork unitOfWork)
        {
            _sampleSqlRepository = sampleSqlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<object>> Handle(DeleteSampleItemCommand request, CancellationToken cancellationToken)
        {
            DeleteSampleItemValidator validator = new();
            validator.ValidateAndThrow(request);

            var sample = await _sampleSqlRepository.FindByIdAsync((int)request.SampleId, true, cancellationToken);

            var sampleItem = sample.Items.FirstOrDefault(s => s.Id == request.Id);
            if (sampleItem is null)
                CustomException.ThrowNotFoundException(typeof(SampleItem), MsgCode.ERR_SAMPLE_ITEM_ID_NOT_FOUND, SampleItemConst.MSG_SAMPLE_ITEM_ID_NOT_FOUND);
            
            using IDbTransaction transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {

                sample.Items.Remove(sampleItem);
                await _unitOfWork.SaveChangesAsync();
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
