using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _365Architect.Demo.Application.Requests.Tags;
using _365Architect.Demo.Contract.DependencyInjection.Extensions;
using _365Architect.Demo.Contract.Enumerations;
using _365Architect.Demo.Contract.Validators;
using _365Architect.Demo.Domain.Constants;

namespace _365Architect.Demo.Application.Validators.Tags
{
    public class CreateTagValidator : Validator<CreateTagCommand>
    {
        public CreateTagValidator()
        {
            WithValidator(MsgCode.ERR_TAG_INVALID);
            RuleFor(x => x.Name).NotNull()!.NotEmpty().MaxLength(TagConst.NAME_MAX_LENGTH);
        }
    }
}
