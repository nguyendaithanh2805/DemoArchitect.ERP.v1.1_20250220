using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _365Architect.Demo.Contract.Abstractions;

namespace _365Architect.Demo.Application.Requests.SampleItems
{
    public class UpdateSampleItemCommand : ICommand
    {
        public int? Id { get; set; }
        public string? Name { get; set; }

        public int? SampleId { get; set; }
    }
}
