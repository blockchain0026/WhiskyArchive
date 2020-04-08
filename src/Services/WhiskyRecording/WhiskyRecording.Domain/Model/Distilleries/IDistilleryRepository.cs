using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WhiskyArchive.Services.WhiskyRecording.Domain.SeedWork;

namespace WhiskyArchive.Services.WhiskyRecording.Domain.Model.Distilleries
{
    public interface IDistilleryRepository : IRepository<Distillery>
    {
        Distillery Add(Distillery distillery);
        Distillery Update(Distillery distillery);
        Task<Distillery> GetByDistilleryIdAsync(string distilleryId);
        Task<Distillery> GetByDistilleryNameAsync(string distilleryName);
        Task<IEnumerable<Distillery>> GetAllAsync();
    }
}
