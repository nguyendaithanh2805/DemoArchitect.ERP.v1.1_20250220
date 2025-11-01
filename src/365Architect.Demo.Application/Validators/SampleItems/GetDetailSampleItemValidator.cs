using _365Architect.Demo.Application.Requests.SampleItems;
using _365Architect.Demo.Contract.DependencyInjection.Extensions;
using _365Architect.Demo.Contract.Enumerations;
using _365Architect.Demo.Contract.Validators;

namespace _365Architect.Demo.Application.Validators.SampleItems
{
    public class GetDetailSampleItemValidator : Validator<GetDetailSampleItemQuery>
    {
        public GetDetailSampleItemValidator()
        {
            WithValidator(MsgCode.ERR_SAMPLE_ITEM_INVALID);
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
        }
    }
}
