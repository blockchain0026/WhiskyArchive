using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WhiskyArchive.Services.WhiskyRecording.API.Application.Commands
{

    public class CreateWhiskyRecordCommand : IRequest<bool>
    {
        public CreateWhiskyRecordCommand(string distilleryName, string whiskyNameChinese, string whiskyNameEnglish, string whiskyBottler,
             string vintage = null, string bottled = null, int? statedAge = null, string caskType = null, string caskNumber = null, int? numberOfBottles = null, float? strength = null, int? size = null, string market = null,
             float? rating = null, string notes = null,
             decimal? price = null, int? currencyId = null, int? priceReferenceId = null, int? priceDateYear = null, int? priceDateMonth = null, int? priceDateDay = null, string seller = null,
             List<string> imageUrls = null)
        {
            DistilleryName = distilleryName ?? throw new ArgumentNullException(nameof(distilleryName));
            WhiskyNameChinese = whiskyNameChinese ?? throw new ArgumentNullException(nameof(whiskyNameChinese));
            WhiskyNameEnglish = whiskyNameEnglish;
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
            Price = price;
            CurrencyId = currencyId;
            PriceReferenceId = priceReferenceId;
            PriceDateYear = priceDateYear;
            PriceDateMonth = priceDateMonth;
            PriceDateDay = priceDateDay;
            Seller = seller;
            ImageUrls = imageUrls;
        }



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



        [DataMember]
        public decimal? Price { get; private set; }

        [DataMember]
        public int? CurrencyId { get; private set; }

        [DataMember]
        public int? PriceReferenceId { get; private set; }

        [DataMember]
        public int? PriceDateYear { get; private set; }

        [DataMember]
        public int? PriceDateMonth { get; private set; }

        [DataMember]
        public int? PriceDateDay { get; private set; }

        [DataMember]
        public string Seller { get; private set; }

        [DataMember]
        public List<string> ImageUrls { get; private set; }
    }
}
