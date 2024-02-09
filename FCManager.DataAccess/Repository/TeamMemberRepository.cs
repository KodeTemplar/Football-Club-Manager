using FCManager.DataAccess.Data;
using FCManager.DataAccess.Interfaces;
using FCManager.Models.Models;
using FCManager.Models.ViewModels.TeamMembers;
using FCManager.Models.Wrappers;
using Microsoft.EntityFrameworkCore;

namespace FCManager.DataAccess.Repository
{
    public class TeamMemberRepository : Repository<TeamMember>, ITeamMemberRepository
    {
        private ApplicationDbContext _db;

        public TeamMemberRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Response<UpdateTeamMemberViewModel>> UpdateTeamMember(UpdateTeamMemberViewModel request)
        {
            try
            {
                var existingTeamMember = await _db.TeamMembers.FirstOrDefaultAsync(x => x.TeamMemberId == request.TeamMemberId);
                if (existingTeamMember != null)
                {
                    existingTeamMember.FirstName = request.FirstName;
                    existingTeamMember.LastName = request.LastName;
                    existingTeamMember.Weight = request.Weight;
                    existingTeamMember.Nationality = request.Nationality;
                    existingTeamMember.Position = request.Position;
                    existingTeamMember.GenderId = request.GenderId;
                    existingTeamMember.PlayerNumber = request.PlayerNumber;
                    existingTeamMember.MemberCategoryId = request.MemberCategoryId;
                    existingTeamMember.DateOfBirth = request.DateOfBirth;
                    existingTeamMember.DateOfSigning = request.DateOfSigning;

                    using (var trans = _db.Database.BeginTransaction())
                    {
                        _db.TeamMembers.Update(existingTeamMember);
                        var save = await _db.SaveChangesAsync();
                        if (save > 0)
                        {
                            if (request.Image != null)
                            {
                                var existingLogo = await _db.TeamMemberImage.FirstOrDefaultAsync(x => x.TeamMemberId == request.TeamMemberId);
                                if (existingLogo != null)
                                {
                                    existingLogo.Image = request.Image;
                                    _db.TeamMemberImage.Update(existingLogo);
                                    var updateLogo = await _db.SaveChangesAsync();
                                    if (updateLogo < 1)
                                    {
                                        trans.Rollback();
                                        return ApplicationResponse.FailureMessage<UpdateTeamMemberViewModel>(null, "Unsuccessful");
                                    }
                                }
                                else
                                {
                                    var image = new TeamMemberImage
                                    {
                                        TeamMemberId = request.TeamMemberId,
                                        Image = request.Image
                                    };
                                    await _db.TeamMemberImage.AddAsync(image);
                                    var saveImage = await _db.SaveChangesAsync();
                                    if (saveImage < 1)
                                    {
                                        trans.Rollback();
                                        return ApplicationResponse.FailureMessage<UpdateTeamMemberViewModel>(null, "Unsuccessful");
                                    }
                                }
                            }
                            trans.Commit();
                            return ApplicationResponse.SuccessMessage<UpdateTeamMemberViewModel>(request, "Successful");
                        }
                        else
                        {
                            trans.Rollback();
                            return ApplicationResponse.FailureMessage<UpdateTeamMemberViewModel>(null, "Unsuccessful");
                        }
                    }
                }
                return ApplicationResponse.NotFoundMessage<UpdateTeamMemberViewModel>(null, "Team Member not found");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
