using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _365Architect.Demo.Application.Requests.Samples;
using _365Architect.Demo.Application.Requests.Tags;
using _365Architect.Demo.Contract.Shared;
using _365Architect.Demo.Domain.Abstractions.Repositories.Sql.Base;
using _365Architect.Demo.Domain.Abstractions.Repositories.Sql;
using MediatR;
using _365Architect.Demo.Application.Validators.Samples;
using _365Architect.Demo.Contract.DependencyInjection.Extensions;
using _365Architect.Demo.Domain.Entities;
using System.Data;
using _365Architect.Demo.Application.Validators.Tags;

namespace _365Architect.Demo.Application.UserCases.Tags
{
    public class UpdateTagHandler : IRequestHandler<UpdateTagCommand, Result<object>>
    {
        private readonly ITagSqlRepository _tagSqlRepository;
        private readonly ISqlUnitOfWork _unitOfWork;

        public UpdateTagHandler(ITagSqlRepository tagSqlRepository, ISqlUnitOfWork unitOfWork)
        {
            _tagSqlRepository = tagSqlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<object>> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            UpdateTagValidator validator = new();
            validator.ValidateAndThrow(request);

            Tag tag = await _tagSqlRepository.FindByIdAsync((int)request.Id, true, cancellationToken);

            request.MapTo(tag, true);

            using IDbTransaction transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                _tagSqlRepository.Update(tag!);

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
