using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClassLibrary
{
    [Table("Company")]
    internal class Company
    {
       
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DataType(DataType.Currency)]
        public decimal TotalProfits { get; set; }

        public virtual List<Hotel> Hotels { get; set; }
    }


}
