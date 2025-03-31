


namespace Bussiness_Layer.Comman.Services.AttachmentServices
{
    public interface IAttachmentService
    {
        //UpLoad,Delete

        public Task< string?> UploadAsync(IFormFile file, string folderName);
        public bool Delete(string filePath);
    }
}
