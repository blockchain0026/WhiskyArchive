using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WhiskyArchive.Services.WhiskyRecording.Domain.Model.Whiskys;

namespace WhiskyArchive.Services.WhiskyRecording.Infrastructure.EntityConfigurations
{
    public class CurrencyEntityTypeConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> currencyConfiguration)
        {
            currencyConfiguration.ToTable("currency", WhiskyRecordingContext.DEFAULT_SCHEMA);

            currencyConfiguration.HasKey(c => c.Id);

            currencyConfiguration.Property(c => c.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            currencyConfiguration.Property(c => c.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
