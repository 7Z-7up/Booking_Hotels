using Booking.Core.Helpers.Enums;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.DTO
{
    public class CustomerDTO
    {
        public Guid ID { get; set; }
        [DataType(DataType.ImageUrl)]
        public string? ImageUrl { get; set; }
        [Required(ErrorMessage ="You must provide a First Name")]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "You must provide a Last Name")]
        [MaxLength(100)]
        public string LastName { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "You must provide a Phone Number")]
        [Phone]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [MaxLength(100)]
        [Required(ErrorMessage ="You Must Enter an Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "You must provide a Gender")]
        public Gender Gender { get; set; }
        [DataType(DataType.Password)]
        [PasswordPropertyText]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [PasswordPropertyText]
        [Compare(nameof(Password),ErrorMessage ="Password and Confirm Password Are Different")]
        public string ConfirmPassword { get; set; }

        public static Customer ToCustomer(CustomerDTO customerDTO)
        {
            return new Customer
            {
                ID = customerDTO.ID,
                FirstName = customerDTO.FirstName,
                LastName = customerDTO.LastName,
                Email = customerDTO.Email,
                ImageUrl = customerDTO.ImageUrl,
                PhoneNumber = customerDTO.PhoneNumber,
                DateOfBirth = customerDTO.DateOfBirth,
                Gender = customerDTO.Gender
            };
        }
    }
}
