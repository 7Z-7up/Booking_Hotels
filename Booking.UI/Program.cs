using Booking.Core.Domain.IdentityEntities;
using Booking.Core.Domain.RepositoryContracts;
using Booking.Core.Helpers.Services;
using Booking.Core.Services;
using Booking.Core.ServicesContract;
using Booking.Infrastructure.Dbcontext;
using Booking.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BookingDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<BookingDbContext>()
    .AddDefaultTokenProviders()
    .AddUserStore<UserStore<AppUser, AppRole, BookingDbContext, Guid>>()
    .AddRoleStore<RoleStore<AppRole, BookingDbContext, Guid>>();
builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.AddDebug();
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IRoomService, RoomService>();

builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<UploadImageService>();


builder.Services.AddSingleton<IStartupFilter>(new StartupFilterHelperService(InitializeHelperService));
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<ICustomerService,CustomerService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
void InitializeHelperService(IWebHostEnvironment webHostEnvironment)
{
    HelperService.Initialize(webHostEnvironment);
}
