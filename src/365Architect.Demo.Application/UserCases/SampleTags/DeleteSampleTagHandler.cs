using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _365Architect.Demo.Application.Requests.SampleTags;
using _365Architect.Demo.Contract.Shared;
using _365Architect.Demo.Domain.Abstractions.Repositories.Sql.Base;
using _365Architect.Demo.Domain.Abstractions.Repositories.Sql;
using MediatR;
using _365Architect.Demo.Application.Validators.SampleTags;
using _365Architect.Demo.Contract.DependencyInjection.Extensions;
using _365Architect.Demo.Domain.Entities;
using System.Data;

namespace _365Architect.Demo.Application.UserCases.SampleTags
{
    public class DeleteSampleTagHandler : IRequestHandler<DeleteSampleTagCommand, Result<object>>
    {
        private readonly ISampleTagSqlRepository _sampleTagSqlRepository;
        private readonly ISqlUnitOfWork _unitOfWork;

        public DeleteSampleTagHandler(ISampleTagSqlRepository sampleTagSqlRepository, ISqlUnitOfWork unitOfWork)
        {
            _sampleTagSqlRepository = sampleTagSqlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<object>> Handle(DeleteSampleTagCommand request, CancellationToken cancellationToken)
        {
            DeteleSampleTagValidator validator = new();
            validator.ValidateAndThrow(request);

            SampleTag sampleTag = await _sampleTagSqlRepository.FindByIdAsync((int)request.Id, true, cancellationToken);

            using IDbTransaction transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                _sampleTagSqlRepository.Remove(sampleTag);
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
