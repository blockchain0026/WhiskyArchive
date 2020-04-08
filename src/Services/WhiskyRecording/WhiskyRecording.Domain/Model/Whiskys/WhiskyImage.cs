using System;
using System.Collections.Generic;
using System.Text;
using WhiskyArchive.Services.WhiskyRecording.Domain.Exceptions;
using WhiskyArchive.Services.WhiskyRecording.Domain.SeedWork;

namespace WhiskyArchive.Services.WhiskyRecording.Domain.Model.Whiskys
{
    public class WhiskyImage : Entity
    {
        public int WhiskyImageNumber { get; private set; }
        public string WhiskyId { get; private set; }
        public string ImageUrl { get; private set; }
        public string Description { get; private set; }



        protected WhiskyImage()
        {

        }

        public WhiskyImage(int whiskyImageNumber, string whiskyId, string imageUrl, string description) : this()
        {
            this.WhiskyImageNumber = whiskyImageNumber;
            this.WhiskyId = whiskyId ?? throw new WhiskyRecordingDomainException("Parameter not provided:" + nameof(whiskyId));
            this.ImageUrl = imageUrl ?? throw new WhiskyRecordingDomainException("Parameter not provided:" + nameof(imageUrl));
            this.Description = description ?? throw new WhiskyRecordingDomainException("Parameter not provided:" + nameof(description));
        }

        public void UpdateDescription(string description)
        {
            this.Description = description;
        }
    }
}
