using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WhiskyArchive.Services.WhiskyRecording.Domain.Model.Whiskys;

namespace WhiskyArchive.Services.WhiskyRecording.Infrastructure.EntityConfigurations
{
    public class WhiskyPriceEntityTypeConfiguration : IEntityTypeConfiguration<WhiskyPrice>
    {
        public void Configure(EntityTypeBuilder<WhiskyPrice> whiskyPriceConfiguration)
        {
            whiskyPriceConfiguration.ToTable("whiskyprices", WhiskyRecordingContext.DEFAULT_SCHEMA);

            whiskyPriceConfiguration.HasKey(i => i.Id);

            whiskyPriceConfiguration.Ignore(i => i.DomainEvents);

            whiskyPriceConfiguration.Property(i => i.Id)
                .ForSqlServerUseSequenceHiLo("whiskypriceseq", WhiskyRecordingContext.DEFAULT_SCHEMA);



            whiskyPriceConfiguration.Property(i => i.WhiskyPriceNumber).IsRequired();
            whiskyPriceConfiguration.Property(i => i.WhiskyId).IsRequired();
            

            whiskyPriceConfiguration.Property(i => i.Price).HasColumnType("decimal(20,2)").IsRequired();

            whiskyPriceConfiguration.Property<int>("CurrencyId").IsRequired();
            whiskyPriceConfiguration.Property<int>("PriceReferenceId").IsRequired();

            whiskyPriceConfiguration.Property(i => i.Seller).IsRequired();
            whiskyPriceConfiguration.Property(i => i.PriceDate).IsRequired();

            whiskyPriceConfiguration.HasOne(i => i.Currency)
                .WithMany()
                .HasForeignKey("CurrencyId");
            whiskyPriceConfiguration.HasOne(i => i.PriceReference)
                .WithMany()
                .HasForeignKey("PriceReferenceId");
        }
    }
}
