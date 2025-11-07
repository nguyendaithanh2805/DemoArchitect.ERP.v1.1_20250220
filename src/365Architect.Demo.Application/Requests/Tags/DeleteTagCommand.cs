using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _365Architect.Demo.Contract.Abstractions;

namespace _365Architect.Demo.Application.Requests.Tags
{
    public class DeleteTagCommand : ICommand
    {
        public int? Id { get; set; }
    }
}
