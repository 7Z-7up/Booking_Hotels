using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace ClassLibrary
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Number { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        public bool Taken { get; set; } = false;

        [Required]
        public RoomType Type { get; set; }

        public List<string>? Images { get; set; }

        [Required]
        public int HotelId { get; set; }

        [ForeignKey("HotelID")]
        public virtual Hotel? Hotel { get; set; }
    }
}
