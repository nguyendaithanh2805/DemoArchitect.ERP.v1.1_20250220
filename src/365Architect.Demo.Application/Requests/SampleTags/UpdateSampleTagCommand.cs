using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _365Architect.Demo.Contract.Abstractions;

namespace _365Architect.Demo.Application.Requests.SampleTags
{
    public class UpdateSampleTagCommand : ICommand
    {
        public int? Id { get; set; }
        public int? SampleId { get; set; }
        public int? TagId { get; set; }
        public string? Note { get; set; }
    }
}
