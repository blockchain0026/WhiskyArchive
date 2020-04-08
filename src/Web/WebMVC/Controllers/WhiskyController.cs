using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Polly.CircuitBreaker;
using WebMVC.Models;
using WebMVC.Services;
using WebMVC.ViewModels;
using WebMVC.ViewModels.Pagination;
using WebMVC.ViewModels.WhiskyViewModels;

namespace WebMVC.Controllers
{
    public class WhiskyController : Controller
    {
        private IWhiskyRecordingService _whiskyRecordingSvc;
        private readonly IIdentityParser<ApplicationUser> _appUserParser;
        private readonly IHostingEnvironment hostingEnvironment;

        public WhiskyController(IWhiskyRecordingService whiskyRecordingSvc, IIdentityParser<ApplicationUser> appUserParser, IHostingEnvironment environment)
        {
            this._whiskyRecordingSvc = whiskyRecordingSvc;
            this._appUserParser = appUserParser;
            hostingEnvironment = environment;
        }
        public async Task<IActionResult> Index(string Name, string Distillery, string Bottler,
            string Vintage, string Bottled, int? StatedAge, string CaskType,
            string CaskNumber, int? NumberOfBottles, float? Strength, int? Size, string Market, int? page, [FromQuery]string errorMsg)
        {
            var itemsPage = 12;
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
            ViewData["Page"] = "Whisky";

            return View(vm);
        }


        public async Task<IActionResult> Detail(string whiskyId)
        {
            //var user = _appUserParser.Parse(HttpContext.User);

            var whisky = await _whiskyRecordingSvc.GetWhisky(whiskyId);
            ViewData["Page"] = "Whisky";

            foreach (var price in whisky.WhiskyPrices)
            {
                price.Currencies = await _whiskyRecordingSvc.GetCurrencies();
                price.PriceReferences = await _whiskyRecordingSvc.GetPriceReferences();
            }
            return View(whisky);
        }


        [Authorize]
        public async Task<IActionResult> Create()
        {
            var vm = new CreateViewModel
            {
                Distilleries = await _whiskyRecordingSvc.GetDistilleries(),
                Bottler="官方"
            };
            vm.WhiskyPrices.Add(new WhiskyPrice
            {
                Currencies = await _whiskyRecordingSvc.GetCurrencies(),
                PriceReferences = await _whiskyRecordingSvc.GetPriceReferences(),
                PriceDateYear = DateTime.Now.ToLocalTime().Year,
                PriceDateMonth = DateTime.Now.ToLocalTime().Month,
                PriceDateDay = DateTime.Now.ToLocalTime().Day,
            });

            ViewData["Page"] = "Whisky";
            return View(vm);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = _appUserParser.Parse(HttpContext.User);

                    var whiskyPrice = model.WhiskyPrices.FirstOrDefault();
                    var imageUrls = new List<string>();


                    if (!string.IsNullOrEmpty(model.ImageOne))
                    {
                        imageUrls.Add(model.ImageOne);
                    }
                    if (!string.IsNullOrEmpty(model.ImageTwo))
                    {
                        imageUrls.Add(model.ImageTwo);
                    }
                    if (!string.IsNullOrEmpty(model.ImageThree))
                    {
                        imageUrls.Add(model.ImageThree);
                    }



                    var whiskyDTO = new WhiskyDTO
                    {
                        DistilleryName = model.Distillery,
                        WhiskyNameChinese = model.WhiskyNameChinese,
                        WhiskyNameEnglish = model.WhiskyNameEnglish,
                        WhiskyBottler = model.Bottler,
                        Vintage = model.Vintage,
                        Bottled = model.Bottled,
                        StatedAge = model.StatedAge,
                        CaskType = model.CaskType,
                        CaskNumber = model.CaskNumber,
                        NumberOfBottles = model.NumOfBottles,
                        Strength = model.Strength / 100,
                        Size = model.Size,
                        Market = model.Market,
                        Rating = model.WhiskyBaseRating,
                        Notes = model.Notes,

                        Price = whiskyPrice?.Price,
                        CurrencyId = int.Parse(whiskyPrice?.Currency),
                        PriceReferenceId = int.Parse(whiskyPrice?.PriceReference),
                        Seller = whiskyPrice?.Seller,
                        PriceDateYear = whiskyPrice?.PriceDateYear,
                        PriceDateMonth = whiskyPrice?.PriceDateMonth,
                        PriceDateDay = whiskyPrice?.PriceDateDay,

                        ImageUrls = imageUrls,

                        RequestId = model.RequestId
                    };
                    await _whiskyRecordingSvc.CreateWhiskyRecord(user, whiskyDTO);



                    ViewData["Page"] = "Whisky";
                    //Redirect to historic list.
                    return RedirectToAction("Index");
                }
            }
            catch (BrokenCircuitException)
            {
                ModelState.AddModelError("Error", "It was not possible to create a new whisky, please try later on. (Business Msg Due to Circuit-Breaker)");
            }

