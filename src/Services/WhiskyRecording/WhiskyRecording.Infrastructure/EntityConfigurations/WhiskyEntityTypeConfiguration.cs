using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WhiskyArchive.Services.WhiskyRecording.Domain.Model.Whiskys;

namespace WhiskyArchive.Services.WhiskyRecording.Infrastructure.EntityConfigurations
{

    public class WhiskyEntityTypeConfiguration : IEntityTypeConfiguration<Whisky>
    {
        public void Configure(EntityTypeBuilder<Whisky> whiskyConfiguration)
        {
            whiskyConfiguration.ToTable("whiskys", WhiskyRecordingContext.DEFAULT_SCHEMA);

            whiskyConfiguration.HasKey(w => w.Id);

            whiskyConfiguration.Ignore(w => w.DomainEvents);

            whiskyConfiguration.Property(w => w.Id)
                .ForSqlServerUseSequenceHiLo("whiskyseq", WhiskyRecordingContext.DEFAULT_SCHEMA);



            whiskyConfiguration.Property(w => w.WhiskyId)
                .HasMaxLength(200)
                .IsRequired();

            whiskyConfiguration.HasIndex("WhiskyId")
              .IsUnique(true);

            whiskyConfiguration.OwnsOne(w => w.WhiskyName);

            whiskyConfiguration.Property(w => w.DistilleryId).IsRequired();
            whiskyConfiguration.Property(w => w.Bottler).IsRequired();

            whiskyConfiguration.OwnsOne(w => w.WhiskyDetail);

            whiskyConfiguration.Property(w => w.WhiskyBaseRating).IsRequired();
            whiskyConfiguration.Property(w => w.Notes).IsRequired();
            whiskyConfiguration.Property(w => w.DateUpdated).IsRequired();




            whiskyConfiguration.HasMany(w => w.WhiskyPrices)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            whiskyConfiguration.HasMany(w => w.WhiskyImages)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
