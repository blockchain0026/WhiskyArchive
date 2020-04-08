using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WhiskyArchive.Services.WhiskyRecording.Domain.Model.Whiskys;

namespace WhiskyArchive.Services.WhiskyRecording.Infrastructure.EntityConfigurations
{
    public class WhiskyImageEntityTypeConfiguration : IEntityTypeConfiguration<WhiskyImage>
    {
        public void Configure(EntityTypeBuilder<WhiskyImage> whiskyImageConfiguration)
        {
            whiskyImageConfiguration.ToTable("whiskyimages", WhiskyRecordingContext.DEFAULT_SCHEMA);

            whiskyImageConfiguration.HasKey(i => i.Id);

            whiskyImageConfiguration.Ignore(i => i.DomainEvents);

            whiskyImageConfiguration.Property(i => i.Id)
                .ForSqlServerUseSequenceHiLo("whiskyimageseq", WhiskyRecordingContext.DEFAULT_SCHEMA);



            whiskyImageConfiguration.Property(i => i.WhiskyImageNumber).IsRequired();
            whiskyImageConfiguration.Property(i => i.WhiskyId).IsRequired();
            whiskyImageConfiguration.Property(i => i.ImageUrl).IsRequired();
            whiskyImageConfiguration.Property(i => i.Description).IsRequired();

        }
    }
}
