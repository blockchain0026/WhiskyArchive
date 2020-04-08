using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WhiskyArchive.Services.WhiskyRecording.API.Application.Commands
{
    public class UpdateWhiskyPriceCommand : IRequest<bool>
    {
        public UpdateWhiskyPriceCommand(string whiskyId, decimal price, int currencyId, int priceReferenceId, int priceDateYear, int priceDateMonth, int priceDateDay, string seller = null, int? priceNumber = null)
        {
            WhiskyId = whiskyId ?? throw new ArgumentNullException(nameof(whiskyId));
            Price = price;
            CurrencyId = currencyId;
            PriceReferenceId = priceReferenceId;
            PriceDateYear = priceDateYear;
            PriceDateMonth = priceDateMonth;
            PriceDateDay = priceDateDay;
            Seller = seller;
            this.PriceNumber = priceNumber;
        }

        [DataMember]
        public string WhiskyId { get; private set; }

        [DataMember]
        public decimal Price { get; private set; }

        [DataMember]
        public int CurrencyId { get; private set; }

        [DataMember]
        public int PriceReferenceId { get; private set; }

        [DataMember]
        public int PriceDateYear { get; private set; }

        [DataMember]
        public int PriceDateMonth { get; private set; }

        [DataMember]
        public int PriceDateDay { get; private set; }

        [DataMember]
        public string Seller { get; private set; }

        [DataMember]
        public int? PriceNumber { get; private set; }
    }
}
