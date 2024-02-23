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
    public class CompanyDTO 
    {
        [Required(ErrorMessage ="You Must Provide ID")]
        public Guid Id { get; set; }
        [Required(ErrorMessage ="You Must Provide Name")]
        public string Name { get; set; }
        [DisplayName("Upload Image")]
        public IFormFile? ImageFile { get; set; }
        public decimal? TotalProfits { get; set; }
        public static Company ToCompany(CompanyDTO companyDto)
        {
            return new Company()
            {
                ID = companyDto.Id,
                Image= "",
                Name = companyDto.Name,
                TotalProfits = companyDto.TotalProfits,
            };
        }


    }
}
