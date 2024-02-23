using Booking.Core.Domain.Entities;
using Booking.Core.Helpers.Enums;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.DTO
{
    public class CreateOrderDTO
    {
        [Required]
        public Guid RoomId { get; set; }

        [Required]
        public Guid CustomerId { get; set; }
        
        [Required]
        public DateTime Start_Date { get; set; }
        
        [Required]
        public DateTime End_Date { get; set; }

        [ForeignKey(nameof(Order))]
        public Guid OrderID { get; set; }

    }
}
