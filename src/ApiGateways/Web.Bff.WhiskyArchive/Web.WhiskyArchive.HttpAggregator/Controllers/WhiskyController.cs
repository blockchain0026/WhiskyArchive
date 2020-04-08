using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhiskyArchive.Web.WhiskyArchive.HttpAggregator.Services;

namespace Web.WhiskyArchive.HttpAggregator.Controllers
{

    [Route("api/[controller]")]
    
    public class WhiskyController : Controller
    {
        private readonly IWhiskyRecordingApiClient _whiskyRecordingClient;
        public WhiskyController(IWhiskyRecordingApiClient whiskyRecordingClient)
        {
            _whiskyRecordingClient = whiskyRecordingClient;
        }

        /*[Route("draft/{basketId}")]
        [HttpGet]
        public async Task<IActionResult> GetOrderDraft(string basketId)
        {
            if (string.IsNullOrEmpty(basketId))
            {
                return BadRequest("Need a valid basketid");
            }
            // Get the basket data and build a order draft based on it
            var basket = await _basketService.GetById(basketId);
            if (basket == null)
            {
                return BadRequest($"No basket found for id {basketId}");
            }

            var orderDraft = await _orderClient.GetOrderDraftFromBasket(basket);
            return Ok(orderDraft);
        }*/
    }
}