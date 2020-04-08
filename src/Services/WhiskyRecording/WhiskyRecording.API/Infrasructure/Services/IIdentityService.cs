using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhiskyArchive.Services.WhiskyRecording.API.Infrasructure.Services
{
    public interface IIdentityService
    {
        string GetUserIdentity();

        string GetUserName();
    }
}
