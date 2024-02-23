using Booking.Core.Domain.RepositoryContracts;
using Microsoft.AspNetCore.Http;


using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
namespace Booking.Core.Helpers.Services
{
    public static class HelperService
    {
        private static IWebHostEnvironment HostEnvironment;

        // Initialize method to set the hosting environment
        public static void Initialize(IWebHostEnvironment webHostEnvironment)
        {
            HostEnvironment = webHostEnvironment;
        }

        public static async Task<string> UploadImage(IFormFile formFile,String controllername)
        {
            // Ensure HostEnvironment is initialized
            if (HostEnvironment == null)
            {
                throw new InvalidOperationException("HelperService not initialized. Call Initialize method first.");
            }
            string UniqueFileName = null;
            if (formFile != null && formFile.Length > 0)
            {
                 UniqueFileName = Guid.NewGuid().ToString() + "-" + formFile.FileName;
                string TargetPath = Path.Combine(HostEnvironment.WebRootPath, "images", controllername, UniqueFileName);

                using (var stream = new FileStream(TargetPath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }  
            }

            return UniqueFileName;
        }
    }
}

