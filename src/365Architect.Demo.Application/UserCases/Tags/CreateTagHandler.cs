using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _365Architect.Demo.Application.Requests.Tags;
using _365Architect.Demo.Application.Validators.Tags;
using _365Architect.Demo.Contract.DependencyInjection.Extensions;
using _365Architect.Demo.Contract.Shared;
using _365Architect.Demo.Domain.Abstractions.Repositories.Sql;
using _365Architect.Demo.Domain.Abstractions.Repositories.Sql.Base;
using _365Architect.Demo.Domain.Entities;
using MediatR;

namespace _365Architect.Demo.Application.UserCases.Tags
{
    public class CreateTagHandler : IRequestHandler<CreateTagCommand, Result<object>>
    {
        private readonly ITagSqlRepository _tagSqlRepository;
        private readonly ISqlUnitOfWork _unitOfWork;

        public CreateTagHandler(ITagSqlRepository tagSqlRepository, ISqlUnitOfWork unitOfWork)
        {
            _tagSqlRepository = tagSqlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<object>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            CreateTagValidator validator = new();
            validator.ValidateAndThrow(request);

            Tag? tag = request.MapTo<Tag>();
            
            using IDbTransaction transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                _tagSqlRepository.Add(tag);

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
