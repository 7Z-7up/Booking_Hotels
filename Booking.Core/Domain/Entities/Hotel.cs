using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities
{
    [Table("Tb_Hotel")]
    public class Hotel
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [Range(1, 5)]
        public double Rate { get; set; }
        [Required]
        [MaxLength(100)]
        public string Address { get; set; }

        //public bool AdditionalServices { get; set; }

        [Required]
        [ForeignKey(nameof(Company))]
        public Guid CompId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public virtual Company? Company { get; set; }
        public virtual ICollection<Room>? Rooms { get; set; }

    }
}
