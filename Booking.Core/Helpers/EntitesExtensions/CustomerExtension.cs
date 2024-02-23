using Booking.Core.DTO;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.Helpers.EntitesExtensions
{
    public static class CustomerExtention
    {
        public static CustomerDTO ToCustomerDTO(this Customer customer)
        {
            return new CustomerDTO()
            {
                ID = customer.ID,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email,
                Gender = customer.Gender,
                DateOfBirth = customer.DateOfBirth,
                ImageUrl = customer.ImageUrl,
            };
        }
    }
}
