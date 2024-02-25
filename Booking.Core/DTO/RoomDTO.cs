using Booking.Core.Helpers.Enums;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.DTO
{
    public class RoomDTO 
    {
        public Guid ID { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Required]
        public bool Taken { get; set; } = false;

        [Required]
        public RoomType Type { get; set; }

       public IEnumerable<string>? Images { get; set; }
        public int RoomNum { get; set; }


        [DisplayName("Upload Images")]
        public List<IFormFile>? ImageFiles { get; set; }



        public String? HotelName { get; set; }
    }
}
