using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McGillWebAPI.Services
{
    public interface IMailService
    {
        Task SendMail( string to, string name, string from, string subject, string body, string cc);
    }
}