            ViewData["Page"] = "Whisky";

            model.Distilleries = await _whiskyRecordingSvc.GetDistilleries();
            foreach (var price in model.WhiskyPrices)
            {
                price.Currencies = await _whiskyRecordingSvc.GetCurrencies();
                price.PriceReferences = await _whiskyRecordingSvc.GetPriceReferences();
            }


            return View("Create", model);
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Update(string whiskyId)
        {
            var user = _appUserParser.Parse(HttpContext.User);
            var whisky = await _whiskyRecordingSvc.GetWhisky(whiskyId);

            if (whisky == null)
            {
                return BadRequest("No whisky found.");
            }

            var vm = new UpdateViewModel
            {
                WhiskyId = whiskyId,
                WhiskyNameChinese = whisky.WhiskyNameChinese,
                WhiskyNameEnglish = whisky.WhiskyNameEnglish,
                Distillery = whisky.Distillery,
                Distilleries = await _whiskyRecordingSvc.GetDistilleries(),
                Bottler = whisky.Bottler,
                Vintage = whisky.Vintage,
                Bottled = whisky.Bottled,
                StatedAge = whisky.StatedAge,
                CaskType = whisky.CaskType,
                CaskNumber = whisky.CaskNumber,
                NumOfBottles = whisky.NumOfBottles,
                Strength = (int)whisky.Strength * 100,
                Size = whisky.Size,
                Market = whisky.Market,
                WhiskyBaseRating = whisky.WhiskyBaseRating,
                Notes = whisky.Notes
            };

            foreach (var price in whisky.WhiskyPrices)
            {
                vm.WhiskyPrices.Add(new WhiskyPrice
                {
                    WhiskyPriceNumber = price.WhiskyPriceNumber,
                    WhiskyId = price.WhiskyId,
                    Price = price.Price,
                    Currency = price.Currency,
                    Currencies = await _whiskyRecordingSvc.GetCurrencies(),
                    PriceReference = price.PriceReference,
                    PriceReferences = await _whiskyRecordingSvc.GetPriceReferences(),
                    Seller = price.Seller,
                    PriceDateDay = price.PriceDateDay,
                    PriceDateMonth = price.PriceDateMonth,
                    PriceDateYear = price.PriceDateYear
                });
            }


            /*foreach (var url in whisky.WhiskyImages)
            {
                vm.ExistingImageUrls.Add(url);
            }*/

            if (whisky.WhiskyImages.ElementAtOrDefault(0) != null)
            {
                vm.ImageOne = whisky.WhiskyImages[0];
            }
            if (whisky.WhiskyImages.ElementAtOrDefault(1) != null)
            {
                vm.ImageTwo = whisky.WhiskyImages[1];
            }
            if (whisky.WhiskyImages.ElementAtOrDefault(2) != null)
            {
                vm.ImageThree = whisky.WhiskyImages[2];
            }


            ViewData["Page"] = "Whisky";
            return View(vm);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Update(UpdateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = _appUserParser.Parse(HttpContext.User);

                    var updatedWhiskyDTO = new UpdatedWhiskyDTO
                    {
                        WhiskyId = model.WhiskyId,
                        DistilleryName = model.Distillery,
                        WhiskyNameChinese = model.WhiskyNameChinese,
                        WhiskyNameEnglish = model.WhiskyNameEnglish,
                        WhiskyBottler = model.Bottler,
                        Vintage = model.Vintage,
                        Bottled = model.Bottled,
                        StatedAge = model.StatedAge,
                        CaskType = model.CaskType,
                        CaskNumber = model.CaskNumber,
                        NumberOfBottles = model.NumOfBottles,
                        Strength = model.Strength / 100,
                        Size = model.Size,
                        Market = model.Market,
                        Rating = model.WhiskyBaseRating,
                        Notes = model.Notes,

                        RequestId = model.RequestId
                    };
                    await _whiskyRecordingSvc.UpdateWhiskyRecord(user, updatedWhiskyDTO);

                    foreach (var price in model.WhiskyPrices)
                    {
                        var whiskyPriceDTO = new WhiskyPriceDTO
                        {
                            WhiskyId = model.WhiskyId,
                            Price = price.Price,
                            CurrencyId = int.Parse(price.Currency),
                            PriceReferenceId = int.Parse(price.PriceReference),
                            PriceDateDay = price.PriceDateDay,
                            PriceDateMonth = price.PriceDateMonth,
                            PriceDateYear = price.PriceDateYear,
                            Seller = price.Seller,
                            PriceNumber = price.WhiskyPriceNumber
                        };

                        await _whiskyRecordingSvc.UpdateWhiskyPrice(user, model.WhiskyId, whiskyPriceDTO);
                    }


                    var urls = new List<string>();


                    if (!string.IsNullOrEmpty(model.ImageOne))
                    {
                        urls.Add(model.ImageOne);
                    }
                    if (!string.IsNullOrEmpty(model.ImageTwo))
                    {
                        urls.Add(model.ImageTwo);
                    }
                    if (!string.IsNullOrEmpty(model.ImageThree))
                    {
                        urls.Add(model.ImageThree);
                    }




                    var whiskyImageDTO = new WhiskyImageDTO
                    {
                        WhiskyId = model.WhiskyId,
                        Urls = urls
                    };

                    await _whiskyRecordingSvc.UpdateWhiskyImage(model.WhiskyId, whiskyImageDTO);


                    ViewData["Page"] = "Whisky";
                    //Redirect to historic list.
                    return RedirectToAction("Index");
                }
            }
            catch (BrokenCircuitException)
            {
                ModelState.AddModelError("Error", "It was not possible to update a new whisky, please try later on. (Business Msg Due to Circuit-Breaker)");
            }
            ViewData["Page"] = "Whisky";

