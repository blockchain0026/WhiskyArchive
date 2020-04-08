using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polly.CircuitBreaker;
using WebMVC.Services;
using WebMVC.ViewModels;
using WebMVC.ViewModels.CalculationViewModels;

namespace WebMVC.Controllers
{

    public class CalculationController : Controller
    {
        private ICalculatingService _calculatingSvc;
        private readonly IIdentityParser<ApplicationUser> _appUserParser;

        public CalculationController(ICalculatingService calculatingSvc, IIdentityParser<ApplicationUser> appUserParser)
        {
            this._calculatingSvc = calculatingSvc;
            this._appUserParser = appUserParser;
        }

        [HttpGet]
        public async Task<IActionResult> Index(IndexViewModel model, [FromQuery]string errorMsg, bool isResult = false)
        {
            if (isResult)
            {
                ViewBag.BasketInoperativeMsg = errorMsg;
                ViewData["Page"] = "Calculation";

                return View(model);
            }
            var vm = new IndexViewModel
            {
                WinPrice = 0,
                Strength = 0.43M,
                Size = 700,
                WinLots = 1,
                Insurance = false,
                ExchangeRate = 41,
                UseAdvancePayment = false,
                Auctions = await _calculatingSvc.GetAuctions()
            };


            ViewBag.BasketInoperativeMsg = errorMsg;
            ViewData["Page"] = "Calculation";

            return View(vm);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Calculate(IndexViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = _appUserParser.Parse(HttpContext.User);

                    model.Auctions = await _calculatingSvc.GetAuctions();
                    model.CalculationResults.Clear();

                    if (model.Auction == "All")
                    {
                        foreach (var auction in model.Auctions)
                        {
                            if (string.IsNullOrEmpty(auction.Value))
                                continue;

                            var result = await _calculatingSvc.Calculate(user,
                                auction.Text,
                                model.WinPrice,
                                model.Strength,
                                model.Size,
                                model.WinLots,
                                model.Insurance,
                                model.ExchangeRate,
                                model.UseAdvancePayment
                                );

                            model.CalculationResults.Add(result);
                        }
                    }
                    else
                    {
                        var result = await _calculatingSvc.Calculate(user,
                            model.Auction,
                            model.WinPrice,
                            model.Strength,
                            model.Size,
                            model.WinLots,
                            model.Insurance,
                            model.ExchangeRate,
                            model.UseAdvancePayment
                            );

                        model.CalculationResults.Add(result);
                    }


                    ViewData["Page"] = "Calculation";
                    //Redirect to historic list.
                    return View("Index", model);
                }
            }
            catch (BrokenCircuitException)
            {
                ModelState.AddModelError("Error", "It was not possible to create a new whisky, please try later on. (Business Msg Due to Circuit-Breaker)");
            }

            ViewData["Page"] = "Calculation";

            model.CalculationResults.Clear();

            return View("Index", model);
        }
    }
}