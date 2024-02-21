using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.DTO
{
    public class LogInDTO
    {
        [EmailAddress]
        [Required(ErrorMessage ="You Have Enter Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "You Have Enter Password")]
        [PasswordPropertyText]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
