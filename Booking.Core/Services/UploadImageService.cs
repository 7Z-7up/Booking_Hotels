using Booking.Core.Domain.RepositoryContracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Booking.Core.Services
{
    public class UploadImageService
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IUnitOfWork _unitOfWork;

        public UploadImageService(IWebHostEnvironment hostEnvironment,IUnitOfWork unitOfWork)
        {
            _hostEnvironment = hostEnvironment;
            _unitOfWork = unitOfWork;
        }
        public async Task DeleteFileAsync(string imageNameInDatabaseTable)
        {
            string ExitingFile = Path.Combine(_hostEnvironment.WebRootPath, "images", "company", imageNameInDatabaseTable);
            File.Delete(ExitingFile);
            var comp=await _unitOfWork.Companies.Find(c => c.Image == imageNameInDatabaseTable);
            comp.Image = null;
            _unitOfWork.Complete();
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
