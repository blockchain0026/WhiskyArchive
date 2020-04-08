using System;
using System.Collections.Generic;
using System.Text;
using WhiskyArchive.Services.WhiskyRecording.Domain.Exceptions;
using WhiskyArchive.Services.WhiskyRecording.Domain.SeedWork;

namespace WhiskyArchive.Services.WhiskyRecording.Domain.Model.Whiskys
{
    public class WhiskyPrice : Entity
    {
        public int WhiskyPriceNumber { get; private set; }
        public string WhiskyId { get; private set; }
        public decimal Price { get; private set; }

        public Currency Currency { get; private set; }
        private int _currencyId;

        public PriceReference PriceReference { get; private set; }
        private int _priceReferenceId;

        public string Seller { get; private set; }
        public DateTime PriceDate { get; private set; }


        protected WhiskyPrice()
        {

        }

        public WhiskyPrice(int whiskyPriceNumber, string whiskyId, decimal price, int currencyId, int priceReferenceId, string seller, DateTime priceDate) : this()
        {
            WhiskyPriceNumber = whiskyPriceNumber;
            WhiskyId = whiskyId ?? throw new WhiskyRecordingDomainException("Parameter not provided:" + nameof(whiskyId));
            Price = price;
            _currencyId = currencyId;
            _priceReferenceId = priceReferenceId;
            Seller = seller ?? throw new WhiskyRecordingDomainException("Parameter not provided:" + nameof(seller));
            PriceDate = priceDate;
        }

        public void  Update(decimal price, int currencyId, int priceReferenceId, string seller, DateTime priceDate) 
        {
            Price = price;
            _currencyId = currencyId;
            _priceReferenceId = priceReferenceId;
            Seller = seller ?? throw new WhiskyRecordingDomainException("Parameter not provided:" + nameof(seller));
            PriceDate = priceDate;
        }
    }
}
