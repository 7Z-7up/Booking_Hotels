using Booking.Core.Domain.Entities;
using Booking.Core.Domain.IdentityEntities;
using Booking.Core.Helpers.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities
{
    [Table("Tb_Customer")]
    public class Customer
    {
        [Key]
        [ForeignKey(nameof(AppUser))]
        public Guid ID { get; set; }
        [DataType(DataType.ImageUrl)]
        public string? ImageUrl { get; set; }
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Required]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [DataType(DataType.EmailAddress)]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required]
        public Gender Gender { get; set; }
        public bool IsDeleted { get; set; } = false;
        public virtual ICollection<Order>? Orders { get; set; }
        public virtual AppUser? AppUser { get; set; }

    }
}
