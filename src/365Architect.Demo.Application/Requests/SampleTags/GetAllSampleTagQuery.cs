using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _365Architect.Demo.Contract.Abstractions;
using _365Architect.Demo.Domain.Entities;

namespace _365Architect.Demo.Application.Requests.SampleTags
{
    public class GetAllSampleTagQuery : IQuery<List<SampleTag>>
    {
    }
}
