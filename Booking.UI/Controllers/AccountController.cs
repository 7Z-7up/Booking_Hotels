using Booking.Core.Domain.IdentityEntities;
using Booking.Core.DTO;
using Booking.Core.Helpers.Enums;
using Booking.Core.ServicesContract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Booking.UI.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        public ICustomerService CustomerService { get; }
        public UserManager<AppUser> UserManager { get; }
        public RoleManager<AppRole> RoleManager { get; }
        public SignInManager<AppUser> SignInManager { get; }

        public AccountController(ICustomerService customerService,UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,SignInManager<AppUser> signInManager)
        {
            CustomerService = customerService;
            UserManager = userManager;
            RoleManager = roleManager;
            SignInManager = signInManager;
        }
        [HttpGet]
        [Route("Register")]
        public async Task<IActionResult> RegisterCustomer()
        {
            return View(new CustomerDTO());
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterCustomer(CustomerDTO customerDTO)
        {
            if (ModelState.IsValid)
            {
                 
                customerDTO.ID = Guid.NewGuid();
                var appUser = new AppUser() { Id = customerDTO.ID, UserName = customerDTO.Email, Email = customerDTO.Email, PasswordHash = customerDTO.Password, PhoneNumber = customerDTO.PhoneNumber };
                var result = await UserManager.CreateAsync(appUser, customerDTO.Password);
                if (result.Succeeded)
                {
                    await CustomerService.CreateAsync(customerDTO);
                    await SignInManager.PasswordSignInAsync(appUser, customerDTO.Password, false, false);
                    if (await RoleManager.FindByNameAsync(UserType.Customer.ToString()) is null)
                    {
                        AppRole role = new AppRole() { Name = UserType.Customer.ToString() };
                        await RoleManager.CreateAsync(role);
                    }
                    await UserManager.AddToRoleAsync(appUser, UserType.Customer.ToString());
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("Register", error.Description);
                    }
                    return View(customerDTO);
                }
            }
            else
            {
                return View(customerDTO);
            }
        }
        [HttpGet("Login")]
        public async Task<IActionResult> LogIn()
        {
            return View(new LogInDTO());
        }
        [HttpPost("Login")]
        public async Task<IActionResult> LogIn(LogInDTO logInDTO)
        {
            if(ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(logInDTO.Email);
                if (user != null)
                {
                    var result = await SignInManager.PasswordSignInAsync(user, logInDTO.Password, logInDTO.RememberMe, false);

                    if (result.Succeeded)
                    {
                        if (await UserManager.IsInRoleAsync(user, UserType.Company.ToString()))
                        {
                            return RedirectToAction("Index", "Home", new { area = "Company" });
                        }
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("LogIn", "You Email or Password are Wrong");
                        return View(logInDTO);
                    }
                }
            }
            return View(logInDTO);
        }
        public async Task<IActionResult> LogOut()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
