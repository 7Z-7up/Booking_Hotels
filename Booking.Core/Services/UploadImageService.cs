using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Booking.Core.Services
{
    public class UploadImageService
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        public UploadImageService(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }
        public async Task<string> UploadFileAsync(IFormFile formFile)
        {
            string UniqueFileName = Guid.NewGuid().ToString() + "-" + formFile.FileName;
            string TargetPath = Path.Combine(_hostEnvironment.WebRootPath, "images", "company", UniqueFileName);
            using (var stream = new FileStream(TargetPath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }
            return UniqueFileName;
        }

    }
}
