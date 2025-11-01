using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _365Architect.Demo.Application.Requests.SampleItems;
using _365Architect.Demo.Application.Requests.Samples;
using _365Architect.Demo.Application.Validators.SampleItems;
using _365Architect.Demo.Contract.DependencyInjection.Extensions;
using _365Architect.Demo.Contract.Shared;
using _365Architect.Demo.Domain.Abstractions.Repositories.Sql;
using _365Architect.Demo.Domain.Abstractions.Repositories.Sql.Base;
using _365Architect.Demo.Domain.Entities;
using MediatR;

namespace _365Architect.Demo.Application.UserCases.SampleItems
{
    public class CreateSampleItemHandler : IRequestHandler<CreateSampleItemCommand, Result<object>>
    {
        private readonly ISampleSqlRepository _sampleSqlRepository;
        private readonly ISqlUnitOfWork _unitOfWork;

        public CreateSampleItemHandler(ISampleSqlRepository sampleSqlRepository, ISqlUnitOfWork unitOfWork)
        {
            _sampleSqlRepository = sampleSqlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<object>> Handle(CreateSampleItemCommand request, CancellationToken cancellationToken)
        {
            CreateSampleItemValidator validator = new();
            validator.ValidateAndThrow(request);

            var sample = await _sampleSqlRepository.FindByIdAsync((int)request.SampleId, true, cancellationToken);

            SampleItem? sampleItem = request.MapTo<SampleItem>();

            using IDbTransaction transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                sample.Items.Add(sampleItem);
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
