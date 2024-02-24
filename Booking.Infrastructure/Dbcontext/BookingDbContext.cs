using Booking.Core.Domain.Entities;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Dbcontext
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions options): base(options){}
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Hotel> Hotels  { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomOrder> RoomOrders { get; set; }
        public virtual DbSet<HotelImages> HotelImages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Configure tables Relations

            modelBuilder.Entity<Company>().HasMany(c => c.Hotels).WithOne(h=>h.Company).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Customer>().HasMany(c => c.Orders).WithOne(o=>o.Customer).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Hotel>().HasMany(h => h.Rooms).WithOne(r=>r.Hotel).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Order>().HasOne(o => o.Customer).WithMany(c => c.Orders).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Room>().HasOne(r=>r.Hotel).WithMany(h=>h.Rooms).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<RoomOrder>().HasKey(ro => new { ro.OrderID, ro.RoomID });
            modelBuilder.Entity<HotelImages>().HasKey(ro => new { ro.hotelId, ro.Image });

            //configure tables Properties

            modelBuilder.Entity<Company>().Property(c=>c.TotalProfits).HasColumnType("DECIMAL(18, 2)");
            modelBuilder.Entity<Order>().Property(o=>o.TotalCost).HasColumnType("DECIMAL(18, 2)");
            modelBuilder.Entity<Room>().Property(r=>r.Price).HasColumnType("DECIMAL(18, 2)");


        }
    }
}
