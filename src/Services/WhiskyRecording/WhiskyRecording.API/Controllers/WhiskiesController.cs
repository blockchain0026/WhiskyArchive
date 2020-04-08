using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhiskyArchive.Services.WhiskyRecording.API.Application.Commands;
using WhiskyArchive.Services.WhiskyRecording.API.Application.Queries;
using WhiskyArchive.Services.WhiskyRecording.API.Infrasructure.Services;
using WhiskyArchive.Services.WhiskyRecording.API.ViewModel;
using WhiskyArchive.Services.WhiskyRecording.Domain.Model.Distilleries;
using WhiskyArchive.Services.WhiskyRecording.Domain.Model.Whiskys;
using WhiskyArchive.Services.WhiskyRecording.Infrastructure;

namespace WhiskyRecording.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WhiskiesController : ControllerBase
    {
        private readonly WhiskyRecordingContext _context;
        private readonly IWhiskyRepository _whiskyRepository;
        private readonly IDistilleryRepository _distilleryRepository;
        private readonly IWhiskyQueries _whiskyQueries;
        private readonly IMediator _mediator;

        public WhiskiesController(WhiskyRecordingContext context, IWhiskyRepository whiskyRepository, IDistilleryRepository distilleryRepository, IWhiskyQueries whiskyQueries, IMediator mediator)
        {
            _context = context;
            _whiskyRepository = whiskyRepository ?? throw new ArgumentNullException(nameof(whiskyRepository));
            _distilleryRepository = distilleryRepository ?? throw new ArgumentNullException(nameof(distilleryRepository));
            _whiskyQueries = whiskyQueries ?? throw new ArgumentNullException(nameof(whiskyQueries));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        // GET: api/getallwhiskies
        [Route("GetAllWhiskies")]
        [HttpGet]
        public async Task<IActionResult> GetAllWhiskies()
        {
            //var whiskies = await this._whiskyRepository.GetAllAsync();
            var whiskies = await this._whiskyQueries.GetAllWhiskiesAsync();
            return Ok(whiskies);
        }



        // GET: api/Whiskies/5
        [HttpGet("{whiskyId}")]
        public async Task<IActionResult> GetWhisky([FromRoute] string whiskyId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var whisky = await _whiskyRepository.GetByWhiskyIdAsync(whiskyId);
            var whisky = await _whiskyQueries.GetWhiskyAsync(whiskyId);

            if (whisky == null)
            {
                return NotFound();
            }

            return Ok(whisky);
        }



        // GET api/v1/[controller]/items[?pageSize=3&pageIndex=10]
        [HttpGet]
        [Route("items")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<WhiskyArchive.Services.WhiskyRecording.API.Application.Queries.Whisky>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<WhiskyArchive.Services.WhiskyRecording.API.Application.Queries.Whisky>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Items([FromQuery]int pageIndex = 0, [FromQuery]int pageSize = 10, [FromQuery]string name = null, [FromQuery]string distillery = null, [FromQuery]string bottler = null,
            [FromQuery]string vintage = null, [FromQuery]string bottled = null, [FromQuery]int? statedAge = null, [FromQuery]string caskType = null,
            [FromQuery]string caskNumber = null, [FromQuery]int? numberOfBottles = null, [FromQuery]float? strength = null, [FromQuery]int? size = null, [FromQuery]string market = null)
        {
            var root = (IQueryable<WhiskyArchive.Services.WhiskyRecording.Domain.Model.Whiskys.Whisky>)
                _context.Whiskys
                .Include(w => w.WhiskyPrices)
                .Include(w => w.WhiskyImages);

            var itemsOnPage = new List<WhiskyArchive.Services.WhiskyRecording.API.Application.Queries.Whisky>();

            #region filters
            if (!string.IsNullOrWhiteSpace(name))
            {
                root = root.Where(w => w.WhiskyName.Chinese.Contains(name) || w.WhiskyName.English.Contains(name));
            }
            if (!string.IsNullOrWhiteSpace(distillery))
            {
                var existingDistillery = await this._distilleryRepository.GetByDistilleryNameAsync(distillery);

                if (existingDistillery == null)
                {
                    return Ok(new PaginatedItemsViewModel<WhiskyArchive.Services.WhiskyRecording.API.Application.Queries.Whisky>(
                        pageIndex, pageSize, 0, itemsOnPage));
                }
                else
                {
                    root = root.Where(w => w.DistilleryId == existingDistillery.DistilleryId);
                }
            }
            if (!string.IsNullOrWhiteSpace(bottler))
            {
                root = root.Where(w => w.Bottler == bottler);
            }
            if (!string.IsNullOrWhiteSpace(vintage))
            {
                root = root.Where(w => w.WhiskyDetail.Vintage.Contains(vintage));
            }
            if (!string.IsNullOrWhiteSpace(bottled))
            {
                root = root.Where(w => w.WhiskyDetail.Bottled.Contains(bottled));
            }
            if (statedAge.HasValue)
            {
                root = root.Where(w => w.WhiskyDetail.StatedAge == statedAge);
            }
            if (!string.IsNullOrWhiteSpace(caskType))
            {
                root = root.Where(w => w.WhiskyDetail.CaskType.Contains(caskType));
            }
            if (!string.IsNullOrWhiteSpace(caskNumber))
            {
                root = root.Where(w => w.WhiskyDetail.CaskNumber.Contains(caskNumber));
            }
            if (numberOfBottles.HasValue)
            {
                root = root.Where(w => w.WhiskyDetail.NumOfBottles == numberOfBottles);
            }
            if (strength.HasValue)
            {
                root = root.Where(w => w.WhiskyDetail.Strength == strength);
            }
            if (size.HasValue)
            {
                root = root.Where(w => w.WhiskyDetail.Size == size);
            }
            if (!string.IsNullOrWhiteSpace(market))
            {
                root = root.Where(w => w.WhiskyDetail.Market.Contains(market));
            }
            #endregion



            var totalItems = await root.LongCountAsync();

            var whiskies = await root
                .OrderBy(c => c.WhiskyName.Chinese)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();


            foreach (var whisky in whiskies)
            {
                foreach (var price in whisky.WhiskyPrices)
                {
                    await _context.Entry(price)
                        .Reference(p => p.Currency).LoadAsync();
                    await _context.Entry(price)
                        .Reference(p => p.PriceReference).LoadAsync();
                }
                itemsOnPage.Add(await _whiskyQueries.MapWhisky(whisky));
            }

            var model = new PaginatedItemsViewModel<WhiskyArchive.Services.WhiskyRecording.API.Application.Queries.Whisky>(
                pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);
        }



        /// <summary>
        /// 建立威士忌條目
        /// </summary>
        /// 
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/Whiskies/CreateWhiskyRecord
        ///     {
        ///        "distilleryName": "阿貝",
        ///        "whiskyNameChinese": "阿貝 1979 GM 26年 地圖標",
        ///        "WhiskyNameEnglish" : "Ardbeg 1979 GM Connoisseurs Choice",
        ///        "whiskyBottler" : "Gordon MacPhail",
        ///        "vintage" : "1979",
        ///        "statedAge" : 26,
        ///        "caskType" : "波本 Barrels(二次充填)",
        ///        "strength" : 0.43,
        ///        "size":  700,
        ///        "rating" : 88.67,
        ///        "notes" : "地圖標"
        ///     } 
        /// </remarks>
        /// 
        /// <param name="command">
        /// 
        /// </param>
        ///
        /// <param name="requestId">
        /// </param> 
        /// 
        /// 
        [Route("CreateWhiskyRecord")]
        [Authorize]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateWhiskyRecord([FromBody] CreateWhiskyRecordCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            bool commandResult = false;
            try
            {
                if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
                {
                    var createWhiskyRecordCommand = new IdentifiedCommand<CreateWhiskyRecordCommand, bool>(command, guid);
                    commandResult = await _mediator.Send(createWhiskyRecordCommand);
                }

                return commandResult ? (IActionResult)Ok() : (IActionResult)BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "  Details: " + ex.InnerException.ToString());
            }
        }




        /// <summary>
        /// 更新威士忌條目
        /// </summary>
        /// 
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/Whiskies/CreateWhiskyRecord
        ///     {
        ///        "whiskyId": "a19bcf213",
        ///        "DistilleryName" : "阿貝",
        ///        "whiskyNameChinese": "阿貝 1974 #2743",
        ///        "WhiskyNameEnglish" : "Ardbeg 1974 #2743",
        ///        "whiskyBottler" : "官方",
        ///        "vintage" : "1974",
        ///        "bottled" : "2005",
        ///        "statedAge" : 31,
        ///        "caskType" : "波本 豬頭",
        ///        "caskNumber" : "2743",
        ///        "NumberOfBottles" : 106,
        ///        "strength" : 0.517,
        ///        "size" : 700,
        ///        "market" : "無特別註明", 
        ///        "rating" : 93.5,
        ///        "notes" : "無備註"
        ///     } 
        /// </remarks>
        /// 
        /// <param name="command">
        /// 
        /// </param>
        ///
        /// <param name="requestId">
        /// </param> 
        /// 
        /// 
        [Route("UpdateWhiskyRecord")]
        [Authorize]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateWhiskyRecord([FromBody] UpdateWhiskyRecordCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            bool commandResult = false;
            try
            {
                if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
                {
                    var updateWhiskyRecordCommand = new IdentifiedCommand<UpdateWhiskyRecordCommand, bool>(command, guid);
                    commandResult = await _mediator.Send(updateWhiskyRecordCommand);
                }

                return commandResult ? (IActionResult)Ok() : (IActionResult)BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "  Details: " + ex.InnerException.ToString());
            }
        }



        /// <summary>
        /// 更新威士忌價格
        /// </summary>
        /// 
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/Whiskies/UpdateWhiskyPrice
        ///     {
        ///        "WhiskyId": "abcd-123a-567fabc",
        ///        "Price": 12500,
        ///        "CurrencyId" : 4,
        ///        "PriceReferenceId" :3,
        ///        "PriceDateYear" : 2019,
        ///        "PriceDateMonth" : 5,
        ///        "PriceDateDay" : 6
        ///        "Seller" : "Mr. Huang"
        ///     } 
        ///               
        /// Currency Id :
        ///     GBP = 1
        ///     EUR = 2
        ///     USD = 3
        ///     TWD = 4
        ///     RMB = 5
        ///     YEN = 6
        ///     
        /// PriceReference Id :
        ///     FacebookPublic  = 1
        ///     FacebookPrivate = 2
        ///     FacebookAuction = 3
        ///     EuropeanAuction = 4
        ///     ChineseAuction  = 5
        ///     JapaneseAuction = 6
        ///     Other           = 7
        /// </remarks>
        /// 
        /// <param name="command">
        /// 
        /// </param>
        ///
        /// <param name="requestId">
        /// </param> 
        /// 
        /// 
        [Route("UpdateWhiskyPrice")]
        [Authorize]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateWhiskyPrice([FromBody] UpdateWhiskyPriceCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            bool commandResult = false;
            try
            {
                if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
                {
                    var updateWhiskyPriceCommand = new IdentifiedCommand<UpdateWhiskyPriceCommand, bool>(command, guid);
                    commandResult = await _mediator.Send(updateWhiskyPriceCommand);
                }

                return commandResult ? (IActionResult)Ok() : (IActionResult)BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "  Details: " + ex.InnerException.ToString());
            }
        }



        /// <summary>
        /// 更新威士忌圖片
        /// </summary>
        /// 
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/Whiskies/UpdateWhiskyImage
        ///     {
        ///        "WhiskyId": "abcd-123a-567fabc",
        ///        "Urls":[
        ///        "https://i.imgur.com/Adce0Tt.jpg"
        ///        ]
        ///     } 
        /// </remarks>
        /// 
        /// <param name="command">
        /// 
        /// </param>
        ///
        /// <param name="requestId">
        /// </param> 
        /// 
        /// 
        [Route("UpdateWhiskyImage")]
        [Authorize]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateWhiskyImage([FromBody] UpdateWhiskyImageCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            bool commandResult = false;
            try
            {
                if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
                {
                    var updateWhiskyImageCommand = new IdentifiedCommand<UpdateWhiskyImageCommand, bool>(command, guid);
                    commandResult = await _mediator.Send(updateWhiskyImageCommand);
                }

                return commandResult ? (IActionResult)Ok() : (IActionResult)BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "  Details: " + ex.InnerException.ToString());
            }
        }



        // DELETE: api/Whiskies/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWhisky([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var whisky = await _context.Whiskys.FindAsync(id);
            if (whisky == null)
            {
                return NotFound();
            }

            _context.Whiskys.Remove(whisky);
            await _context.SaveChangesAsync();

            return Ok(whisky);
        }

        private bool WhiskyExists(int id)
        {
            return _context.Whiskys.Any(e => e.Id == id);
        }
    }
}