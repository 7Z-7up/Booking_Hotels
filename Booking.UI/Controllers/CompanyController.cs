
using Booking.Core.DTO;
using Booking.Core.ServicesContract;

using Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Booking.UI.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }


        public async Task<IActionResult> Index()
        {
            var company = await _companyService.GetAll();
            return View(company);
        }


        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(CompanyDTO companyDTO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (companyDTO.ImageFile != null)
        //        {
        //            if (!companyDTO.ImageFile.ContentType.StartsWith("image/"))
        //            {
        //                ModelState.AddModelError("ImageFile", "Wrong File Format!");
        //                return View(companyDTO);
        //            }
        //        }
                
        //        return RedirectToAction("Index");
        //    }

        //    return View(companyDTO);
        //}
        //[HttpGet]
        //public async Task<IActionResult> Edit(string id)
        //{
        //    //var company = await UnitOfWork.Companies.GetById(Guid.Parse(id));
        //    //CompanyDTO companyDTO = new CompanyDTO(company);

        //    return View();//companyDTO);
        //}

        //public async Task<IActionResult> Edit(CompanyDTO companyDTO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (companyDTO.ImageFile != null)
        //        {
                   
        //            if (!companyDTO.ImageFile.ContentType.StartsWith("image/"))
        //            {
        //                ModelState.AddModelError("ImageFile", "Wrong File Format!");
        //                return View(companyDTO);
        //            }
        //        }
        //        return RedirectToAction("Index");
        //    }

        //    return View(companyDTO);
        //}

        //[HttpGet]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    await _companyService.DeleteAsync(id);
        //    return RedirectToAction("Index");
        //}


        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var company = _companyService.GetCompanyById(id);
            if (company != null)
            {
                return View(company);
            }

            return RedirectToAction("Index");
        }

    }
}
