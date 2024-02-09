using FCManager.DataAccess.Interfaces;
using FCManager_Web.Models;
using IdentityManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FCManager_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAuthenticatedUserService _auth;
        private readonly IAccountService _accountService;

        public HomeController(ILogger<HomeController> logger, IAuthenticatedUserService auth, IAccountService accountService)
        {
            _logger = logger;
            _auth = auth;
            _accountService = accountService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ViewPlayers()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}