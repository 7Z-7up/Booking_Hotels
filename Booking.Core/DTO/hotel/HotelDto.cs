using Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.DTO.hotel
{
    public class HotelDto
    {
        public Guid ID { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }

        public bool IsDeleted { get; set; }

        public double Rate { get; set; }

         
        public IEnumerable<Company>? Comps { get; set; }
        public string? OwnerCompany { get; set; }
        public Guid companyId { get; set; }
        public ICollection<IFormFile>? ImageUrl { get; set; }
        public List<string>? ImagePath { get; set; }
 
        // public ICollection<Room> HotelRooms { get; set; }




    }
}
