using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models;
using WebMVC.ViewModels;

namespace WebMVC.Services
{
    public interface IWhiskyRecordingService
    {
        Task<List<Whisky>> GetAllWhiskies();
        Task<Whisky> GetWhisky(string whiskyId);
        Task<PaginatedWhisky> GetWhiskies(int page, int take,string name, string distillery, string bottler,
            string vintage, string bottled, int? statedAge, string caskType,
            string caskNumber, int? numberOfBottles, float? strength, int? size, string market);

        Task<IEnumerable<SelectListItem>> GetCurrencies();
        Task<IEnumerable<SelectListItem>> GetPriceReferences();
        Task<IEnumerable<SelectListItem>> GetDistilleries();

        Task CreateWhiskyRecord(ApplicationUser user, WhiskyDTO whisky);
        Task UpdateWhiskyRecord(ApplicationUser user, UpdatedWhiskyDTO whisky);
        Task UpdateWhiskyPrice(ApplicationUser user, string whiskyId,WhiskyPriceDTO whiskyPrice);
        Task UpdateWhiskyImage(string whiskyId, WhiskyImageDTO whiskyImage);
    }
}
