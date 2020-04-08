using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskyArchive.Services.WhiskyRecording.Domain.Model.Distilleries;
using WhiskyArchive.Services.WhiskyRecording.Domain.SeedWork;

namespace WhiskyArchive.Services.WhiskyRecording.Infrastructure.Repositories
{

    public class DistilleryRepository : IDistilleryRepository
    {
        private readonly WhiskyRecordingContext _context;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public DistilleryRepository(WhiskyRecordingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Distillery Add(Distillery distillery)
        {
            if (distillery.IsTransient())
            {
                return _context.Distillerys
                    .Add(distillery)
                    .Entity;
            }
            else
            {
                return distillery;
            }
        }

        public Distillery Update(Distillery distillery)
        {
            return _context.Distillerys.Update(distillery).Entity;

            //_context.Entry(candleChart).State = EntityState.Modified;
            //return candleChart;
        }

        public async Task<Distillery> GetByDistilleryIdAsync(string distilleryId)
        {
            var distillery = await _context.Distillerys
                .Where(d => d.DistilleryId == distilleryId)
                .SingleOrDefaultAsync();


            return distillery;
        }


        public async Task<IEnumerable<Distillery>> GetAllAsync()
        {
            var distillerys = _context.Distillerys.AsEnumerable();


            return distillerys;
        }

        public async Task<Distillery> GetByDistilleryNameAsync(string distilleryName)
        {
            var distillery = await _context.Distillerys.Where(d =>
               d.DistilleryName.ChineseTraditional == distilleryName
               || d.DistilleryName.ChineseSimplified == distilleryName
               || d.DistilleryName.English == distilleryName).SingleOrDefaultAsync();

            return distillery;
        }
    }
}
