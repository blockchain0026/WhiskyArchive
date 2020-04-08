using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebMVC.Infrastructure;
using WebMVC.Models;
using WebMVC.ViewModels;

namespace WebMVC.Services
{
    public class WhiskyRecordingService : IWhiskyRecordingService
    {
        private HttpClient _httpClient;
        private readonly string _remoteServiceBaseUrl;
        private readonly string _remoteServiceBaseUrlWithAuthentication;
        private readonly IOptions<AppSettings> _settings;

        public WhiskyRecordingService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings;

            _remoteServiceBaseUrl = $"{settings.Value.WhiskyArchiveUrl}/whiskyrecording-api";
            _remoteServiceBaseUrlWithAuthentication = $"{settings.Value.WhiskyArchiveUrl}/api/w";
        }


        public async Task<List<Whisky>> GetAllWhiskies()
        {
            var uri = API.WhiskyRecord.GetAllWhiskies(_remoteServiceBaseUrl);

            var responseString = await _httpClient.GetStringAsync(uri);
            var response = JsonConvert.DeserializeObject<List<Whisky>>(responseString);

            return response;
        }


        public async Task<PaginatedWhisky> GetWhiskies(int page, int take, string name, string distillery, string bottler, 
            string vintage, string bottled, int? statedAge, string caskType, string caskNumber, int? numberOfBottles, float? strength, int? size, string market)
        {
            var uri = API.WhiskyRecord.GetWhiskies(_remoteServiceBaseUrl,page,take,name,distillery,bottler,vintage,bottled,statedAge,caskType,caskNumber,numberOfBottles,strength,size,market);

            var responseString = await _httpClient.GetStringAsync(uri);

            var response = JsonConvert.DeserializeObject<PaginatedWhisky>(responseString);

            return response;
        }


        public async Task<Whisky> GetWhisky(string whiskyId)
        {
            var uri = API.WhiskyRecord.GetWhisky(_remoteServiceBaseUrl, whiskyId);
            var responseString = await _httpClient.GetStringAsync(uri);
            var response = JsonConvert.DeserializeObject<Whisky>(responseString);

            return response;
        }
        public async Task<IEnumerable<SelectListItem>> GetCurrencies()
        {

            var items = new List<SelectListItem>();

            items.Add(new SelectListItem()
            {
                Value = "1",
                Text = "GBP"
            });
            items.Add(new SelectListItem()
            {
                Value = "2",
                Text = "EUR"
            });
            items.Add(new SelectListItem()
            {
                Value = "3",
                Text = "USD"
            });
            items.Add(new SelectListItem()
            {
                Value = "4",
                Text = "TWD",
                Selected=true
            });
            items.Add(new SelectListItem()
            {
                Value = "5",
                Text = "RMB"
            });
            items.Add(new SelectListItem()
            {
                Value = "6",
                Text = "YEN"
            });



            return items;
        }

        public async Task<IEnumerable<SelectListItem>> GetPriceReferences()
        {
            var items = new List<SelectListItem>();

            items.Add(new SelectListItem()
            {
                Value = "1",
                Text = "FacebookPublic"
            });
            items.Add(new SelectListItem()
            {
                Value = "2",
                Text = "FacebookPrivate"
            });
            items.Add(new SelectListItem()
            {
                Value = "3",
                Text = "FacebookAuction"
            });
            items.Add(new SelectListItem()
            {
                Value = "4",
                Text = "EuropeanAuction"
            });
            items.Add(new SelectListItem()
            {
                Value = "5",
                Text = "ChineseAuction"
            });
            items.Add(new SelectListItem()
            {
                Value = "6",
                Text = "JapaneseAuction"
            });
            items.Add(new SelectListItem()
            {
                Value = "7",
                Text = "Other",
                Selected = true
            });


            return items;
        }

        public async Task<IEnumerable<SelectListItem>> GetDistilleries()
        {
            var uri = API.WhiskyRecord.GetAllDistilleries(_remoteServiceBaseUrl);

            var responseString = await _httpClient.GetStringAsync(uri);

            var items = new List<SelectListItem>();

            items.Add(new SelectListItem() { Value = null, Text = "All", Selected = true });

            var distilleries = JArray.Parse(responseString);

            foreach (var distillery in distilleries.Children<JObject>())
            {
                items.Add(new SelectListItem()
                {
                    Value = distillery.Value<string>("distilleryNameCHT"),
                    Text = distillery.Value<string>("distilleryNameCHT")
                });
            }

            return items;
        }

        public async Task CreateWhiskyRecord(ApplicationUser user, WhiskyDTO whisky)
        {
            var uri = API.WhiskyRecord.CreateWhiskyRecord(_remoteServiceBaseUrlWithAuthentication);
            var whiskyContent = new StringContent(JsonConvert.SerializeObject(whisky), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, whiskyContent);

            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateWhiskyRecord(ApplicationUser user, UpdatedWhiskyDTO whisky)
        {
            var uri = API.WhiskyRecord.UpdateWhiskyRecord(_remoteServiceBaseUrlWithAuthentication);
            var whiskyContent = new StringContent(JsonConvert.SerializeObject(whisky), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, whiskyContent);

            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateWhiskyPrice(ApplicationUser user, string whiskyId, WhiskyPriceDTO whiskyPrice)
        {
            var uri = API.WhiskyRecord.UpdateWhiskyPrice(_remoteServiceBaseUrlWithAuthentication);
            var whiskyPriceContent = new StringContent(JsonConvert.SerializeObject(whiskyPrice), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, whiskyPriceContent);

            response.EnsureSuccessStatusCode();
        }


        public async Task UpdateWhiskyImage(string whiskyId, WhiskyImageDTO whiskyImage)
        {
            var uri = API.WhiskyRecord.UpdateWhiskyImage(_remoteServiceBaseUrlWithAuthentication);
            var whiskyImageContent = new StringContent(JsonConvert.SerializeObject(whiskyImage), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, whiskyImageContent);

            response.EnsureSuccessStatusCode();
        }
    }
}
