using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using WebMVC.ViewModels;

namespace WebMVC.Services
{
    public class CalculatingService : ICalculatingService
    {
        private HttpClient _httpClient;
        private readonly string _remoteServiceBaseUrl;
        private readonly string _remoteServiceBaseUrlWithAuthentication;
        private readonly IOptions<AppSettings> _settings;

        public CalculatingService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings;

            _remoteServiceBaseUrl = $"{settings.Value.WhiskyArchiveUrl}/whiskyrecording-api";
            _remoteServiceBaseUrlWithAuthentication = $"{settings.Value.WhiskyArchiveUrl}/api/w";
        }

        public async Task<CalculationResult> Calculate(ApplicationUser user, string auctionName,
            decimal winPrice, decimal strength, int size, int winLots, bool insurance, decimal exchangeRate, bool useAdvancePayment)
        {
            Auction auction;

            decimal commission = 0;
            decimal deliveryFee = 0;
            decimal insuranceFee = 0;
            decimal surcharge = 0;
            decimal VAT = 0;
            decimal customs = 0;
            decimal tobaccoAlcoholTax = 0;
            decimal businessTax = 0;
            decimal advancePayment = 0;
            decimal total = 0;

            if (auctionName == "Whisky Auctioneer")
            {
                auction = new Auction
                {
                    Commission = 0.1M,
                    Insurance = 0.03M,
                    Surcharge = 0.03M,
                    VAT = 0
                };

                commission = Math.Round(winPrice * auction.Commission, 0);
                deliveryFee = (40 + ((winLots - 1) * 10)) / winLots;
                insuranceFee = insurance ? winPrice * 0.03M : 0;
                surcharge = (winPrice + commission + deliveryFee + insuranceFee) * auction.Surcharge;
                VAT = 0;
                customs = 0;
                tobaccoAlcoholTax = Math.Round((strength * 100 * 2.5M) * (size / 1000M), 0);
                businessTax = Math.Round(((winPrice * exchangeRate) + customs + tobaccoAlcoholTax) * 0.05M, 0);
                advancePayment = useAdvancePayment ? 420M : 0;
            }

            if (auctionName == "Scotch Whisky Auctions")
            {
                auction = new Auction
                {
                    Commission = 0.1M,
                    Insurance = 0.03M,
                    Surcharge = 0.03M,
                    VAT = 0
                };

                commission = Math.Round(winPrice * auction.Commission, 0);
                deliveryFee = (45 + ((winLots - 1) * 10)) / winLots;
                insuranceFee = insurance ? winPrice * 0.03M : 0;
                surcharge = (winPrice + commission + deliveryFee + insuranceFee) * auction.Surcharge;
                VAT = 0;
                customs = 0;
                tobaccoAlcoholTax = Math.Round((strength * 100 * 2.5M) * (size / 1000M), 0);
                businessTax = Math.Round((((winPrice + 7.64M) * exchangeRate) + customs + tobaccoAlcoholTax) * 0.05M, 0);
                advancePayment = useAdvancePayment ? 420M : 0;
            }

            total = ((winPrice + commission + deliveryFee + insuranceFee + surcharge + VAT) * exchangeRate)
                + customs + tobaccoAlcoholTax + businessTax + advancePayment;

            return new CalculationResult
            {
                WinPrice = winPrice,
                Commission = commission,
                DeliveryFee = deliveryFee,
                Insurance = insuranceFee,
                PaymentSurcharge = surcharge,
                VAT = VAT,
                Customs = customs,
                TobaccoAlcoholTax = tobaccoAlcoholTax,
                BusinessTax = businessTax,
                AdvancePayment = advancePayment,
                Total = total
            };
        }

        public async Task<IEnumerable<SelectListItem>> GetAuctions()
        {
            var items = new List<SelectListItem>();

            items.Add(new SelectListItem()
            {
                Value = null,
                Text = "All",
                Selected = true
            });

            items.Add(new SelectListItem()
            {
                Value = "1",
                Text = "Whisky Auctioneer"
            });
            items.Add(new SelectListItem()
            {
                Value = "2",
                Text = "Scotch Whisky Auctions"
            });
            /*items.Add(new SelectListItem()
            {
                Value = "3",
                Text = "WhiskyAuction.Com"
            });*/

            return items;
        }

        private class Auction
        {
            public decimal Commission { get; set; }
            public decimal Insurance { get; set; }
            public decimal Surcharge { get; set; }
            public decimal VAT { get; set; }
        }
    }

}
