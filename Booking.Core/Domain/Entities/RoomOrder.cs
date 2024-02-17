using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.Domain.Entities
{
    public class RoomOrder
    {
        [ForeignKey(nameof(Room))]
        public Guid RoomID { get; set; }
        [ForeignKey(nameof(Order))]
        public Guid OrderID { get; set; }
        [Required]
        public DateTime Start_Date { get; set; }
        [Required]
        public DateTime End_Date { get; set; }
        public virtual Order? Order { get; set; }
        public virtual Room? Room { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
