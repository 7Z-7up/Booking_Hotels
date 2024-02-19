using Booking.Core.Domain.RepositoryContracts;
using Booking.Core.DTO;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Booking.UI.Controllers
{
    public class CompanyController : Controller
    {
        public CompanyController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            UnitOfWork = unitOfWork;
            this.HostEnvironment = hostEnvironment;
        }

        public IUnitOfWork UnitOfWork { get; }
        public IWebHostEnvironment HostEnvironment { get; }

        public async Task<IActionResult> Index()
        {
            var company = await UnitOfWork.Companies.GetAll();
            return View(company);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(CompanyDTO companyDTO)
        {
            if (ModelState.IsValid)
            {
                Company company = new()
                {
                    ID = Guid.NewGuid(),
                    Name = companyDTO.Name,
                    TotalProfits = 0,
                    IsDeleted = false,
                };
                if (companyDTO.ImageFile != null)
                {
                    if (companyDTO.ImageFile.ContentType.StartsWith("image/"))
                        company.Image = await UploadFileAsync(companyDTO.ImageFile);
                    else
                    {
                        ModelState.AddModelError("ImageFile", "Wrong File Format!");
                        return View(companyDTO);
                    }
                }
                else
                {
                    ModelState.AddModelError("ImageFile", "Image is Required!!");
                    return View(companyDTO);
                }

                UnitOfWork.Companies.Add(company);
                UnitOfWork.Complete();
                return RedirectToAction("Index");
            }

            return View(companyDTO);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var company = await UnitOfWork.Companies.GetById(Guid.Parse(id));
            CompanyDTO companyDTO = new CompanyDTO(company);

            return View(companyDTO);
        }

        public async Task<IActionResult> Edit(CompanyDTO companyDTO)
        {
            if (ModelState.IsValid)
            {

            var company = await UnitOfWork.Companies.GetById(companyDTO.ID);

                company.Name = companyDTO.Name;
                if (companyDTO.ImageFile != null)
                {
                    if (companyDTO.ImageFile.ContentType.StartsWith("image/"))
                    {
                        if (company.Image != null)
                        {
                            string ExitingFile = Path.Combine(HostEnvironment.WebRootPath, "images","company" , company.Image);
                            System.IO.File.Delete(ExitingFile);
                        }
                        company.Image = await UploadFileAsync(companyDTO.ImageFile);
                    }
                    else
                    {
                        ModelState.AddModelError("ImageFile", "Wrong File Format!");
                        return View(companyDTO);
                    }
                }
                UnitOfWork.Companies.Update(company);
                UnitOfWork.Complete();
                return RedirectToAction("Index");
            }

            return View(companyDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var company = await UnitOfWork.Companies.GetById(Guid.Parse(id));
            if(company != null)
            {
                company.IsDeleted = true;

                UnitOfWork.Companies.Update(company);
                UnitOfWork.Complete();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var company = await UnitOfWork.Companies.GetById(Guid.Parse(id));
            if(company != null)
            {
                return View(company);
            }

            return RedirectToAction("Index");
        }

        private async Task<string> UploadFileAsync(IFormFile formFile)
        {
            string UniqueFileName = Guid.NewGuid().ToString() + "-" + formFile.FileName;
            string TargetPath = Path.Combine(HostEnvironment.WebRootPath, "images","company" , UniqueFileName);
            using (var stream = new FileStream(TargetPath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }
            return UniqueFileName;
        }
    }
}
