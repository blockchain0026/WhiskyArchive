using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WhiskyArchive.Services.WhiskyRecording.Domain.Model.Whiskys;

namespace WhiskyArchive.Services.WhiskyRecording.Domain.Events
{
    public class WhiskyCreatedDomainEvent : INotification
    {
        public Whisky Whisky { get; private set; }


        public WhiskyCreatedDomainEvent(Whisky whisky)
        {
            Whisky = whisky;
        }
    }
}
