using Booking.Core.Domain.Entities;
using Booking.Core.Domain.RepositoryContracts;
using Booking.Core.DTO.hotel;
using Booking.Core.Services;
using Booking.Core.ServicesContract;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Booking.Core.Services
{
    public class HotelService:IHotelService
    {
        public IImageService ImageService { get; set; }
       
        public IUnitOfWork UnitOfWork { get; set; }

        public HotelService(IUnitOfWork unitOfWork, IImageService imageService)
        {
            UnitOfWork = unitOfWork;
            ImageService = imageService;
        }

        //=========================================================================================================

        public async Task<ICollection<HotelDto>> showAllHotel()
        {
            
            List<HotelDto> hotelDtos=new();
            //var hotels=UnitOfWork.Hotels.FindAll(hotel=>hotel.IsDeleted==false);
            IEnumerable<Hotel> hotels= await UnitOfWork.Hotels.FindAll(hotel => hotel.IsDeleted == false);
            
            foreach (Hotel hotel in hotels)
            {
                hotel.Images = await UnitOfWork.HotelImages.FindAll(himage=>himage.hotelId==hotel.ID);
                HotelDto hotelDto = new()
                {
                    ID = hotel.ID,
                    Rate = hotel.Rate,
                    Address = hotel.Address,
                    Name = hotel.Name,
                   // OwnerCompany=hotel.Company.Name

                };
                hotelDto.ImagePath = new();
                foreach (var image in hotel.Images)
                {
                    
                    hotelDto.ImagePath?.Add(image.Image);
                }
                 hotelDtos.Add(hotelDto);

            }
            return hotelDtos;
            
        }

        //=========================================================================================================
        public async Task<IEnumerable<Company>> GetCompanies()
        {
           return await UnitOfWork.Companies.FindAll(com => com.IsDeleted == false);
               
        }
        //=========================================================================================================
        public  int SaveChanges()
        {
           return UnitOfWork.Complete();
        }

        //=========================================================================================================

        public async Task<HotelDto> GetHotelDetails(Guid id, Hotel hotel)
        {           
            hotel.Images = await UnitOfWork.HotelImages.FindAll(himage => himage.hotelId == id);
            HotelDto hotelDto = new()
            {
                ID = hotel.ID,
                Rate = hotel.Rate,
                Address = hotel.Address,
                Name = hotel.Name,

            };
            hotelDto.ImagePath = new();
            foreach (var image in hotel.Images)
            {
                hotelDto.ImagePath?.Add(image.Image);
            }
            return hotelDto;
        }

        //=========================================================================================================
        public async Task<IEnumerable<HotelImages>> GetImages(Guid id)
        {
            return await UnitOfWork.HotelImages.FindAll(img => img.hotelId == id);
        }
        //=========================================================================================================
        public async Task<Hotel> GetHotel(Guid id)
        {
            return await UnitOfWork.Hotels.GetById(id);
        }

        //=========================================================================================================
       
        
        public async Task<bool> Permanent_Deletion(Guid id,string WebRootPath)
        {
            var hotel = await UnitOfWork.Hotels.GetById(id);
            var hotelImages = await GetImages(id);

            if (hotel != null)
            {
                hotel.IsDeleted = true;
                UnitOfWork.Hotels.Delete(hotel);
                foreach (var hotelImage in hotelImages)
                {
                    UnitOfWork.HotelImages.Delete(hotelImage);
                    ImageService.DeleteFileAsync(hotelImage.Image, WebRootPath);

                }
                SaveChanges();
                return true;
            }
            return false;
        }


        //=========================================================================================================


        public async Task<bool> Create (HotelDto hotelDto,string WebRootPath)
        {
        Hotel hotel = new Hotel()
        {
            ID = Guid.NewGuid(),
            Name = hotelDto.Name,
            Address = hotelDto.Address,
            CompId = hotelDto.companyId,
            IsDeleted = false,
            Rate = hotelDto.Rate,
        };
        await UnitOfWork.Hotels.Add(hotel);


            if (hotelDto.ImageUrl != null)
            {
                foreach (var image in hotelDto.ImageUrl)
                {
                    if (image.ContentType.StartsWith("image/"))
                    {

                        string FilePath = await ImageService.UploadFileAsync(image, WebRootPath);
                        var HotelImage = new HotelImages()
                        {
                            hotelId = hotel.ID,
                            Image = FilePath
                        };
                        await UnitOfWork.HotelImages.Add(HotelImage);
                    }
                    else
                    {
                        return false;
                       
                    }

                }
            }
            else
            {
                return false;

            }
            return true;
        }


        public async Task<bool> deleteHotel(Guid id)
        {
            var hotel = await UnitOfWork.Hotels.GetById(id);
            if (hotel != null)
            {
                hotel.IsDeleted = true;
                UnitOfWork.Hotels.Update(hotel);
                UnitOfWork.Complete();
                return true;
            }
            return false;

        }

    }
}
