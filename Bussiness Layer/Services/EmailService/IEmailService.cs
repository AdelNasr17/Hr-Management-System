using DataAccess.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Layer.Services.EmailService
{
    public interface IEmailService
    {
        public void SendEmail(Email email);
    }
}
