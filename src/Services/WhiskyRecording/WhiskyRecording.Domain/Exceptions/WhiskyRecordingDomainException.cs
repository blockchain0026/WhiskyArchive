using System;
using System.Collections.Generic;
using System.Text;

namespace WhiskyArchive.Services.WhiskyRecording.Domain.Exceptions
{
    public class WhiskyRecordingDomainException : Exception
    {
        public WhiskyRecordingDomainException()
        { }

        public WhiskyRecordingDomainException(string message)
            : base(message)
        { }

        public WhiskyRecordingDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
