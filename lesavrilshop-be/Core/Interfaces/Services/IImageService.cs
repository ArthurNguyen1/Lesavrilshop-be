using CloudinaryDotNet.Actions;

namespace lesavrilshop_be.Core.Interfaces.Services
{
    public interface IImageService
    {
        Task<ImageUploadResult> AddImageAsync(IFormFile file);
        Task<DeletionResult> DeleteImageAsync(string id);
    }
}