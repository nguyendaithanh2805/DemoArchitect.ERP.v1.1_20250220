using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _365Architect.Demo.Application.Requests.SampleTags;
using _365Architect.Demo.Contract.DependencyInjection.Extensions;
using _365Architect.Demo.Contract.Enumerations;
using _365Architect.Demo.Contract.Validators;

namespace _365Architect.Demo.Application.Validators.SampleTags
{
    public class DeteleSampleTagValidator : Validator<DeleteSampleTagCommand>
    {
        public DeteleSampleTagValidator()
        {
            WithValidator(MsgCode.ERR_SAMPLE_TAG_INVALID);
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
        }
    }
}
