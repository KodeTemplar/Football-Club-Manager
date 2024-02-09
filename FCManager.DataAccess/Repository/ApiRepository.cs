using AutoMapper;
using FCManager.DataAccess.Data;
using FCManager.DataAccess.Interfaces;
using FCManager.IdentityManager.Contexts;
using FCManager.Models.Models;
using FCManager.Models.ViewModels;
using FCManager.Models.ViewModels.Team;
using FCManager.Models.ViewModels.TeamMembers;
using FCManager.Models.Wrappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static FCManager.Models.Wrappers.Enum;

namespace FCManager.DataAccess.Repository
{
    public class ApiRepository : IApiRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IdentityContext _identityContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApiRepository(IUnitOfWork unitOfWork, ApplicationDbContext context, IMapper mapper, IdentityContext identitycontext, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _mapper = mapper;
            _identityContext = identitycontext;
            _userManager = userManager;
        }
        public async Task<Response<string>> AddTeamMember(CreateTeamMemberViewModel request)
        {
            try
            {
                if (request == null)
                {
                    return ApplicationResponse.FailureMessage("Please fill Mandatory fields");
                }

                var teamMemberModel = _mapper.Map<TeamMember>(request);
                using (var trans = _context.Database.BeginTransaction())
                {
                    var addMember = await _context.TeamMembers.AddAsync(teamMemberModel);
                    var save = await _context.SaveChangesAsync();
                    if (save > 0)
                    {
                        if (!string.IsNullOrEmpty(request.Image))
                        {
                            var image = new TeamMemberImage
                            {
                                TeamMemberId = teamMemberModel.TeamMemberId,
                                Image = request.Image
                            };
                            await _context.TeamMemberImage.AddAsync(image);
                            var saver = await _context.SaveChangesAsync();
                            if (saver < 1)
                            {
                                trans.Rollback();
                                return ApplicationResponse.FailureMessage("Team member creation was not successful");
                            }
                        }

                        trans.Commit();
                        return ApplicationResponse.SuccessMessage("Team member creation was successful");
                    }
                    else
                    {
                        trans.Rollback();
                        return ApplicationResponse.FailureMessage("Team member creation was not successful");
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<string>> CreateTeam(CreateTeamViewModel createTeamViewModel)
        {
            try
            {
                if (string.IsNullOrEmpty(createTeamViewModel.Name) || string.IsNullOrEmpty(createTeamViewModel.Country) || string.IsNullOrEmpty(createTeamViewModel.Stadium))
                {
                    return ApplicationResponse.FailureMessage("Index", "Admin");
                }

                if (_unitOfWork.Team.GetFirstOrDefault(t => t.Name.ToLower() == createTeamViewModel.Name.ToLower() && t.Country.ToLower() == createTeamViewModel.Country.ToLower()) != null)
                {
                    return ApplicationResponse.FailureMessage("Team Already exist");
                }
                var teamModel = _mapper.Map<Team>(createTeamViewModel);
                using (var trans = _context.Database.BeginTransaction())
                {
                    var createTeam = await _context.Teams.AddAsync(teamModel);
                    var save = await _context.SaveChangesAsync();
                    if (save > 0)
                    {
                        if (!string.IsNullOrEmpty(createTeamViewModel.Image))
                        {
                            var image = new TeamLogo
                            {
                                TeamId = teamModel.TeamId,
                                Logo = createTeamViewModel.Image
                            };
                            await _context.TeamLogo.AddAsync(image);
                            var saver = await _context.SaveChangesAsync();
                            if (saver < 1)
                            {
                                trans.Rollback();
                                return ApplicationResponse.FailureMessage("Team creation was not successful");
                            }
                        }
                        trans.Commit();
                        return ApplicationResponse.SuccessMessage("Team creation was successful");
                    }
                    else
                    {
                        trans.Rollback();
                        return ApplicationResponse.FailureMessage("Team creation was not successful");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response<string>> Delete(string id)
        {
            var obj = _unitOfWork.Team.GetFirstOrDefault(u => u.TeamId == Guid.Parse(id));
            if (obj == null)
            {
                return ApplicationResponse.FailureMessage("Team was not found");
            }

            _unitOfWork.Team.Remove(obj);
            var save = await _context.SaveChangesAsync();
            if (save > 0)
            {
                return ApplicationResponse.SuccessMessage("Deletion was successful");
            }
            return ApplicationResponse.FailureMessage("Deletion failed");
        }

        public async Task<Response<string>> DeleteTeamMember(string id)
        {
            var obj = _unitOfWork.TeamMember.GetFirstOrDefault(u => u.TeamMemberId == Guid.Parse(id));
            if (obj == null)
            {
                return ApplicationResponse.FailureMessage("Team member was not found");
            }

            _unitOfWork.TeamMember.Remove(obj);
            var save = await _context.SaveChangesAsync();
            if (save > 0)
            {
                return ApplicationResponse.SuccessMessage("Deletion was successful");
            }
            return ApplicationResponse.FailureMessage("Deletion failed");
        }

        public async Task<Response<dynamic>> GetImage(string id, ImageType imageType)
        {
            try
            {
                if (imageType == ImageType.Logo)
                {
                    var image = await _context.TeamLogo.FirstOrDefaultAsync(x => x.TeamId == Guid.Parse(id));
                    return ApplicationResponse.SuccessMessage<dynamic>(image, "successful");
                }
                if (imageType == ImageType.TeamMember)
                {
                    var image = await _context.TeamMemberImage.FirstOrDefaultAsync(x => x.TeamMemberId == Guid.Parse(id));
                    return ApplicationResponse.SuccessMessage<dynamic>(image, "successful");
                }
                return ApplicationResponse.FailureMessage<dynamic>(null, "unsuccessful");
            }
            catch (Exception ex)
            {
                return ApplicationResponse.FailureMessage<dynamic>(null, "unsuccessful");
            }
        }

        public async Task<Response<TeamViewModel>> UpdateTeam(TeamViewModel request)
        {
            var response = await _unitOfWork.Team.UpdateTeam(request);
            return response;
        }

        public async Task<Response<UpdateTeamMemberViewModel>> UpdateTeamMember(UpdateTeamMemberViewModel request)
        {
            var response = await _unitOfWork.TeamMember.UpdateTeamMember(request);
            return response;
        }

        public async Task<Response<Team>> ViewTeamAsync(string id)
        {

            if (string.IsNullOrEmpty(id))
            {
                return ApplicationResponse.FailureMessage<Team>(null, "unsuccessful");
            }
            var teamInfo = _unitOfWork.Team.GetFirstOrDefault(u => u.TeamId == Guid.Parse(id));
            return ApplicationResponse.SuccessMessage<Team>(teamInfo, "successful");
        }

        public async Task<Response<dynamic>> ViewTeamList([FromRoute] int page, int pageSize)
        {
            try
            {
                var skip = (page - 1) * pageSize;
                var teamInfo = _unitOfWork.Team.GetAll()
                        .OrderByDescending(x => x.CreatedOn)
                        .Skip(skip)
                        .Take(pageSize)
                        .ToList();
                int pageCount = (int)Math.Ceiling(teamInfo.Count / (double)pageSize);
                var response = new PagedTeamResponse<Team>
                {
                    ResponseData = teamInfo,
                    Pages = pageCount,
                    CurrentPage = page,
                };
                return ApplicationResponse.SuccessMessage<dynamic>(response, "successful");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response<TeamMember>> ViewTeamMemberAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return ApplicationResponse.SuccessMessage<TeamMember>(null, "Team Id is invalid");
            }
            var teamMemberInfo = _unitOfWork.TeamMember.GetFirstOrDefault(u => u.TeamMemberId == Guid.Parse(id));
            return ApplicationResponse.SuccessMessage<TeamMember>(teamMemberInfo, "successful");
        }

        public async Task<Response<dynamic>> ViewTeamMemberList(int page, int pageSize, string teamId)
        {
            try
            {
                var skip = (page - 1) * pageSize;
                var teamInfo = _unitOfWork.TeamMember.GetAll(u => u.TeamId == Guid.Parse(teamId))
                        .OrderByDescending(x => x.CreatedOn)
                        .Skip(skip)
                        .Take(pageSize)
                        .ToList();
                int pageCount = (int)Math.Ceiling(teamInfo.Count / (double)pageSize);
                var response = new PagedTeamResponse<TeamMember>
                {
                    ResponseData = teamInfo,
                    Pages = pageCount,
                    CurrentPage = page,
                };
                return ApplicationResponse.SuccessMessage<dynamic>(response, "successful");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response<string>> Register(CreateTeamMemberViewModel request)
        {
            try
            {
                if (request == null)
                {
                    return ApplicationResponse.FailureMessage("Please fill Mandatory fields");
                }

                var teamMemberModel = _mapper.Map<TeamMember>(request);
                using (var trans = _context.Database.BeginTransaction())
                {
                    var addMember = await _context.TeamMembers.AddAsync(teamMemberModel);
                    var save = await _context.SaveChangesAsync();
                    if (save > 0)
                    {
                        if (!string.IsNullOrEmpty(request.Image))
                        {
                            var image = new TeamMemberImage
                            {
                                TeamMemberId = teamMemberModel.TeamMemberId,
                                Image = request.Image
                            };
                            await _context.TeamMemberImage.AddAsync(image);
                            var saver = await _context.SaveChangesAsync();
                            if (saver < 1)
                            {
                                trans.Rollback();
                                return ApplicationResponse.FailureMessage("Team member creation was not successful");
                            }
                        }

                        var user = new ApplicationUser
                        {
                            UserName = request.FirstName,
                            Email = request.Email,
                            EmailConfirmed = true,
                            PhoneNumberConfirmed = true,
                            IsActive = true
                        };

                        var createUserAccount = await CreateUserAccount(user, request.Password, request.MemberCategoryId);
                        if (createUserAccount)
                        {
                            trans.Commit();
                            return ApplicationResponse.SuccessMessage("Team member creation was successful");
                        }

                        trans.Rollback();
                        return ApplicationResponse.FailureMessage("Team member creation was not successful");
                    }
                    else
                    {
                        trans.Rollback();
                        return ApplicationResponse.FailureMessage("Team member creation was not successful");
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private async Task<bool> CreateUserAccount(ApplicationUser appUser, string password, int roleId)
        {
            if (_userManager.Users.All(u => u.Id != appUser.Id))
            {
                var user = _identityContext.Users.FirstOrDefault(x => x.Email == appUser.Email);
                if (user == null)
                {
                    var role = _context.MemberCategories.Select(x => new { x.TeamMemberCategory, x.CategoryId }).FirstOrDefault(x => x.CategoryId == roleId);
                    var createUser = _userManager.CreateAsync(appUser, password).Result;
                    var roles = _userManager.AddToRoleAsync(appUser, role.TeamMemberCategory).Result;
                    if (createUser.Succeeded && roles.Succeeded)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
