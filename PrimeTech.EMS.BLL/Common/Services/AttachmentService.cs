using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeTech.EMS.BLL.Common.Services
{
    public class AttachmentService : IAttachmentService
    {
        // Allowed Extention { ".png", ".jpg", ".jpeg" } 
        public readonly List<string> _allowedExtension = new() { ".png", ".jpg", ".jpeg" };

        // Allowed MAX SIZE => 2 MB 
        public const int _allowedMaxSize = 2_097_152;

        public async Task<string?> UploadAsync(IFormFile file, string folderName)
        {
            // 1] Validation For Type Extentions => { ".png", ".jpg", ".jpeg" } 
            var extention = Path.GetExtension(file.FileName); // Saad.png
            if(!_allowedExtension.Contains(extention))
                return null;

            // 2] Validation For Max Size [2 MB]
            if(file.Length > _allowedMaxSize)
                return null;
            
            // 3] Get Located Folder Path

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files",folderName);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // 4] Set Image be Unique [Must Image Name Be Unique] To Block Dublicate
            var fileName= $"{Guid.NewGuid()}{extention}";

            // 5] Get FilePath = [FolderPath + FileName]
            var filePath = Path.Combine(folderPath, fileName);

            // 6] Save File AS Streams =>[Data Per Time]
            using var fileStream = new FileStream(filePath, FileMode.Create);

            // 7] Copy File To FileStream 
            await file.CopyToAsync(fileStream);

            // 8] Return FileName => that store in DB
            return fileName;
        }
        public bool Delete(string filePath)
        {
            // 1. Check File Exist Or No  (filePath)
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }

        // File streaming 
        /* 
          * File streaming :
          1)You Open the Image File
          FileStream establishes a connection between your program and the image file.
          The file is opened in read, write, or both modes.
         
          2)You Read or Write Binary Data
          Images are stored as bytes (0s and 1s), not as human-readable text.
         
          3)You Close the File After Use
          Keeping an image file open too long can cause file locks or memory issues.
          Use using to ensure proper closure.
         */

        // FileMode 
        /*
         FileMode.Create       Creates a new file. Overwrites if the file already exists.
         FileMode.Open         Opens an existing file. Throws an exception if it doesn't exist.
         FileMode.Append       Opens the file if it exists or creates a new one. Data is written to the
                               end of the file.
         FileMode.Truncate     Opens an existing file and clears its content.
         FileMode.OpenOrCreate Opens the file if it exists or creates a new one.
        */




    }
}
