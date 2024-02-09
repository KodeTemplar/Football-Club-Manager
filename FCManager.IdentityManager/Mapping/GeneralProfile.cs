using AutoMapper;
using FCManager.Models.Account;
using FCManager.Models.Models;
using FCManager.Models.ViewModels.Team;
using FCManager.Models.ViewModels.TeamMembers;

namespace FCManager.IdentityManager.Mapping
{
    internal class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<ApplicationUser, AuthenticationResponse>().ReverseMap();
            CreateMap<CreateTeamViewModel, Team>().ReverseMap();
            CreateMap<CreateTeamViewModel, Team>().ReverseMap();
            CreateMap<TeamMember, CreateTeamMemberViewModel>().ReverseMap();
            CreateMap<TeamMember, TeamMembersViewModel>().ReverseMap();
        }
    }
}
