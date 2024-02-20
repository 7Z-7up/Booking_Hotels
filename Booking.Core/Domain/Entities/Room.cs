using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Booking.Core.Helpers.Enums;
namespace Core.Domain.Entities
{
    [Table("Tb_Room")]
    public class Room
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Number { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Required]
        public bool Taken { get; set; } = false;

        [Required]
        public RoomType Type { get; set; }

        public List<string>? Images { get; set; }

        [Required]
        [ForeignKey("Hotel")]
        public Guid HotelId { get; set; }

        public virtual Hotel? Hotel { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
