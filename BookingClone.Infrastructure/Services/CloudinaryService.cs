
using BookingClone.Application.Contracts;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace BookingClone.Infrastructure.Services;

public class CloudinaryService : ICloudinaryService
{
    private readonly Cloudinary cloudinary;
    public CloudinaryService(IConfiguration configuration)
    {
       
        var CloudName = configuration["CLOUDINARY__CLOUD_NAME"];
        var CloudApiKey = configuration["CLOUDINARY__API_KEY"];
        var CloudSecret = configuration["CLOUDINARY__API_SECRET"];

        cloudinary = new Cloudinary(new Account(CloudName, CloudApiKey, CloudSecret));
    }

    public async Task<string> SaveImageAndGetUrl(IFormFile file, string subfolder)
    {
        using (var stream = file.OpenReadStream())
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(fileName, stream),
                Folder = subfolder
            };

            var uploadedImg = await cloudinary.UploadAsync(uploadParams);

            return uploadedImg.SecureUrl.ToString();
        }
    }
}
