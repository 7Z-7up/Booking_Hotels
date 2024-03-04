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
        public List<Guid> RoomId { get; set; }

        [Required]
        public List<Guid> CustomerId { get; set; }

        [ForeignKey(nameof(Order))]
        public List<Guid> OrderID { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public List<int> NumberofDays { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public List<int> Number { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        [DataType(DataType.Currency)]
        public List<decimal> Price { get; set; }

        [Required]
        public List<RoomType> Type { get; set; }

        public CreateOrderDTO()
        {
            Number = new List<int>();
            RoomId = new List<Guid>();
            CustomerId = new List<Guid>();
            Type = new List<RoomType>();
            Price = new List<decimal>();
            OrderID = new List<Guid>();
            NumberofDays=new List<int>();
        }
    }
}
