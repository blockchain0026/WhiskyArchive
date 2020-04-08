using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Models;
using WebMVC.Services;
using WebMVC.ViewModels;
using WebMVC.ViewModels.Pagination;
using WebMVC.ViewModels.WhiskyViewModels;

namespace WebMVC.Controllers
{
    public class HomeController : Controller
    {
        private IWhiskyRecordingService _whiskyRecordingSvc;
        private readonly IIdentityParser<ApplicationUser> _appUserParser;

        public HomeController(IWhiskyRecordingService whiskyRecordingSvc, IIdentityParser<ApplicationUser> appUserParser)
        {
            this._whiskyRecordingSvc = whiskyRecordingSvc;
            this._appUserParser = appUserParser;

            
        }

        public async Task<IActionResult> Index(string Name, string Distillery, string Bottler,
         string Vintage, string Bottled, int? StatedAge, string CaskType,
         string CaskNumber, int? NumberOfBottles, float? Strength, int? Size, string Market, int? page, [FromQuery]string errorMsg)
        {
            var itemsPage = 10;
            var paginatedWhisky = await _whiskyRecordingSvc.GetWhiskies(page ?? 0, itemsPage,
                Name, Distillery == "All" ? string.Empty : Distillery, Bottler, Vintage, Bottled, StatedAge, CaskType, CaskNumber, NumberOfBottles, Strength, Size, Market);
            var vm = new IndexViewModel()
            {
                Whiskies = paginatedWhisky.Data,
                Distilleries = await _whiskyRecordingSvc.GetDistilleries(),
                Distillery = Distillery ?? "All",
                Name = Name,
                Bottler = Bottler,
                Vintage = Vintage,
                Bottled = Bottled,
                StatedAge = StatedAge,
                CaskType = CaskType,
                CaskNumber = CaskNumber,
                NumberOfBottles = NumberOfBottles,
                Strength = Strength,
                Size = Size,
                Market = Market,
                PaginationInfo = new PaginationInfo()
                {
                    ActualPage = page ?? 0,
                    ItemsPerPage = paginatedWhisky.Data.Count,
                    TotalItems = paginatedWhisky.Count,
                    TotalPages = (int)Math.Ceiling(((decimal)paginatedWhisky.Count / itemsPage))
                }
            };

            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";

            ViewBag.BasketInoperativeMsg = errorMsg;
            ViewData["Page"] = "Home";
            return View(vm);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
