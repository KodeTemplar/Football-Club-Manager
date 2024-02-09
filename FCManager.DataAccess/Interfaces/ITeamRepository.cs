using FCManager.Models.Models;
using FCManager.Models.ViewModels.Team;
using FCManager.Models.Wrappers;

namespace FCManager.DataAccess.Interfaces
{
    public interface ITeamRepository : IRepository<Team>
    {
        Task<Response<TeamViewModel>> UpdateTeam(TeamViewModel request);
    }
}
