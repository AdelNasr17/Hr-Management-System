using DataAccess.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Layer.Services.EmailService
{
    public class EmailService : IEmailService
    {

        void IEmailService.SendEmail(Email email)
        {
            using (var Client = new SmtpClient("smtp.gmail.com", 587))
            {
                Client.EnableSsl = true;

                Client.Credentials = new NetworkCredential("adelnasrroute@gmail.com", "fbbohcmeoerznujd");
                Client.Send("adelnasrroute@gmail.com", email.To, email.Subject, email.Body);
            }
         
        }
    }
}
