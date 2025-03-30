using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Layer.Comman.Services.AttachmentServices
{
    public class AttachmentService : IAttachmentService
    {
        //Allowed Extensions [png,ipg,jpng]
        public readonly List<string> _AllowedExtensions = new() { "png", "jgp", "jpeg" };
        //Max Size //2MB
        public const int _MaxAllowedSize = 2097152;
        public bool Delete(string filePath)
        {
            if(File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            else  return false; 
        }

        public string? Upload(IFormFile file, string folderName)
        {
            //1] Validate for extensions [".png", ".jpg",
            var extension=Path.GetExtension(file.FileName);
            if(!_AllowedExtensions.Contains(extension))
                return null;

            //2] Validate for Max size [2_097_152; //2MB]
            if(file.Length > _MaxAllowedSize)
                return null;
            //3] Get located folder path
            //var folderPath= "G:\\01 Courses\\01 course Back_End Route\\05-ASP.Net MVC\\Presentation Layer\\Presentation Layer\\wwwroot\\files\\Images\\"
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files",folderName);
            //4] Set unique file name
            var fileName = $"{Guid.NewGuid()}{extension}";
            //5] Get file path[FolderPath + FileName]
            var filePath= Path.Combine(folderPath,fileName);
            //6] Save file as stream[Data per time]
            using var fileStream = new FileStream(filePath, FileMode.Create);
            
            //7] Copy file to the stream
            file.CopyTo(fileStream);
            //8] Return file name
            return fileName;
        }
    }
}
