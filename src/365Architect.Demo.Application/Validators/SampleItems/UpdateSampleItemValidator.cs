using _365Architect.Demo.Application.Requests.SampleItems;
using _365Architect.Demo.Contract.DependencyInjection.Extensions;
using _365Architect.Demo.Contract.Enumerations;
using _365Architect.Demo.Contract.Validators;
using _365Architect.Demo.Domain.Constants;

namespace _365Architect.Demo.Application.Validators.SampleItems
{
    public class UpdateSampleItemValidator : Validator<UpdateSampleItemCommand>
    {
        public UpdateSampleItemValidator()
        {
            WithValidator(MsgCode.ERR_SAMPLE_ITEM_INVALID);
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
            RuleFor(x => x.SampleId).NotNull().GreaterThan(0);
            RuleFor(x => x.Name).NotNull()!.NotEmpty().MaxLength(SampleItemConst.NAME_MAX_LENGTH);
        }
    }
}
