using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.Domain.Entities
{
    public class RoomImages
    {
        [ForeignKey("Room")]
        public Guid RoomId { get; set; }
        public string Image { get; set; }

        public virtual Room? Room { get; set; }
    }
}
