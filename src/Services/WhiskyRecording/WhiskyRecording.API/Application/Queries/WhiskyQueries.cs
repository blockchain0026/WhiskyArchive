using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhiskyArchive.Services.WhiskyRecording.Domain.Model.Distilleries;
using WhiskyArchive.Services.WhiskyRecording.Domain.Model.Whiskys;

namespace WhiskyArchive.Services.WhiskyRecording.API.Application.Queries
{
    public class WhiskyQueries : IWhiskyQueries
    {
        private readonly IWhiskyRepository _whiskyRepository;
        private readonly IDistilleryRepository _distilleryRepository;


        public WhiskyQueries(IWhiskyRepository whiskyRepository, IDistilleryRepository distilleryRepository)
        {
            _whiskyRepository = whiskyRepository ?? throw new ArgumentNullException(nameof(whiskyRepository));
            _distilleryRepository = distilleryRepository ?? throw new ArgumentNullException(nameof(distilleryRepository));
        }

        public async Task<Whisky> GetWhiskyAsync(string whiskyId)
        {

            var whisky = await _whiskyRepository.GetByWhiskyIdAsync(whiskyId);

            if (whisky == null)
            {
                throw new KeyNotFoundException();
            }

            return await MapWhisky(whisky);


        }


        public async Task<List<Whisky>> GetAllWhiskiesAsync()
        {
            var whiskiesModel = new List<Whisky>();
            var whiskies = await _whiskyRepository.GetAllAsync();

            foreach (var whisky in whiskies)
            {
                whiskiesModel.Add(await MapWhisky(whisky));
            }

            return whiskiesModel;
        }


        public async Task<Whisky> MapWhisky(Domain.Model.Whiskys.Whisky whisky)
        {
            var whiskyPrices = new List<WhiskyPrice>();

            foreach (var whiskyPrice in whisky.WhiskyPrices)
            {
                whiskyPrices.Add(new WhiskyPrice
                {
                    WhiskyPriceNumber = whiskyPrice.WhiskyPriceNumber,
                    WhiskyId = whiskyPrice.WhiskyId,
                    Price = whiskyPrice.Price,
                    Currency = whiskyPrice.Currency.Id.ToString(),
                    PriceReference = whiskyPrice.PriceReference.Id.ToString(),
                    Seller = whiskyPrice.Seller,
                    PriceDateDay = whiskyPrice.PriceDate.Day,
                    PriceDateMonth = whiskyPrice.PriceDate.Month,
                    PriceDateYear = whiskyPrice.PriceDate.Year,
                });
            }

            var whiskyImages = new List<string>();

            foreach (var image in whisky.WhiskyImages)
            {
                whiskyImages.Add(image.ImageUrl);
            }

            var distillery = await _distilleryRepository.GetByDistilleryIdAsync(whisky.DistilleryId);
            var whiskyDetail = whisky.WhiskyDetail;

            var whiskyModel = new Whisky
            {
                WhiskyId = whisky.WhiskyId,
                WhiskyNameChinese = whisky.WhiskyName.Chinese,
                WhiskyNameEnglish = whisky.WhiskyName.English,
                Distillery = distillery.DistilleryName.ChineseTraditional,
                Bottler = whisky.Bottler,
                Vintage = whiskyDetail.Vintage,
                Bottled = whiskyDetail.Bottled,
                StatedAge = whiskyDetail.StatedAge,
                CaskType = whiskyDetail.CaskType,
                CaskNumber=whiskyDetail.CaskNumber,
                NumOfBottles = whiskyDetail.NumOfBottles,
                Strength = whiskyDetail.Strength,
                Size = whiskyDetail.Size,
                Market = whiskyDetail.Market,
                WhiskyBaseRating = whisky.WhiskyBaseRating,
                Notes = whisky.Notes,
                DateUpdated = whisky.DateUpdated,
                WhiskyPrices = whiskyPrices,
                WhiskyImages=whiskyImages
            };


            return whiskyModel;
        }


        public Distillery MapDistillery(Domain.Model.Distilleries.Distillery distillery)
        {

            var distilleryModel = new Distillery
            {
                DistilleryId = distillery.DistilleryId,
                DistilleryNameCHT = distillery.DistilleryName.ChineseTraditional,
                DistilleryNameCHS = distillery.DistilleryName.ChineseSimplified,
                DistilleryNameEN = distillery.DistilleryName.English,
                Established = distillery.Established,
                Introdution = distillery.Introdution,
                SmwsCode = distillery.SmwsCode,
            };


            return distilleryModel;
        }

        public async Task<List<Distillery>> GetAllDistilleriesAsync()
        {
            var distilleriesModel = new List<Distillery>();
            var distilleries = await _distilleryRepository.GetAllAsync();

            foreach (var distillery in distilleries)
            {
                distilleriesModel.Add(MapDistillery(distillery));
            }

            return distilleriesModel;
        }
    }
}
