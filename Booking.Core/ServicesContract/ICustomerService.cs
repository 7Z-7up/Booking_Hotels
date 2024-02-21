using Booking.Core.DTO;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.ServicesContract
{
    public interface ICustomerService
    {
        public Task<IEnumerable<CustomerDTO>> GetAll();
        public Task<CustomerDTO> GetCustomerById(Guid id);
        public Task CreateAsync(CustomerDTO customerDTO);
        public Task UpdateAsync(CustomerDTO customerDTO);
        public Task DeleteAsync(Guid id);
    }
}
