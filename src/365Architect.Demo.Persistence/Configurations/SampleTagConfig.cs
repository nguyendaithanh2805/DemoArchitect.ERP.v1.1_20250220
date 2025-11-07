using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _365Architect.Demo.Domain.Abstractions.Entities;
using _365Architect.Demo.Domain.Constants;
using _365Architect.Demo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _365Architect.Demo.Persistence.Configurations
{
    public class SampleTagConfig : IEntityTypeConfiguration<SampleTag>
    {
        public void Configure(EntityTypeBuilder<SampleTag> builder)
        {
            builder.ToTable(SampleTagConst.TABLE_NAME);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Note).HasColumnName(SampleTagConst.FIELD_NOTE).HasMaxLength(SampleTagConst.NOTE_MAX_LENGTH);

            builder.HasOne(x => x.Sample)
                  .WithMany(x => x.SampleTags)
                  .HasForeignKey(x => x.SampleId)
                  .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Tag)
                  .WithMany(x => x.SampleTags)
                  .HasForeignKey(x => x.TagId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
