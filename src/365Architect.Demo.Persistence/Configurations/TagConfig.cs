using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _365Architect.Demo.Domain.Constants;
using _365Architect.Demo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _365Architect.Demo.Persistence.Configurations
{
    public class TagConfig : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable(TagConst.TABLE_NAME);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasColumnName(TagConst.FIELD_NAME).HasMaxLength(TagConst.NAME_MAX_LENGTH);
        }
    }
}
