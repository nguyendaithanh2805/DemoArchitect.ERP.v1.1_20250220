using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _365Architect.Demo.Application.Requests.SampleTags;
using _365Architect.Demo.Contract.DependencyInjection.Extensions;
using _365Architect.Demo.Contract.Enumerations;
using _365Architect.Demo.Contract.Validators;
using _365Architect.Demo.Domain.Constants;
using _365Architect.Demo.Domain.Entities;

namespace _365Architect.Demo.Application.Validators.SampleTags
{
    public class UpdateSampleTagValidator : Validator<UpdateSampleTagCommand>
    {
        public UpdateSampleTagValidator()
        {
            WithValidator(MsgCode.ERR_SAMPLE_TAG_INVALID);
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
            RuleFor(x => x.SampleId).NotNull().GreaterThan(0);
            RuleFor(x => x.TagId).NotNull().GreaterThan(0);
            RuleFor(x => x.Note).NotNull()!.NotEmpty().MaxLength(SampleTagConst.NOTE_MAX_LENGTH);
        }
    }
}
