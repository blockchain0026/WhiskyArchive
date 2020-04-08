using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WhiskyArchive.Services.WhiskyRecording.API.Application.Commands
{
    public class CreateDistilleryCommand:IRequest<bool>
    {
        public CreateDistilleryCommand(string chineseTraditional, string chineseSimplified, string english, string established, string introdution, string smwsCode)
        {
            ChineseTraditional = chineseTraditional ?? throw new ArgumentNullException(nameof(chineseTraditional));
            ChineseSimplified = chineseSimplified ?? throw new ArgumentNullException(nameof(chineseSimplified));
            English = english ?? throw new ArgumentNullException(nameof(english));
            Established = established ?? throw new ArgumentNullException(nameof(established));
            Introdution = introdution ?? throw new ArgumentNullException(nameof(introdution));
            SmwsCode = smwsCode ?? throw new ArgumentNullException(nameof(smwsCode));
        }

        [DataMember]
        public string ChineseTraditional { get; private set; }

        [DataMember]
        public string ChineseSimplified { get; private set; }

        [DataMember]
        public string English { get; private set; }

        [DataMember]
        public string Established { get; private set; }

        [DataMember]
        public string Introdution { get; private set; }

        [DataMember]
        public string SmwsCode { get; private set; }
    }
}
