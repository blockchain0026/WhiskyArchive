using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WhiskyArchive.Services.Identity.API;
using WhiskyArchive.Services.Identity.API.Models;
using WhiskyArchive.Services.Identity.API.Services;

namespace Identity.API.Controllers
{
    public class HomeController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IOptionsSnapshot<AppSettings> _settings;
        private readonly IRedirectService _redirectSvc;
        private readonly ILoginService<ApplicationUser> _loginService;

        public HomeController(IIdentityServerInteractionService interaction, IOptionsSnapshot<AppSettings> settings, IRedirectService redirectSvc, ILoginService<ApplicationUser> loginService)
        {
            _interaction = interaction;
            _settings = settings;
            _redirectSvc = redirectSvc;
            _loginService = loginService;
        }

        public IActionResult Index(string returnUrl)
        {
            bool val1 = (HttpContext.User != null) && HttpContext.User.Identity.IsAuthenticated;
            return View();
            //return new RedirectResult("~/api/docs");
        }

        public IActionResult ReturnToOriginalApplication(string returnUrl)
        {
            if (returnUrl != null)
                return Redirect(_redirectSvc.ExtractRedirectUriFromReturnUrl(returnUrl));
            else
                return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Shows the error page
        /// </summary>
        public async Task<IActionResult> Error(string errorId)
        {
            var vm = new ErrorViewModel();

            // retrieve error details from identityserver
            var message = await _interaction.GetErrorContextAsync(errorId);
            if (message != null)
            {
                vm.Error = message;
                Console.WriteLine(vm.Error);
            }
            
            return View("Error", vm);
        }
    }
}