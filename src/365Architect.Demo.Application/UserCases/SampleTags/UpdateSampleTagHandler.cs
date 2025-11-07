using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _365Architect.Demo.Application.Requests.SampleTags;
using _365Architect.Demo.Application.Requests.Tags;
using _365Architect.Demo.Contract.Shared;
using _365Architect.Demo.Domain.Abstractions.Repositories.Sql.Base;
using _365Architect.Demo.Domain.Abstractions.Repositories.Sql;
using MediatR;
using _365Architect.Demo.Application.Validators.SampleTags;
using _365Architect.Demo.Domain.Entities;
using _365Architect.Demo.Contract.DependencyInjection.Extensions;
using System.Data;

namespace _365Architect.Demo.Application.UserCases.SampleTags
{
    public class UpdateSampleTagHandler : IRequestHandler<UpdateSampleTagCommand, Result<object>>
    {
        private readonly ISampleTagSqlRepository _sampleTagSqlRepository;
        private readonly ISqlUnitOfWork _unitOfWork;
        private readonly ITagSqlRepository _tagSqlRepository;
        private readonly ISampleSqlRepository _sampleSqlRepository;

        public UpdateSampleTagHandler(ISampleTagSqlRepository sampleTagSqlRepository, ISqlUnitOfWork unitOfWork, ITagSqlRepository tagSqlRepository, ISampleSqlRepository sampleSqlRepository)
        {
            _sampleTagSqlRepository = sampleTagSqlRepository;
            _unitOfWork = unitOfWork;
            _tagSqlRepository = tagSqlRepository;
            _sampleSqlRepository = sampleSqlRepository;
        }

        public async Task<Result<object>> Handle(UpdateSampleTagCommand request, CancellationToken cancellationToken)
        {
            UpdateSampleTagValidator validator = new();
            validator.ValidateAndThrow(request);

            Tag tag = await _tagSqlRepository.FindByIdAsync((int)request.TagId, true, cancellationToken);
            Sample sample = await _sampleSqlRepository.FindByIdAsync((int)request.SampleId, true, cancellationToken);

            SampleTag sampleTag = await _sampleTagSqlRepository.FindByIdAsync((int)request.Id, true, cancellationToken);

            request.MapTo(sampleTag, true);

            using IDbTransaction transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                _sampleTagSqlRepository.Update(sampleTag!);

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
