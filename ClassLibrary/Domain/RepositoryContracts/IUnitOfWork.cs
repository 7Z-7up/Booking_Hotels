using Booking.Core.Domain.Entities;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.Domain.RepositoryContracts
{
    public interface IUnitOfWork
    {
        IBaseRepository<Company> Companies { get; }
        IBaseRepository<Customer> Customers { get; }
        IBaseRepository<Hotel> Hotels { get; }
        IBaseRepository<Order> Orders { get; }
        IBaseRepository<Room> Rooms { get; }
        IBaseRepository<RoomOrder> RoomOrders { get; }

        int Complete();
    }
}
