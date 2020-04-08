using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WhiskyArchive.Services.WhiskyRecording.API.Application.Commands
{
    public class UpdateWhiskyImageCommand : IRequest<bool>
    {
        public UpdateWhiskyImageCommand(string whiskyId, List<string> urls)
        {
            WhiskyId = whiskyId ?? throw new ArgumentNullException(nameof(whiskyId));
            Urls = urls ?? throw new ArgumentNullException(nameof(urls));
        }

        [DataMember]
        public string WhiskyId { get; private set; }

        [DataMember]
        public List<string> Urls { get; private set; }

   
    }
}
