using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WhiskyArchive.Services.WhiskyRecording.API.Application.Commands
{
    public class UpdateWhiskyRecordCommand : IRequest<bool>
    {
        public UpdateWhiskyRecordCommand(string whiskyId,string distilleryName, string whiskyNameChinese, string whiskyNameEnglish, string whiskyBottler,
             string vintage = null, string bottled = null, int? statedAge = null, string caskType = null, string caskNumber = null, int? numberOfBottles = null, float? strength = null, int? size = null, string market = null,
             float? rating = null, string notes = null)
        {
            WhiskyId = whiskyId ?? throw new ArgumentNullException(nameof(whiskyId));
            DistilleryName = distilleryName ?? throw new ArgumentNullException(nameof(distilleryName));
            WhiskyNameChinese = whiskyNameChinese ?? throw new ArgumentNullException(nameof(whiskyNameChinese));
            WhiskyNameEnglish = whiskyNameEnglish ?? throw new ArgumentNullException(nameof(whiskyNameEnglish));
            WhiskyBottler = whiskyBottler ?? throw new ArgumentNullException(nameof(whiskyBottler));
            Vintage = vintage;
            Bottled = bottled;
            StatedAge = statedAge;
            CaskType = caskType;
            CaskNumber = caskNumber;
            NumberOfBottles = numberOfBottles;
            Strength = strength;
            Size = size;
            Market = market;
            Rating = rating;
            Notes = notes;
        }

        [DataMember]
        public string WhiskyId { get; private set; }

        [DataMember]
        public string DistilleryName { get; private set; }

        [DataMember]
        public string WhiskyNameChinese { get; private set; }

        [DataMember]
        public string WhiskyNameEnglish { get; private set; }

        [DataMember]
        public string WhiskyBottler { get; private set; }

        [DataMember]
        public string Vintage { get; private set; }

        [DataMember]
        public string Bottled { get; private set; }

        [DataMember]
        public int? StatedAge { get; private set; }

        [DataMember]
        public string CaskType { get; private set; }

        [DataMember]
        public string CaskNumber { get; private set; }

        [DataMember]
        public int? NumberOfBottles { get; private set; }

        [DataMember]
        public float? Strength { get; private set; }

        [DataMember]
        public int? Size { get; private set; }

        [DataMember]
        public string Market { get; private set; }

        [DataMember]
        public float? Rating { get; private set; }

        [DataMember]
        public string Notes { get; private set; }
    }
}
