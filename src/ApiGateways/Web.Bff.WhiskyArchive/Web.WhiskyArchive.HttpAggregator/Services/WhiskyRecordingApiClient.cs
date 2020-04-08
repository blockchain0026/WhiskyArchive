using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WhiskyArchive.Web.WhiskyArchive.HttpAggregator.Config;

namespace WhiskyArchive.Web.WhiskyArchive.HttpAggregator.Services
{
    public class WhiskyRecordingApiClient:IWhiskyRecordingApiClient
    {
        private readonly HttpClient _apiClient;
        private readonly ILogger<WhiskyRecordingApiClient> _logger;
        private readonly UrlsConfig _urls;

        public WhiskyRecordingApiClient(HttpClient httpClient, ILogger<WhiskyRecordingApiClient> logger, IOptions<UrlsConfig> config)
        {
            _apiClient = httpClient;
            _logger = logger;
            _urls = config.Value;
        }
    }
}
