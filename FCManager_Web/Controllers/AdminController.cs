using AutoMapper;
using FCManager.DataAccess.Data;
using FCManager.DataAccess.Interfaces;
using FCManager.IdentityManager.Settings;
using FCManager.Models.Models;
using FCManager.Models.ViewModels.Team;
using FCManager.Models.ViewModels.TeamMembers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FCManager_Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISession _session;
        private readonly ViewAuthorization _auth;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminController(ApplicationDbContext contex, IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _context = contex;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _session = httpContextAccessor.HttpContext.Session;
            _auth = new ViewAuthorization(_httpContextAccessor);
        }

        public IActionResult Index()
        {
            var authorizer = _auth.IsAuthorized("Admin");
            if (!authorizer)
            {
                TempData["err"] = "You are not authorized";
                return RedirectToAction("Index", "Home");
            }
            IEnumerable<Team> teamList = _unitOfWork.Team.GetAll().OrderByDescending(x => x.CreatedOn);
            var responseObj = new CreateReadTeamViewModel
            {
                Teams = teamList
            };
            return View(responseObj);
        }

        public async Task<IActionResult> Players(string id)
        {
            var authorizer = _auth.IsAuthorized("Admin");
            if (!authorizer)
            {
                TempData["err"] = "You are not authorized";
                return RedirectToAction("Index", "Home");
            }
            IEnumerable<TeamMember> teamMembers = _unitOfWork.TeamMember.GetAll(p => p.TeamId == Guid.Parse(id), includeProperties: "MemberCategory").OrderByDescending(x => x.CreatedOn);
            var mapMembersResponse = _mapper.Map<IEnumerable<TeamMembersViewModel>>(teamMembers);
            var team = _unitOfWork.Team.GetFirstOrDefault(u => u.TeamId == Guid.Parse(id));
            var genders = await _context.Genders.ToListAsync();
            var categories = await _context.MemberCategories.ToListAsync();
            var viewResponse = new TeamMembersResponseViewModel
            {
                TeamMembers = mapMembersResponse,
                TeamModel = team,
            };

            ViewBag.AllGenderList = new SelectList(genders, "GenderId", "GenderName");
            ViewBag.CategoryList = new SelectList(categories, "CategoryId", "TeamMemberCategory");
            return View(viewResponse);
        }
    }
}
