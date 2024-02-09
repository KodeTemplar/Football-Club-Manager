using FCManager.Models.Models;
using FCManager.Models.ViewModels.Team;
using FCManager.Models.ViewModels.TeamMembers;
using FCManager.Models.Wrappers;
using static FCManager.Models.Wrappers.Enum;

namespace FCManager.DataAccess.Interfaces
{
    public interface IApiRepository
    {
        Task<Response<Team>> ViewTeamAsync(string id);
        Task<Response<TeamMember>> ViewTeamMemberAsync(string id);
        Task<Response<dynamic>> ViewTeamList(int page, int pageSize);
        Task<Response<dynamic>> ViewTeamMemberList(int page, int pageSize, string teamId);
        Task<Response<string>> CreateTeam(CreateTeamViewModel createTeamViewModel);
        Task<Response<dynamic>> GetImage(string id, ImageType imageType);
        Task<Response<string>> Delete(string id);
        Task<Response<string>> DeleteTeamMember(string id);
        Task<Response<TeamViewModel>> UpdateTeam(TeamViewModel request);
        Task<Response<UpdateTeamMemberViewModel>> UpdateTeamMember(UpdateTeamMemberViewModel request);
        Task<Response<string>> AddTeamMember(CreateTeamMemberViewModel request);
        Task<Response<string>> Register(CreateTeamMemberViewModel request);
    }
}
