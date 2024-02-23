using Booking.Core.Helpers.Enums;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.DTO
{
    public class CompanyDTO : Company
    {
       
            [DisplayName("Upload Image")]
            public IFormFile? ImageFile { get; set; }

            public CompanyDTO()
            {

            }

            public CompanyDTO(Company company)
            {
                Name = company.Name;
                ID = company.ID;
                Image = company.Image;
            }
        


    }
}
