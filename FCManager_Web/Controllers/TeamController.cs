using AutoMapper;
using FCManager.DataAccess.Data;
using FCManager.DataAccess.Interfaces;
using FCManager.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace FCManager_Web.Controllers;
public class TeamController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public TeamController(ApplicationDbContext contex, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _context = contex;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
        IEnumerable<Team> objCategoryList = _unitOfWork.Team.GetAll(includeProperties: "TeamLogo").OrderByDescending(x => x.CreatedOn);
        return View(objCategoryList);
    }

    public IActionResult ViewPlayers(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return Content("This Team is invalid");
        }
        return RedirectToAction("Index", "TeamMember", new { teamId = id });
    }

    public IActionResult Player()
    {
        return View();
    }

    //[Authorize]
    public IActionResult Create()
    {
        return View();
    }
}

