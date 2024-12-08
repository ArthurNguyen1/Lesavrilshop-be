using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using lesavrilshop_be.Core.Interfaces.Services;
using lesavrilshop_be.Helpers;
using Microsoft.Extensions.Options;

namespace lesavrilshop_be.Application.Services
{
    public class ImageService : IImageService
    {
        private readonly Cloudinary _cloudinary;
        public ImageService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account
            (
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }
        public async Task<ImageUploadResult> AddImageAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult;
        }

        public async Task<DeletionResult> DeleteImageAsync(string id)
        {
            var deleteParams = new DeletionParams(id);

            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result;
        }

        public async Task<string> UploadImageAsync(IFormFile file, string folder)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("No file uploaded");

            // Validate file type
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(fileExtension))
                throw new ArgumentException("Invalid file type. Only jpg, jpeg, png, and gif are allowed.");

            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = folder,
                // Optional transformations
                Transformation = new Transformation()
                    .Width(500) // Set maximum width
                    .Height(500) // Set maximum height
                    .Crop("limit") // Maintain aspect ratio
                    .Quality("auto") // Automatic quality optimization
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
                throw new Exception(uploadResult.Error.Message);

            return uploadResult.SecureUrl.ToString();
        }
    }
}