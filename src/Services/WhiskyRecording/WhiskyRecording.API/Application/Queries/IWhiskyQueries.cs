using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhiskyArchive.Services.WhiskyRecording.API.Application.Queries
{
    public interface IWhiskyQueries
    {
        Task<Whisky> GetWhiskyAsync(string whiskyId);
        Task<List<Whisky>> GetAllWhiskiesAsync();
        Task<List<Distillery>> GetAllDistilleriesAsync();
        Task<Whisky> MapWhisky(Domain.Model.Whiskys.Whisky whisky);
    }
}
