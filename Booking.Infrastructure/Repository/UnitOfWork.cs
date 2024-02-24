using Booking.Core.Domain.Entities;
using Booking.Core.Domain.RepositoryContracts;
using Booking.Infrastructure.Dbcontext;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public BookingDbContext _context;

        public IBaseRepository<Company> Companies { get; private set; }
        public IBaseRepository<Customer> Customers { get; private set; }
        public IBaseRepository<Hotel> Hotels { get; private set; }
        public IBaseRepository<Order> Orders { get; private set; }
        public IBaseRepository<Room> Rooms { get; private set; }
        public IBaseRepository<RoomOrder> RoomOrders { get; private set; }

        public IBaseRepository<HotelImages> HotelImages {  get; private set; }

        public UnitOfWork(BookingDbContext context)
        {
            _context = context;
            Companies = new BaseRepository<Company>(_context);
            Customers = new BaseRepository<Customer>(_context);
            Hotels = new BaseRepository<Hotel>(_context);
            Orders = new BaseRepository<Order>(_context);
            Rooms = new BaseRepository<Room>(_context);
            RoomOrders = new BaseRepository<RoomOrder>(_context);
            HotelImages = new BaseRepository<HotelImages>(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
