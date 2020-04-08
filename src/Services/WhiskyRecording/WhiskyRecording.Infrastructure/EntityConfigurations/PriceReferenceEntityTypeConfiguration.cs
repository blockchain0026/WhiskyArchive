using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WhiskyArchive.Services.WhiskyRecording.Domain.Model.Whiskys;

namespace WhiskyArchive.Services.WhiskyRecording.Infrastructure.EntityConfigurations
{
    public class PriceReferenceEntityTypeConfiguration : IEntityTypeConfiguration<PriceReference>
    {
        public void Configure(EntityTypeBuilder<PriceReference> priceReferenceConfiguration)
        {
            priceReferenceConfiguration.ToTable("pricereference", WhiskyRecordingContext.DEFAULT_SCHEMA);

            priceReferenceConfiguration.HasKey(c => c.Id);

            priceReferenceConfiguration.Property(c => c.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            priceReferenceConfiguration.Property(c => c.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
