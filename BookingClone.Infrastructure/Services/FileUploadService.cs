
using BookingClone.Application.Contracts;
using Microsoft.AspNetCore.Http;

namespace BookingClone.Infrastructure.Services;
public class FileUploadService : IFileUploadService
{

    public FileUploadService()
    {
    }
    public string SaveImageAndGetUrl(IFormFile img)
    {
        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);

        return "";
    }
}
