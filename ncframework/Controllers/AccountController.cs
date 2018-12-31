using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ncframework.Models;
using ncframework.Models.ViewModel;
using Newtonsoft.Json;

namespace ncframework.Controllers
{
    public class AccountController : Controller
    {
        private readonly IxContext _context;

        public AccountController(IxContext context)
        {
            _context = context;
        }

        public AppCurrentUser CurrentUser
        {
            get
            {
                return new AppCurrentUser(this.User as ClaimsPrincipal);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var _user = await _context.User.FirstOrDefaultAsync(o => o.UserId == model.UserName && o.Password == model.Password);

                    if (_user != null)
                    {
                        AppUser user = new AppUser()
                        {
                            Id = _user.UserId,
                            IsAdmin = _user.IsAdmin.ToString(),
                            Name = _user.UserId,
                            Email = string.Empty,
                            Roles = string.Empty,
                            MenuJson = string.Empty
                        };

                        if (!_user.IsAdmin)
                        {
                            var _employee = await _context.Employee.FindAsync(_user.EmployeeId);

                            if (_employee != null)
                            {
                                user.Email = _employee.Email;
                                user.Name = _employee.Name;
                                user.Roles = string.IsNullOrEmpty(_employee.GroupMenu) ? string.Empty : _employee.GroupMenu;
                            }

                            string[] menuArray = _employee.GroupMenu.Split(",");
                            var _access = await _context.Access.Where(o => menuArray.Contains(o.Group.Code)).ToListAsync();
                            string[] accessArray = _access.Select(o => o.MenuId).ToArray();
                            var _menu = await _context.Menu.Where(o => accessArray.Contains(o.Id)).ToListAsync();

                            string _menuJson = JsonConvert.SerializeObject(_menu);
                            user.MenuJson = _menuJson;
                        }
                        else
                        {
                            var _menu = await _context.Menu.ToListAsync();
                            string _menuJson = JsonConvert.SerializeObject(_menu);
                            user.MenuJson = _menuJson;
                        }

                        var userClaims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id),
                            new Claim(ClaimTypes.Name, user.Name),
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.Role, user.Roles),
                            new Claim(ClaimTypes.UserData, user.MenuJson)
                        };

                        var identity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    }              

                    //   await this.HubContext.Clients.All.SendAsync("Join", user.UserName);

                    return RedirectToLocal(returnUrl);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(model);
        }


        private IActionResult RedirectToLocal(string returnUrl)
        {

            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}