using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.ViewModels;

namespace WebMVC.Services
{

    public interface ICalculatingService
    {

        Task<IEnumerable<SelectListItem>> GetAuctions();

        Task<CalculationResult> Calculate(ApplicationUser user,string auctionName, decimal winPrice, decimal strength, int size, int winLots,bool insurance, decimal exchangeRate, bool useAdvancePayment );

    }
}
