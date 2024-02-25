using Booking.Core.Domain.RepositoryContracts;
using Booking.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.ServicesContract
{
    public interface IOrderForUserService
    {
        public Task<IEnumerable<OrderForUserDTO>> GetHistoryUserOrder(Guid id);
        public Task<bool> Delete(Guid id);
        public Task<OrderForUserDTO> GetOrderId(Guid id);
    }
}
