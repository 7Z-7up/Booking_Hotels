using Booking.Core.Domain.IdentityEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities
{
    [Table("Tb_Company")]
    public class Company
    {
       
        [Key]
        [ForeignKey(nameof(AppUser))]
        public Guid ID { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [DataType(DataType.ImageUrl)]
        public string? Image {  get; set; }
        [DataType(DataType.Currency)]
        public decimal? TotalProfits { get; set; }

        public bool IsDeleted { get; set; } = false;
        public virtual ICollection<Hotel>? Hotels { get; set; }
        public virtual AppUser? AppUser { get; set; }
    }


}
