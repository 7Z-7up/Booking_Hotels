using Booking.Core.DTO;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.ServicesContract
{
    public interface ICompanyService
    {
        public Task<IEnumerable<CompanyDTO>> GetAll();
        public Task<CompanyDTO> GetCompanyById(Guid id);
        public Task CreateAsync(CompanyDTO customerDTO);
        public Task UpdateAsync(CompanyDTO customerDTO);
        public Task DeleteAsync(Guid id);
    }
}
