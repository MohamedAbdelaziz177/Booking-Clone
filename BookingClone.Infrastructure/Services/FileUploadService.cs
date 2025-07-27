
using BookingClone.Application.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace BookingClone.Infrastructure.Services;
public class FileUploadService : IFileUploadService
{
    private readonly IWebHostEnvironment webHostEnvironment;

    public FileUploadService(IWebHostEnvironment webHostEnvironment)
    {
        this.webHostEnvironment = webHostEnvironment;
    }
    public async Task<string> SaveImageAndGetUrl(IFormFile img, string Subfolder)
    {
        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);

        string rootDir = webHostEnvironment.WebRootPath;

        var fullPath = Path.Combine(rootDir, Subfolder, img.FileName);


        using(var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
        {
            await img.CopyToAsync(fileStream);
        }


        return fullPath;
    }
}
