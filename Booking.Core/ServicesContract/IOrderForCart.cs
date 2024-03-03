using Booking.Core.Domain.Entities;
using Booking.Core.DTO;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.ServicesContract
{
    public interface IOrderForCart
    {
        public Task Create(Guid Uesrid, CreateOrderDTO createOrderDTO);
        public Task Delete(Guid RoomId, CreateOrderDTO createOrderDTO);
        public Task<CreateOrderDTO> PutRoomsInDTO(List<Guid> roomid);
        //public Task Update(Guid OrderId, CreateOrderDTO createOrderDTO);
        public Task<Room> GetById(Guid RoomId);
        public Task<Order> GetOrderById(Guid Order);
    }
}
