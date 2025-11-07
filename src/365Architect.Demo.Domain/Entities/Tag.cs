using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using _365Architect.Demo.Domain.Abstractions.Aggregates;

namespace _365Architect.Demo.Domain.Entities
{
    public class Tag : AggregateRoot<int>
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ICollection<SampleTag> SampleTags { get; set; } = new List<SampleTag>();
    }
}
