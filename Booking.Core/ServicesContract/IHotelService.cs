using Booking.Core.Domain.Entities;
using Booking.Core.Domain.RepositoryContracts;
using Booking.Core.DTO.hotel;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.ServicesContract
{
    public interface IHotelService
    {
        public Task<ICollection<HotelDto>> showAllHotel();
        public Task<IEnumerable<Company>> GetCompanies();
        public Task<bool> Create(HotelDto hotelDto,string WebRootPath);
        public Task<bool> deleteHotel(Guid id);
        public Task<Hotel> GetHotel(Guid id);
        public Task<IEnumerable<HotelImages>> GetImages(Guid id);
        public Task<HotelDto> GetHotelDetails(Guid id,Hotel hotel);
        public Task<bool> Permanent_Deletion(Guid id, string WebRootPath);

        public int SaveChanges();


    }
}
