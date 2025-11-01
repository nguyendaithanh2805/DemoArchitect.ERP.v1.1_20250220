using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using _365Architect.Demo.Domain.Abstractions.Entities;

namespace _365Architect.Demo.Domain.Entities
{
    public class SampleItem : Entity<int>
    {
        public string? Name { get; set; }

        public int? SampleId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [JsonIgnore]
        public Sample? Sample { get; set; }
    }

}
