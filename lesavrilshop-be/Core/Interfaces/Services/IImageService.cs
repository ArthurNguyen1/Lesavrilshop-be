using CloudinaryDotNet.Actions;

namespace lesavrilshop_be.Core.Interfaces.Services
{
    public interface IImageService
    {
        Task<DeletionResult> DeleteImageAsync(string id);

        Task<string> UploadImageAsync(IFormFile file, string folder);
    }
}