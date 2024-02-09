using AutoMapper;
using FCManager.DataAccess.Data;
using FCManager.DataAccess.Interfaces;
using FCManager.IdentityManager.Contexts;
using FCManager.Models.ViewModels.Account;
using IdentityManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static FCManager.Models.Wrappers.Enum;

namespace FCManager_Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAuthenticatedUserService _auth;
        private readonly IAccountService _accountService;
        private readonly ISession _session;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly IdentityContext _identityContext;

        public AccountController(ILogger<HomeController> logger, IAuthenticatedUserService auth, IAccountService accountService, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IMapper mapper, ApplicationDbContext context, IdentityContext identityContext)
        {
            _logger = logger;
            _auth = auth;
            _accountService = accountService;
            _session = httpContextAccessor.HttpContext.Session;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
            _identityContext = identityContext;
        }
        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Register()
        {
            var teams = _unitOfWork.Team.GetAll().ToList();
            var genders = await _context.Genders.ToListAsync();
            var roles = await _identityContext.Roles.ToListAsync();
            var categories = await _context.MemberCategories.ToListAsync();

            ViewBag.TeamList = new SelectList(teams, "TeamId", "Name");
            ViewBag.GenderList = new SelectList(genders, "GenderId", "GenderName");
            ViewBag.RoleList = new SelectList(roles, "Id", "Name");
            ViewBag.CategoryList = new SelectList(categories, "CategoryId", "TeamMemberCategory");
            return View();
        }

        public IActionResult Logout()
        {
            _session.Clear();
            return RedirectToAction("Index", "Home");
        }



        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (string.IsNullOrEmpty(loginViewModel.Email))
            {
                ModelState.AddModelError("email", "Email is required to login");
            }
            if (string.IsNullOrEmpty(loginViewModel.Password))
            {
                ModelState.AddModelError("password", "Password is required to login");
            }
            if (ModelState.IsValid)
            {
                var login = await _accountService.LoginAsync(loginViewModel, GenerateIPAddress());
                if (login.Succeeded)
                {
                    _session.SetString("jwToken", login.Data.JWToken);
                    _session.SetString("email", login.Data.Email);
                    _session.SetString("roleName", login.Data.RoleName);

                    if (login.Data.RoleName != Roles.Player.ToString())
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return Content(login.Message);
                }
            }
            return Content("An error occured while trying to login");
        }

        private string GenerateIPAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
