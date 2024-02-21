using Booking.Core.Domain.RepositoryContracts;
using Booking.Core.DTO;
using Booking.Core.ServicesContract;
using Core.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.Services
{
    public class CustomerService : ICustomerService
    {
        public IUnitOfWork UnitOfWork { get; }
        public ILogger Logger { get; }

        public CustomerService(IUnitOfWork unitOfWork, ILogger logger) 
        {
            UnitOfWork = unitOfWork;
            Logger = logger;
        }

        public async Task CreateAsync(CustomerDTO customerDTO)
        {
            Customer customer = CustomerDTO.ToCustomer(customerDTO);
            try
            {
                await UnitOfWork.Customers.Add(customer);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
            }
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CustomerDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<CustomerDTO> GetCustomerById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(CustomerDTO customer)
        {
            throw new NotImplementedException();
        }
    }
}
