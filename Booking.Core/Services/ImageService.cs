using Booking.Core.Domain.Entities;
using Booking.Core.ServicesContract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.Services
{
    public class ImageService: IImageService
    {
        public void DeleteFileAsync(string imageNameInDatabaseTable, string WebRootPath)
        {
            string ExitingFile = Path.Combine(WebRootPath, "images", "Hotel", imageNameInDatabaseTable);
            File.Delete(ExitingFile);
        }

        public async Task<string> UploadFileAsync(IFormFile ImageFile,string WebRootPath)
        {
            string UniqueFileName = null;
            if (ImageFile != null)
            {
                UniqueFileName = Guid.NewGuid().ToString() + "-" + ImageFile.FileName;
                string TargetPath = Path.Combine(WebRootPath, "images", "Hotel", UniqueFileName);
                using (var stream = new FileStream(TargetPath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }
            }
            return UniqueFileName;
        }

    }
}
