using Booking.Core.Domain.Entities;
using Booking.Core.Domain.RepositoryContracts;
using Booking.Core.DTO.hotel;
using Booking.Core.ServicesContract;
using Booking.Infrastructure.Repository;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;

namespace Booking.UI.Controllers
{
    public class HotelController : Controller
    {
        // GET: HotelController
        public IWebHostEnvironment Environment { get; set; }
        public IHotelService HotelService { get; set; }
        public IImageService ImageService { get; set; }

       
        public HotelController(IWebHostEnvironment environment, IHotelService hotelService ,IImageService imageService) 
        {
           
            Environment= environment;
            HotelService= hotelService;
            ImageService= imageService;

        }
        //================================================
        public async Task<IActionResult> Index()
        {
            ICollection<HotelDto> hotelDtos = await HotelService.showAllHotel();
            return View(hotelDtos);
        }


        //==================================================

        // GET: HotelController/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var hotel = await HotelService.GetHotel(Guid.Parse(id));
            if (hotel != null)
            {
                return View(await HotelService.GetHotelDetails(Guid.Parse(id),hotel));
            }
            return RedirectToAction("Index");
        }
        //===========================================================================================

        // GET: HotelController/Create
        public async Task<IActionResult> Create()
        {
            HotelDto hotel = new HotelDto();
            hotel.Comps = await HotelService.GetCompanies(); //UnitOfWork.Companies.FindAll(com=>com.IsDeleted==false);
            return View(hotel);
        }

        // POST: HotelController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(include:"Name,Address,companyId,Rate,ImageUrl")] HotelDto hotelDto)
        {
            string WebRootPath = Environment.WebRootPath;
            if (ModelState.IsValid)
            {
               
                if (await HotelService.Create(hotelDto, WebRootPath) == false)
                {
                    ModelState.AddModelError("ImageFile", "Wrong File Format!");
                    hotelDto.Comps = await HotelService.GetCompanies();
                    return View(hotelDto);
                }
                else
                {
                    //UnitOfWork.Complete();
                    HotelService.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                hotelDto.Comps = await HotelService.GetCompanies();
                return View(hotelDto);
            }
        }
        //===========================================================================================



        // GET: HotelController/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            Hotel hotel = await HotelService.GetHotel(Guid.Parse(id)); 
            if (hotel != null)
            {
                IEnumerable<HotelImages> hotelImage = await HotelService.GetImages(Guid.Parse(id));
                UpdateHotelDTO updateHotelDTO = new UpdateHotelDTO()
                {
                    Name = hotel.Name,
                    Address = hotel.Address,
                    Rate= hotel.Rate,
                    companyId=hotel.CompId,
                    
                };
                updateHotelDTO.ExsitingFile = new();
                if (hotelImage != null)
                {
                    foreach (HotelImages image in hotelImage)
                    {
                        updateHotelDTO.ExsitingFile?.Add(image.Image);
                    }
                    updateHotelDTO.Comps = await HotelService.GetCompanies();
                    
                    return View(updateHotelDTO);
                }
            }
            return RedirectToAction("Index");
        }

       // POST: HotelController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(string id, [Bind(include: "Name,Address,companyId,Rate,ImageUrl,IsDeleted")] UpdateHotelDTO updateHotelDTO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var hotel = await UnitOfWork.Hotels.GetById(Guid.Parse(id));
        //        if (hotel != null)
        //        {
        //            hotel.Name = updateHotelDTO.Name;
        //            hotel.Address = updateHotelDTO.Address;
        //            hotel.Rate = updateHotelDTO.Rate;
        //            hotel.CompId = updateHotelDTO.companyId;
        //            hotel.IsDeleted= updateHotelDTO.IsDeleted;
                  
        //        }
        //        UnitOfWork.Hotels.Update(hotel);
        //        //IEnumerable<HotelImages> hotelImages = await UnitOfWork.HotelImages.FindAll(hot => hot.hotelId == Guid.Parse(id));
        //        //int len = Math.Max(hotelImages.Count(), updateHotelDTO.ImageUrl.Count);
        //        //for (int i = 0; i < len; i++)
        //        //{
        //        //    hotelImages[i] = imagePath;

        //        //}
        //        string WebRootPath = Environment.WebRootPath;

        //        if (updateHotelDTO.ImageUrl != null)
        //        {
        //            var hotelImages = await UnitOfWork.HotelImages.FindAll(img => img.hotelId == Guid.Parse(id));
        //            foreach (var hotelImage in hotelImages)
        //            {
        //                if (hotelImage != null)
        //                {
        //                    UnitOfWork.HotelImages.Delete(hotelImage);
        //                    string ExitingFile = Path.Combine(Environment.WebRootPath, "images", "Hotel", hotelImage.Image);
        //                    System.IO.File.Delete(ExitingFile);

        //                }

        //            }
           
                
        //            foreach (var image in updateHotelDTO.ImageUrl)
        //            {
        //                if (image.ContentType.StartsWith("image/"))
        //                {
        //                    string FilePath = await ImageService.UploadFileAsync(image, WebRootPath);
        //                    var HotelImage = new HotelImages()
        //                    {
        //                        hotelId = hotel.ID,
        //                        Image = FilePath
        //                    };
        //                    await UnitOfWork.HotelImages.Add(HotelImage);
        //                }
        //                else
        //                {
        //                    ModelState.AddModelError("ImageFile", "Wrong File Format!");
        //                updateHotelDTO.Comps = await UnitOfWork.Companies.FindAll(com => com.IsDeleted == false);
        //                return View(updateHotelDTO);
        //                }
        //            }
        //        }
        //        UnitOfWork.Complete();
        //        return RedirectToAction("Index");



        //    }
        //    else
        //    {
        //        ModelState.AddModelError("ImageFile", "Wrong File Format!");
        //        updateHotelDTO.Comps = await UnitOfWork.Companies.FindAll(com => com.IsDeleted == false);
        //        return View(updateHotelDTO);
        //    }

        //}

        // GET: HotelController/Delete/5

        public async Task<IActionResult> Delete(string id)
        {
            if (await HotelService.deleteHotel(Guid.Parse(id)))
            {
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        public async Task<IActionResult> permantentDeltion(string id)
        {
            string WebRootPath = Environment.WebRootPath;

            if (await HotelService.Permanent_Deletion(Guid.Parse(id), WebRootPath)) 
            {
                return RedirectToAction("Index");
            }
            else
            {
                return NoContent();
            }
        }
        
    }
}
