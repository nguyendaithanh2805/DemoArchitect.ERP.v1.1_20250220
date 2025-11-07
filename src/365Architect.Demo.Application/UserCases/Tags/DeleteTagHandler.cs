using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _365Architect.Demo.Contract.Shared;
using _365Architect.Demo.Domain.Abstractions.Repositories.Sql.Base;
using _365Architect.Demo.Domain.Abstractions.Repositories.Sql;
using _365Architect.Demo.Domain.Entities;
using MediatR;
using _365Architect.Demo.Application.Requests.Tags;
using _365Architect.Demo.Application.Validators.Tags;
using System.Data;

namespace _365Architect.Demo.Application.UserCases.Tags
{
    public class DeleteTagHandler : IRequestHandler<DeleteTagCommand, Result<object>>
    {
        private readonly ITagSqlRepository _tagSqlRepository;
        private readonly ISqlUnitOfWork _unitOfWork;

        public DeleteTagHandler(ITagSqlRepository tagSqlRepository, ISqlUnitOfWork unitOfWork)
        {
            _tagSqlRepository = tagSqlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<object>> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            DeleteTagValidator validator = new DeleteTagValidator();
            validator.ValidateAndThrow(request);

            Tag tag = await _tagSqlRepository.FindByIdAsync((int)request.Id, true, cancellationToken);

            using IDbTransaction transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                _tagSqlRepository.Remove(tag);
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
