using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Booking.Core.Domain.IdentityEntities
{
    public class AppUser:IdentityUser<Guid>
    {
    }
}
