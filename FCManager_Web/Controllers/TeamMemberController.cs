using FCManager.DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FCManager_Web.Controllers
{
    public class TeamMemberController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeamMemberController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(string teamId)
        {
            var players = _unitOfWork.TeamMember.GetAll(u => u.TeamId == Guid.Parse(teamId));
            return View(players);
        }
    }
}
