using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClassLibrary
{
    public class Hotel
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, 5)]
        public double Rate { get; set; }
        [Required]
        public string Address { get; set; }

        public List<string>? Images { get; set; }

        //public bool AdditionalServices { get; set; }

        [Required]
        public int RoomId { get; set; }

        public virtual List<Room>? Rooms { get; set; }
    }
}
