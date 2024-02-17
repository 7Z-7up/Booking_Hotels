using Booking.Core.Helpers.Enums;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.Domain.Entities
{
    [Table("Tb_Order")]
    public class Order
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal TotalCost { get; set; }
        [Required]
        public PaymentType PaymentType { get; set; }
        [Required]
        [ForeignKey(nameof(Customer))]
        public Guid CustomerID { get; set; }
        public bool IsDeleted { get; set; } = false;

        public virtual Customer? Customer { get; set; }
    }
}
