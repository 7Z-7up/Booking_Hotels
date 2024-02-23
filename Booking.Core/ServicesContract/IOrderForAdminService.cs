using Booking.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.ServicesContract
{
    public interface IOrderForAdminService
    {
        public Task<IEnumerable<OrdersForAdminDTO>> GetAll();
        public Task<OrdersForAdminDTO> GetOrderId(Guid id);
    }
}
