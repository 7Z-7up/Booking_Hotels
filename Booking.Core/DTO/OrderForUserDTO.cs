using Booking.Core.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.DTO
{
    public class OrderForUserDTO
    {
        [Required]
        [DataType(DataType.Currency)]
        public decimal TotalCost { get; set; }

        [EnumDataType(typeof(PaymentType))]
        [Required]
        public PaymentType PaymentType { get; set; }

        [Required]
        public DateTime Start_Date { get; set; }

        [Required]
        public DateTime End_Date { get; set; }

        [Required]
        [MaxLength(100)]
        public string HotelName { get; set; }

        [Required]
        [Range(1, 5)]
        public double HotelRate { get; set; }

        [Required]
        [MaxLength(100)]
        public string HotelAddress { get; set; }

        [Required]
        public Guid OrderId { get; set; }

    }
}
