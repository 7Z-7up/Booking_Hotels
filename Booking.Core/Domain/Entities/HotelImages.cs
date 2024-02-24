using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.Domain.Entities
{
    [Table("Tb_HotelImages")]
    public class HotelImages
    {
        [ForeignKey("Hotel")]
        public Guid hotelId {  get; set; }
        public string Image { get; set; }

        public virtual Hotel? Hotel { get; set; }
    }
}
