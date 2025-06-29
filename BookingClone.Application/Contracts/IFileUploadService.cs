
using Microsoft.AspNetCore.Http;

namespace BookingClone.Application.Contracts;
public interface IFileUploadService
{
    Task<string> SaveImageAndGetUrl(IFormFile img, string Subfolder);
}
