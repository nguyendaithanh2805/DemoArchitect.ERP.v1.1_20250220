using _365Architect.Demo.Application.Requests.SampleItems;
using _365Architect.Demo.Application.Requests.Samples;
using _365Architect.Demo.Contract.DependencyInjection.Extensions;
using _365Architect.Demo.Contract.Enumerations;
using _365Architect.Demo.Contract.Validators;

namespace _365Architect.Demo.Application.Validators.SampleItems
{
    public class DeleteSampleItemValidator : Validator<DeleteSampleItemCommand>
    {
        public DeleteSampleItemValidator()
        {
            WithValidator(MsgCode.ERR_SAMPLE_ITEM_INVALID);
            RuleFor(x => x.SampleId).NotNull().GreaterThan(0);
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
        }
    }
}
