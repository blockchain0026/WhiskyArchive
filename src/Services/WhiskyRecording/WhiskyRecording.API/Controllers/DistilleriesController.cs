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
using WhiskyArchive.Services.WhiskyRecording.Domain.Model.Distilleries;
using WhiskyArchive.Services.WhiskyRecording.Infrastructure;

namespace WhiskyRecording.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistilleriesController : ControllerBase
    {
        private readonly WhiskyRecordingContext _context;
        private readonly IDistilleryRepository _distilleryRepository;
        private readonly IWhiskyQueries _whiskyQueries;
        private readonly IMediator _mediator;
        public DistilleriesController(WhiskyRecordingContext context, IDistilleryRepository distilleryRepository, IWhiskyQueries whiskyQueries, IMediator mediator)
        {
            _context = context;
            _distilleryRepository = distilleryRepository ?? throw new ArgumentNullException(nameof(distilleryRepository));
            _whiskyQueries = whiskyQueries ?? throw new ArgumentNullException(nameof(whiskyQueries));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        // GET: api/Distilleries
        [Route("GetAllDistilleries")]
        [HttpGet]
        public async Task<IActionResult> GetAllDistilleries()
        {
            var distilleries = await this._whiskyQueries.GetAllDistilleriesAsync();

            return Ok(distilleries);
        }


        // GET: api/Whiskies/5
        [HttpGet("{distilleryId}")]
        public async Task<IActionResult> GetDistillery([FromRoute] string distilleryId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var whisky = await _distilleryRepository.GetByDistilleryIdAsync(distilleryId);

            if (whisky == null)
            {
                return NotFound();
            }

            return Ok(whisky);
        }

        /// <summary>
        /// 建立蒸餾場條目
        /// </summary>
        /// 
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/Distilleries/CreateDistillery
        ///     {
        ///        "chineseTraditional" : "阿貝",
        ///        "chineseSimplified" : "阿贝",
        ///        "english" : "Ardbeg",
        ///        "established" : "1815",
        ///        "introdution" : "單一麥芽威士忌酒廠，1815年創廠時便受到各方關注...",
        ///        "smwsCode" : "33"
        ///     } 
        ///     
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
        [Route("CreateDistillery")]
        [Authorize]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateDistillery([FromBody] CreateDistilleryCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            bool commandResult = false;
            try
            {
                if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
                {
                    var createDistilleryCommand = new IdentifiedCommand<CreateDistilleryCommand, bool>(command, guid);
                    commandResult = await _mediator.Send(createDistilleryCommand);
                }

                return commandResult ? (IActionResult)Ok() : (IActionResult)BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "  Details: " + ex.InnerException.ToString());
            }
        }

        // DELETE: api/Distilleries/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDistillery([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var distillery = await _context.Distillerys.FindAsync(id);
            if (distillery == null)
            {
                return NotFound();
            }

            _context.Distillerys.Remove(distillery);
            await _context.SaveChangesAsync();

            return Ok(distillery);
        }

        private bool DistilleryExists(int id)
        {
            return _context.Distillerys.Any(e => e.Id == id);
        }
    }
}