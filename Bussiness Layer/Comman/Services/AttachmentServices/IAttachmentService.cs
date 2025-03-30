using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Layer.Comman.Services.AttachmentServices
{
    public interface IAttachmentService
    {
        //UpLoad,Delete

        public string? Upload(IFormFile file, string folderName);
        public bool Delete(string filePath);
    }
}