            model.Distilleries = await _whiskyRecordingSvc.GetDistilleries();
            foreach (var price in model.WhiskyPrices)
            {
                price.Currencies = await _whiskyRecordingSvc.GetCurrencies();
                price.PriceReferences = await _whiskyRecordingSvc.GetPriceReferences();
            }

            return View("Update", model);
        }



        public JsonResult UploadFile()
        {
            uploadResult result = new uploadResult();
            try
            {

                var formFile = Request.Form.Files["txt_file"];

                long size = formFile.Length;

                // full path to file in temp location
                var filePath = Path.GetTempFileName();


                if (formFile.Length > 0)
                {
                    var uploads = Path.Combine(hostingEnvironment.WebRootPath, "images/whiskies");
                    Console.Write("Image Uploaded To Path: " + uploads);
                    var fileNmae = GetUniqueFileName(formFile.FileName);
                    var fullPath = Path.Combine(uploads, fileNmae);
                    formFile.CopyTo(new FileStream(fullPath, FileMode.Create));



                    /*using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }*/
                    result.fileName = fileNmae;
                }


            }
            catch (Exception ex)
            {
                result.error = ex.Message;
            }
            return Json(result);
        }
        public class uploadResult
        {
            public string fileName { get; set; }
            public string error { get; set; }
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }
    }
}