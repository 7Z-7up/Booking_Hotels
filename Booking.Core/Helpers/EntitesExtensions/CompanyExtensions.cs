using Booking.Core.DTO;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.Helpers.EntitesExtensions
{
    public static class CompanyExtensions
    {
        public static CompanyDTO ToCustomerDTO(this Company company)
        {
            return new CompanyDTO()
            {
                Id = company.ID,
                Name = company.Name,
                TotalProfits = company.TotalProfits,
            };

        }
    }
}