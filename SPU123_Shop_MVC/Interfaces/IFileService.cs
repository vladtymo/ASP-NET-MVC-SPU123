namespace SPU123_Shop_MVC.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadImage(IFormFile file);
        Task DeleteImage(string path);
    }
}
