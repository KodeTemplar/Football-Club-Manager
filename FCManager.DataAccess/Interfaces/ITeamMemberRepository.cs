using FCManager.Models.Models;
using FCManager.Models.ViewModels.TeamMembers;
using FCManager.Models.Wrappers;

namespace FCManager.DataAccess.Interfaces
{
    public interface ITeamMemberRepository : IRepository<TeamMember>
    {
        Task<Response<UpdateTeamMemberViewModel>> UpdateTeamMember(UpdateTeamMemberViewModel request);
    }
}
