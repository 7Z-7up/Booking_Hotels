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
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(IUnitOfWork unitOfWork, ILogger<CustomerService> logger) 
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task CreateAsync(CustomerDTO customerDTO)
        {
            Customer customer = CustomerDTO.ToCustomer(customerDTO);
            try
            {
                await _unitOfWork.Customers.Add(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                Customer customer =await _unitOfWork.Customers.Find(c=>c.ID==id);
                customer.IsDeleted = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        public async Task<IEnumerable<CustomerDTO>> GetAll()
        {
            try
            {
                var customers =await _unitOfWork.Customers.GetAll();
                var customerDtos = customers.Select(c => c.ToCustomerDTO());
                return customerDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Enumerable.Empty<CustomerDTO>();
            }
        }

        public async Task<CustomerDTO> GetCustomerById(Guid id)
        {
            try
            {
                var customer = await _unitOfWork.Customers.GetById(id);
                var customerDto = customer.ToCustomerDTO();
                return customerDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new CustomerDTO();
            }
        }

        public async Task UpdateAsync(CustomerDTO customerDto)
        {
            var customer = CustomerDTO.ToCustomer(customerDto);
            try
            {
                _unitOfWork.Customers.Update(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
