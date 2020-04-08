using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiskyArchive.Services.WhiskyRecording.Domain.Events;
using WhiskyArchive.Services.WhiskyRecording.Domain.Exceptions;
using WhiskyArchive.Services.WhiskyRecording.Domain.Model.Distilleries;
using WhiskyArchive.Services.WhiskyRecording.Domain.SeedWork;

namespace WhiskyArchive.Services.WhiskyRecording.Domain.Model.Whiskys
{
    public class Whisky : Entity, IAggregateRoot
    {
        public string WhiskyId { get; private set; }
        public WhiskyName WhiskyName { get; private set; }
        public string DistilleryId { get; private set; }
        public string Bottler { get; private set; }
        public WhiskyDetail WhiskyDetail { get; private set; }

        public float WhiskyBaseRating { get; private set; }
        public string Notes { get; private set; }
        public DateTime DateUpdated { get; private set; }


        private readonly List<WhiskyPrice> _whiskyPrices;
        public IReadOnlyCollection<WhiskyPrice> WhiskyPrices => _whiskyPrices;

        private readonly List<WhiskyImage> _whiskyImages;
        public IReadOnlyCollection<WhiskyImage> WhiskyImages => _whiskyImages;


        protected Whisky()
        {
            this._whiskyPrices = new List<WhiskyPrice>();
            this._whiskyImages = new List<WhiskyImage>();
        }

        public Whisky(WhiskyName whiskyName, string distilleryId, string bottler, WhiskyDetail whiskyDetail, float whiskyBaseRating, string notes) : this()
        {
            this.WhiskyId = Guid.NewGuid().ToString();
            this.WhiskyName = whiskyName;
            this.DistilleryId = distilleryId ?? throw new WhiskyRecordingDomainException("The distilleryId is not provided when create new Whisky.");
            this.Bottler = bottler ?? throw new WhiskyRecordingDomainException("The bottler is not provided when creating new Whisky.");
            this.WhiskyDetail = whiskyDetail ?? throw new WhiskyRecordingDomainException("The whiskyDetail is not provided when create new Whisky.");
            this.WhiskyBaseRating = whiskyBaseRating;
            this.Notes = notes ?? throw new WhiskyRecordingDomainException("The notes is not provided when creating new Whisky.");

            this.DateUpdated = DateTime.UtcNow;

            this.AddDomainEvent(
                new WhiskyCreatedDomainEvent(this));
        }


        #region Functions

        public static Whisky From(Distillery distillery, string whiskyNameChinese, string whiskyNameEnglish = null, string whiskyBottler = null,
            string vintage = null, string bottled = null, int? statedAge = null, string caskType = null, string caskNumber = null, int? numberOfBottles = null, float? strength = null, int? size = null, string market = null,
            float? rating = null, string notes = null
            /*decimal? price = null, string currency = null, int referenceId = null, string seller = null, DateTime? priceDate = null*/)
        {
            if (distillery == null || String.IsNullOrEmpty(distillery.DistilleryId))
            {
                throw new WhiskyRecordingDomainException("Distillery must be provided when creating Whisky.");
            }
            if (whiskyNameChinese == null)
            {
                throw new WhiskyRecordingDomainException("WhiskyNameChinese must be provided when creating Whisky.");
            }

            if (statedAge != null)
            {
                if (statedAge == 0)
                {
                    statedAge = null;
                }
            }
            if (numberOfBottles != null)
            {
                if (numberOfBottles == 0)
                {
                    numberOfBottles = null;
                }
            }
            var whiskyName = new WhiskyName(whiskyNameChinese, whiskyNameEnglish ?? String.Empty);
            var distilleryId = distillery.DistilleryId;
            var bottler = whiskyBottler ?? String.Empty;
            var whiskyDetail = new WhiskyDetail(
                vintage ?? String.Empty,
                bottled ?? String.Empty,
                statedAge,
                caskType ?? String.Empty,
                caskNumber ?? String.Empty,
                numberOfBottles,
                strength ?? 0,
                size ?? 0,
                market ?? String.Empty
                );


            return new Whisky(
                whiskyName,
                distilleryId,
                bottler,
                whiskyDetail,
                rating ?? 0,
                notes ?? String.Empty
                );
        }

        public void UpdateName(string chinese = null, string english = null)
        {
            if (chinese == null & english == null)
            {
                throw new WhiskyRecordingDomainException("Chinese or english must be provided.");
            }

            this.WhiskyName = this.WhiskyName.Update(chinese, english);

            this.DataUpdated();
        }

        public void UpdateBottler(string bottlerName)
        {
            this.Bottler = bottlerName ?? throw new WhiskyRecordingDomainException("The bottlerName must be provided.");

            this.DataUpdated();
        }

        public void UpdateDetail(string vintage = null, string bottled = null, int? statedAge = null, string caskType = null,
            string caskNumber = null, int? numberOfBottles = null, float? strength = null, int? size = null, string market = null)
        {
            this.WhiskyDetail = this.WhiskyDetail.DetailUpdated(vintage, bottled, statedAge, caskType, caskNumber, numberOfBottles, strength, size, market);

            this.DataUpdated();
        }

        public void UpdatePrice(decimal price, int currencyId, int referenceId, DateTime priceDate, string seller = null,int? priceNumber = null)
        {
            if (priceNumber != null)
            {
                var whiskyPrice = this._whiskyPrices.Where(w => w.WhiskyPriceNumber == priceNumber).SingleOrDefault();
                if (whiskyPrice == null)
                {
                    throw new WhiskyRecordingDomainException("Whisky Price Record Not Found With Number "+ priceNumber);
                }

                whiskyPrice.Update(price, currencyId, referenceId, seller ?? string.Empty, priceDate);
            }
            else
            {
                var priceNum = _whiskyPrices.Count;
                var whiskyPrice = new WhiskyPrice(
                    priceNum,
                    this.WhiskyId,
                    price,
                    currencyId,
                    referenceId,
                    seller ?? String.Empty,
                    priceDate
                    );

                this._whiskyPrices.Add(whiskyPrice);
            }

            this.DataUpdated();
        }

        public void UpdateImages(List<string> urls)
        {
            //Clear
            this._whiskyImages.Clear();

            //Re Add
            foreach (var url in urls)
            {
                var imageNum = this._whiskyImages.Count + 1;
                var newImage = new WhiskyImage(imageNum, this.WhiskyId, url, string.Empty);
                this._whiskyImages.Add(newImage);
            }

            this.DataUpdated();
        }

        public void UpdateImageDescription(int imageNum, string description)
        {
            var image = this._whiskyImages.Where(i => i.WhiskyImageNumber == imageNum).SingleOrDefault();
            if (image == null)
            {
                throw new WhiskyRecordingDomainException($"The image was not found with ImageNum {imageNum}.");
            }
            image.UpdateDescription(description);

            this.DataUpdated();
        }

        public void DeleteImage(int imageNum)
        {
            var image = this._whiskyImages.Where(i => i.WhiskyImageNumber == imageNum).SingleOrDefault();
            if (image == null)
            {
                throw new WhiskyRecordingDomainException($"The image was not found with ImageNum {imageNum}.");
            }

            this._whiskyImages.Remove(image);

            this.DataUpdated();
        }

        public void UpdateNotes(string notes)
        {
            this.Notes = notes ?? string.Empty;

            this.DataUpdated();
        }

        #endregion


        #region Private Functions

        private void DataUpdated()
        {
            this.DateUpdated = DateTime.UtcNow;
        }

        #endregion
    }
}
