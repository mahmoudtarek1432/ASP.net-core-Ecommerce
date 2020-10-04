using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAcessoriesBack.Services
{
    public interface IEMailService
    {
         Task<bool> SendEMailAsync(string mailFrom, string mailFromPassword, string mailTo, string content, string subject, bool isBodyHtml);
    }
}
