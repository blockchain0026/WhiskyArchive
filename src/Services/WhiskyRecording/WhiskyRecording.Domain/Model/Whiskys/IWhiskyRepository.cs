using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskyArchive.Services.WhiskyRecording.Domain.SeedWork;

namespace WhiskyArchive.Services.WhiskyRecording.Domain.Model.Whiskys
{
    public interface IWhiskyRepository : IRepository<Whisky>
    {
        Whisky Add(Whisky whisky);
        Whisky Update(Whisky whisky);
        Task<Whisky> GetByWhiskyIdAsync(string whiskyId);
        Task<IEnumerable<Whisky>> GetByKeywordAsync(string keyword);
        Task<IEnumerable<Whisky>> GetByDistilleryIdAsync(string distilleryId);

        Task<IEnumerable<Whisky>> GetAllAsync();
    }
}
