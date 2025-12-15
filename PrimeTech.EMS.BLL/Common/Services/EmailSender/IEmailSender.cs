using PrimeTech.EMS.DAL.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeTech.EMS.BLL.Common.Services.EmailSender
{
    public interface IEmailSender
    {
        void SendEmail(Email email); // Signature Method [SendEmail()]
    }
}
