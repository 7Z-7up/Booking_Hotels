using Booking.Core.Helpers.Enums;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.DTO
{
    public class OrderRoomViewDTO
    {
        [Key]
        public Guid OrderId { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal TotalCost { get; set; }

        [Required]
        public PaymentType PaymentType { get; set; }

        [Required]
        public Guid CustomerID { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Start_Date { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime End_Date { get; set; }
    }
}
