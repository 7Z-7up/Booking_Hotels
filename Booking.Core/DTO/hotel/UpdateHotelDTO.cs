using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.DTO.hotel
{
    public class UpdateHotelDTO:HotelDto
    {
        public int ID { get; set; }
        public List<string>? ExsitingFile { get; set; }

    }
}
