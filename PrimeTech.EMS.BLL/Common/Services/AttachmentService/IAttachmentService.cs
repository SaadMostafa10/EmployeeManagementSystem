using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeTech.EMS.BLL.Common.Services.AttachmentService
{
    public interface IAttachmentService
    {
        // Upload , Delete
        Task<string?> UploadAsync(IFormFile file,string folderName);
        bool Delete(string filePath);
    }
}
