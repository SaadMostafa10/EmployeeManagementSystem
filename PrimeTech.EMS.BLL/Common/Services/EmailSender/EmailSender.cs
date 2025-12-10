using PrimeTech.EMS.DAL.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PrimeTech.EMS.BLL.Common.Services.EmailSender
{
    public class EmailSender : IEmailSender
    {
        public void SendEmail(Email email)
        {
            // Protocols : FTP , HTTP
            // Email Protocol : SMTP 
            // SendEmail => Noon@gmail.com , Noon@yahoo.com , Noon@outlook.com
            var client = new SmtpClient("smtp.gmail.com", 587); // SSL , TLS [Enable SSL / TLS]
            client.EnableSsl = true;
            // Sender , Reciever
            // Sender => NetworkCredential(username[Email Sender],AppPassword [Sender])
            // Gmail saadmostafa1174@gmail.com
            client.Credentials = new NetworkCredential("bdelrahman944@gmail.com", "ubnljrlmchsirbya");

            client.Send("bdelrahman944@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}
