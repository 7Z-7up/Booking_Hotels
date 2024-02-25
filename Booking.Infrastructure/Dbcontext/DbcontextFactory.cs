using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Dbcontext
{
    internal class DbcontextFactory : IDesignTimeDbContextFactory<BookingDbContext>
    {
        public BookingDbContext CreateDbContext(string[] args)
        {

            var optionsBuilder = new DbContextOptionsBuilder<BookingDbContext>();
            optionsBuilder.UseSqlServer("Server=.;Database=BookingMVC_Project;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new BookingDbContext(optionsBuilder.Options);
        }
    }
}
