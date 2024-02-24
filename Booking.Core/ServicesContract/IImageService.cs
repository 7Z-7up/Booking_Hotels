using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.ServicesContract
{
    public interface IImageService
    {
        public Task<string> UploadFileAsync(IFormFile ImageFile,string WebRootPath);
        public void DeleteFileAsync(string imageNameInDatabaseTable,string WebRootPath);




    }
}
