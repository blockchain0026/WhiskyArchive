using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WhiskyArchive.Services.WhiskyRecording.Domain.Model.Distilleries;

namespace WhiskyArchive.Services.WhiskyRecording.Infrastructure.EntityConfigurations
{
    public class DistilleryEntityTypeConfiguration : IEntityTypeConfiguration<Distillery>
    {
        public void Configure(EntityTypeBuilder<Distillery> distilleryConfiguration)
        {
            distilleryConfiguration.ToTable("distilleries", WhiskyRecordingContext.DEFAULT_SCHEMA);

            distilleryConfiguration.HasKey(d => d.Id);

            distilleryConfiguration.Ignore(d => d.DomainEvents);

            distilleryConfiguration.Property(d => d.Id)
                .ForSqlServerUseSequenceHiLo("distilleryseq", WhiskyRecordingContext.DEFAULT_SCHEMA);



            distilleryConfiguration.Property(d => d.DistilleryId)
                .HasMaxLength(200)
                .IsRequired();

            distilleryConfiguration.HasIndex("DistilleryId")
              .IsUnique(true);

            distilleryConfiguration.OwnsOne(w => w.DistilleryName);

            distilleryConfiguration.Property(w => w.Established).IsRequired();
            distilleryConfiguration.Property(w => w.Introdution).IsRequired();
            distilleryConfiguration.Property(w => w.SmwsCode).IsRequired();
        }
    }
}
