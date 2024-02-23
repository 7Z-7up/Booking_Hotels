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
        private readonly ICustomerService _customerService;
        private readonly ICompanyService _companyService;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(ICustomerService customerService,ICompanyService companyService,UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,SignInManager<AppUser> signInManager)
        {
            _customerService = customerService;
            _companyService = companyService;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
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
                var result = await _userManager.CreateAsync(appUser, customerDTO.Password);
                if (result.Succeeded)
                {
                    await _customerService.CreateAsync(customerDTO);
                    await _signInManager.PasswordSignInAsync(appUser, customerDTO.Password, false, false);
                    if (await _roleManager.FindByNameAsync(UserType.Customer.ToString()) is null)
                    {
                        AppRole role = new AppRole() { Name = UserType.Customer.ToString() };
                        await _roleManager.CreateAsync(role);
                    }
                    await _userManager.AddToRoleAsync(appUser, UserType.Customer.ToString());
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
        [HttpGet("RegisterCompany")]
        public IActionResult RegisterCompany()
        {
            return View(new CompanyDTO());
        }

        [HttpPost("RegisterCompany")]
        public async Task<IActionResult> RegisterCompany(CompanyDTO companyDTO)
        {
            if (ModelState.IsValid)
            {
                if (companyDTO.ImageFile != null)
                {
                    if (!companyDTO.ImageFile.ContentType.StartsWith("image/"))
                    {
                        ModelState.AddModelError("ImageFile", "Wrong File Format!");
                        return View(companyDTO);
                    }
                }
                companyDTO.Id = Guid.NewGuid();
                var appUser = new AppUser() { Id = companyDTO.Id, UserName = companyDTO.Email, Email = companyDTO.Email, PasswordHash = companyDTO.Password };
                var result = await _userManager.CreateAsync(appUser, companyDTO.Password);
                if (result.Succeeded)
                {
                    await _companyService.CreateAsync(companyDTO);
                    await _signInManager.PasswordSignInAsync(appUser, companyDTO.Password, false, false);
                    if (await _roleManager.FindByNameAsync(UserType.Company.ToString()) is null)
                    {
                        AppRole role = new AppRole() { Name = UserType.Company.ToString() };
                        await _roleManager.CreateAsync(role);
                    }
                    await _userManager.AddToRoleAsync(appUser, UserType.Company.ToString());
                    return RedirectToAction("Index", "Home",new {Area="Company"});
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("Register", error.Description);
                    }
                    return View(companyDTO);
                }
            }
            else
            {
                return View(companyDTO);
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
                var user = await _userManager.FindByEmailAsync(logInDTO.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, logInDTO.Password, logInDTO.RememberMe, false);

                    if (result.Succeeded)
                    {
                        if (await _userManager.IsInRoleAsync(user, UserType.Company.ToString()))
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
        [Route("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
