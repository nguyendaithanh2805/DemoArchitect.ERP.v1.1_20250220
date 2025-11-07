using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _365Architect.Demo.Application.Requests.SampleTags;
using _365Architect.Demo.Application.Validators.SampleTags;
using _365Architect.Demo.Contract.DependencyInjection.Extensions;
using _365Architect.Demo.Contract.Shared;
using _365Architect.Demo.Domain.Abstractions.Repositories.Sql;
using _365Architect.Demo.Domain.Abstractions.Repositories.Sql.Base;
using _365Architect.Demo.Domain.Entities;
using MediatR;

namespace _365Architect.Demo.Application.UserCases.SampleTags
{
    public class CreateSampleTagHandler : IRequestHandler<CreateSampleTagCommand, Result<object>>
    {
        private readonly ISampleSqlRepository _sampleSqlRepository;
        private readonly ITagSqlRepository _tagSqlRepository;
        private readonly ISampleTagSqlRepository _sampleTagSqlRepository;
        private readonly ISqlUnitOfWork _unitOfWork;

        public CreateSampleTagHandler(ISampleSqlRepository sampleSqlRepository, ITagSqlRepository tagSqlRepository, ISampleTagSqlRepository sampleTagSqlRepository, ISqlUnitOfWork unitOfWork)
        {
            _sampleSqlRepository = sampleSqlRepository;
            _tagSqlRepository = tagSqlRepository;
            _sampleTagSqlRepository = sampleTagSqlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<object>> Handle(CreateSampleTagCommand request, CancellationToken cancellationToken)
        {
            CreateSampleTagValidator validator = new CreateSampleTagValidator();
            validator.ValidateAndThrow (request);

            Tag tag = await _tagSqlRepository.FindByIdAsync((int)request.TagId, true, cancellationToken);
            Sample sample = await _sampleSqlRepository.FindByIdAsync((int)request.SampleId, true, cancellationToken);

            SampleTag? sampleTag = request.MapTo<SampleTag>();
            using IDbTransaction transaction = await _unitOfWork.BeginTransactionAsync (cancellationToken);
            try
            {
               _sampleTagSqlRepository.Add(sampleTag);
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
