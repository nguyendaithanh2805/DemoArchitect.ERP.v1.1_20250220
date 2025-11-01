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
    public class SampleItemConfig : IEntityTypeConfiguration<SampleItem>
    {
        public void Configure(EntityTypeBuilder<SampleItem> builder)
        {
            builder.ToTable(SampleItemConst.TABLE_NAME);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasColumnName(SampleItemConst.FIELD_NAME)
                .HasMaxLength(SampleItemConst.NAME_MAX_LENGTH)
                .IsRequired();

            builder.Property(x => x.SampleId)
                .HasColumnName(SampleItemConst.FIELD_SAMPLE_ID)
                .IsRequired();

            builder.HasOne(x => x.Sample)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.SampleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
