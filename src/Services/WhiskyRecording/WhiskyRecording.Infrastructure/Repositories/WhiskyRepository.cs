using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskyArchive.Services.WhiskyRecording.Domain.Model.Whiskys;
using WhiskyArchive.Services.WhiskyRecording.Domain.SeedWork;

namespace WhiskyArchive.Services.WhiskyRecording.Infrastructure.Repositories
{

    public class WhiskyRepository : IWhiskyRepository
    {
        private readonly WhiskyRecordingContext _context;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public WhiskyRepository(WhiskyRecordingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Whisky Add(Whisky whisky)
        {
            if (whisky.IsTransient())
            {
                return _context.Whiskys
                    .Add(whisky)
                    .Entity;
            }
            else
            {
                return whisky;
            }
        }

        public Whisky Update(Whisky whisky)
        {
            return _context.Whiskys.Update(whisky).Entity;

            //_context.Entry(candleChart).State = EntityState.Modified;
            //return candleChart;
        }

        public async Task<Whisky> GetByWhiskyIdAsync(string whiskyId)
        {
            var whisky = await _context.Whiskys
                .Include(w => w.WhiskyPrices)
                .Include(w => w.WhiskyImages)
                .Where(w => w.WhiskyId == whiskyId)
                .SingleOrDefaultAsync();

            if (whisky != null)
            {
                /*await _context.Entry(whisky)
                      .Reference(t => t.TraceStatus).LoadAsync();*/
                foreach (var price in whisky.WhiskyPrices)
                {
                    await _context.Entry(price)
                        .Reference(p => p.Currency).LoadAsync();
                    await _context.Entry(price)
                        .Reference(p => p.PriceReference).LoadAsync();
                }
            }

            return whisky;
        }





        public async Task<IEnumerable<Whisky>> GetByKeywordAsync(string keyword)
        {
            var whiskys = _context.Whiskys
                  .Include(w => w.WhiskyPrices)
                  .Include(w => w.WhiskyImages)
                  .Where(w => w.WhiskyName.English.Contains(keyword) || w.WhiskyName.Chinese.Contains(keyword));

            if (whiskys.Any())
            {
                foreach (var whisky in whiskys)
                {

                    foreach (var price in whisky.WhiskyPrices)
                    {
                        await _context.Entry(price)
                            .Reference(p => p.Currency).LoadAsync();
                        await _context.Entry(price)
                            .Reference(p => p.PriceReference).LoadAsync();
                    }
                }
            }

            return whiskys;
        }

        public async Task<IEnumerable<Whisky>> GetByDistilleryIdAsync(string distilleryId)
        {
            var whiskys = _context.Whiskys
                  .Include(w => w.WhiskyPrices)
                  .Include(w => w.WhiskyImages)
                  .Where(w => w.DistilleryId == distilleryId);

            if (whiskys.Any())
            {
                foreach (var whisky in whiskys)
                {

                    foreach (var price in whisky.WhiskyPrices)
                    {
                        await _context.Entry(price)
                            .Reference(p => p.Currency).LoadAsync();
                        await _context.Entry(price)
                            .Reference(p => p.PriceReference).LoadAsync();
                    }
                }
            }

            return whiskys;
        }

        public async Task<IEnumerable<Whisky>> GetAllAsync()
        {
            var whiskys = _context.Whiskys
                    .Include(w => w.WhiskyPrices)
                    .Include(w => w.WhiskyImages);

            if (whiskys.Any())
            {
                foreach (var whisky in whiskys)
                {

                    foreach (var price in whisky.WhiskyPrices)
                    {
                        await _context.Entry(price)
                            .Reference(p => p.Currency).LoadAsync();
                        await _context.Entry(price)
                            .Reference(p => p.PriceReference).LoadAsync();
                    }
                }
            }

            return whiskys;
        }
    }
}
